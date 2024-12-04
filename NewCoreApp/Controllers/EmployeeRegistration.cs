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

                return Json(new { success = true, message = "Data submitted successfully!" });
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
    }
}
