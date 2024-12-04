using Microsoft.AspNetCore.Mvc;
using Data;
using Entity.Modal; // Adjust the namespace for your DbContext
using Microsoft.AspNetCore.Mvc.Rendering;
using InfraStucture.Repository;
using InfraStucture.Contract;
using ClassDAL;
using Entity.ViewModal;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace NewCoreApp.Controllers.EmployeeRegistration
{
    public class EmployeeRegistrationController : Controller
    {
        private readonly UserMgMtContext _context; // Replace with your actual DbContext
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dbOperations;
        public EmployeeRegistrationController(UserMgMtContext context,IUnitOfWork unitOfWork,dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbOperations = dbRepository;
        }

        // GET: EmployeeRegistration/Create
        public IActionResult Create()
        {
            
            var items = _context.State
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),   
                    Text = x.StateName             
                })
                .ToList();

            // ViewBag me data store karna
            ViewBag.stateDropdown = items;
            return View(); // Return the view for creating a new employee
        }

       // POST: EmployeeRegistration/Create
       [HttpPost]
        public IActionResult CreateEmployee(EmployeeRegistrationVM model)
        {
            //if (ModelState.IsValid)
            //{
                // ViewModel ko Entity me map karna
                var employee = new EmployeeReg
                {
                    StateId = model.StateId,
                    EmployeeName = model.EmployeeName,
                    EmpFatherName = model.EmpFatherName,
                };
       
                // Document ko save karna
                if (model.FileUpload != null && model.FileUpload.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/EmployeeFileUpload", model.FileUpload.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.FileUpload.CopyTo(stream);
                    }

                    employee.FileUpload = filePath; // Document path ko employee entity me store karenge
                }

                // Data ko main table me save karna
                _unitOfWork.employeeRegRepository.SaveAsync(employee);

            TempData["SuccessMessage"] = "Data submitted successfully!";
            return RedirectToAction("EmployeeList");
            //}

            return Json(new { success = false, message = "Validation failed" });
        }
        // GET: EmployeeRegistration/EmployeeList
        public async Task<IActionResult> EmployeeList()
        {
            // UnitOfWork se employee data fetch karna
            var employees = await _unitOfWork.employeeRegRepository.GetAllAsync(); // Replace with your method to fetch employees
             // Replace with your method to fetch states
             var states = await _context.State.ToListAsync();
            var employeeViewModels = (from emp in employees
                                      join state in states on emp.StateId equals state.Id // Perform join
                                      select new EmployeeRegistrationVM
                                      {
                                          EmployeeId = emp.EmployeeId, // Primary key
                                          EmployeeName = emp.EmployeeName,
                                          EmpFatherName = emp.EmpFatherName,
                                          StateId = emp.StateId,
                                          StateName = state.StateName, // Get StateName from joined state
                                          varfile = emp.FileUpload
                                      }).ToList();

            return View("EmployeeList", employeeViewModels);
        }
        // GET: Edit Employee
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _unitOfWork.employeeRegRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Map employee data to the view model
            var model = new EmployeeRegistrationVM
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                EmpFatherName = employee.EmpFatherName,
                StateId = employee.StateId,
                varfile = employee.FileUpload // Assuming this is the file path
            };

            // Fetch states for the dropdown
            ViewBag.stateDropdown = new SelectList(await _context.State.ToListAsync(), "Id", "StateName", model.StateId);
            ViewBag.IsEditMode = true;
            return View("Create",model); // Use CreateEmployee view for editing
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeRegistrationVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    // Reload the state dropdown if there are validation errors
            //    ViewBag.stateDropdown = new SelectList(await _context.State.ToListAsync(), "Id", "StateName", model.StateId);
            //    return View("Create", model); // Redirect back to the same view
            //}

            // Find the existing employee record in the database
            var employee = await _unitOfWork.employeeRegRepository.GetByIdAsync(model.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            // Update employee properties
            employee.EmployeeName = model.EmployeeName;
            employee.EmpFatherName = model.EmpFatherName;
            employee.StateId = model.StateId;

            // Update the file if a new one has been uploaded
            if (model.FileUpload != null && model.FileUpload.Length > 0)
            {
                // Save the new file and update the file path
                var filePath = Path.Combine("wwwroot/EmployeeFileUpload", model.FileUpload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.FileUpload.CopyToAsync(stream);
                }
                employee.FileUpload = filePath; // Update the file path in the database
            }

            // Save the changes
            await _unitOfWork.employeeRegRepository.UpdateAsync(employee);
            // Save transaction if using Unit of Work

            return RedirectToAction("EmployeeList"); // Redirect to the list after update
        }

    }
}
