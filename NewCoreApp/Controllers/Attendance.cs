using ClassDAL;
using Data;
using Entity.Modal;
using Entity.ViewModal;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;

namespace NewCoreApp.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly UserMgMtContext _context; // Replace with your actual DbContext
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dbOperations;
        public AttendanceController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbOperations = dbRepository;
        }
        [HttpGet]
        public IActionResult Index(DateTime? date)
        {
            var selectedDate = date ?? DateTime.Today;
            var students = _context.StudentReg.ToList();
            var attendanceList = students.Select(student => new Tbl_Attendance
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Date = selectedDate, // Set the selected date
                IsPresent = false // Default to absent
            }).ToList();

            var model = new AttendanceViewModel
            {
                SelectedDate = selectedDate,
                Tbl_Attendance = attendanceList
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SaveAttendance(AttendanceViewModel model)
        {
            // Check if the form has been submitted with valid data
            if (model.Tbl_Attendance != null && model.Tbl_Attendance.Any())
            {
                foreach (var attendance in model.Tbl_Attendance)
                {
                    // Check if a record already exists for this student on the selected date
                    var existingRecord = _context.Tbl_Attendance
                        .FirstOrDefault(a => a.StudentId == attendance.StudentId && a.Date.Date == model.SelectedDate.Date);

                    if (existingRecord != null)
                    {
                        // Update existing record if found
                        existingRecord.IsPresent = attendance.IsPresent;
                    }
                    else
                    {
                        // Add new record if not found
                        _context.Tbl_Attendance.Add(new Tbl_Attendance
                        {
                            StudentId = attendance.StudentId,
                            StudentName = attendance.StudentName,
                            Date = model.SelectedDate,
                            IsPresent = attendance.IsPresent
                        });
                    }
                }

                // Save the changes to the database
                _context.SaveChanges();

                // Provide feedback to the user
                TempData["SuccessMessage"] = "Attendance saved successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "No attendance data to save.";
            }

            // Redirect back to the index page with the selected date
            return RedirectToAction("Index", new { date = model.SelectedDate });
        }
    }
}
