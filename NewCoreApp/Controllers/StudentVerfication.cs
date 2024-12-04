using ClassDAL;
using Data;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace NewCoreApp.Controllers
{
    public class StudentVerficationController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;   
        private readonly dbRepository _dbOperations;    
        public StudentVerficationController(UserMgMtContext context,IUnitOfWork unitOfWork,dbRepository dbRepository)
        {
            _context = context; 
            _unitOfWork = unitOfWork;   
            _dbOperations = dbRepository;   
        }
        public IActionResult StudentVerification()
        {
            var StudentVerification = _context.Tbl_StudentVerifications.ToList();   
            return View(StudentVerification);
        }
        // GET: Load student details for verification in the popup
        //public IActionResult Verify(int id)
        //{
        //    var student = _context.Tbl_StudentVerifications.FirstOrDefault(s => s.StdVerifcnID == id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    return PartialView("_VerifyPopup", student);
        //}
        // Check verification status and return response
        public IActionResult CheckVerification(int id)
        {
            var student = _context.Tbl_StudentVerifications.FirstOrDefault(s => s.StdVerifcnID == id);

            if (student == null)
            {
                return NotFound();
            }

            // If the student is already verified
            if (student.IsVerified)
            {
                return Content("Verified");
            }

            // Load Partial View for unverified students
            return PartialView("_VerifyPopup", student);
        }
        // POST: Save verification details
        [HttpPost]
        public IActionResult VerifyStudent(int id)
        {
            var student = _context.Tbl_StudentVerifications.FirstOrDefault(s => s.StdVerifcnID == id);
            if (student != null)
            {
                student.IsVerified = true;
                _context.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction("StudentVerification");
        }
    }
}
