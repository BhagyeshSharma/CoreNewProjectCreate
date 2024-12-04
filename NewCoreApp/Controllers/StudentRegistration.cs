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
using System.Data;

namespace NewCoreApp.Controllers
{
    public class StudentRegistrationController : Controller
    {
        private readonly UserMgMtContext _context; // Replace with your actual DbContext
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dbOperations;
        public StudentRegistrationController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbOperations = dbRepository;
        }

        // GET: StudentRegistration/Create
        public IActionResult Create()
        {

            var items = _context.Tbl_Subject
                .Select(x => new SelectListItem
                {
                    Value = x.SubjectId.ToString(),
                    Text = x.SubjectName,   
                })
                .ToList();

            // ViewBag me data store karna
            ViewBag.subjectDropdown = items;
            return View(); // Return the view for creating a new employee
        }
        [HttpPost]
        public IActionResult SaveAllData([FromBody] studentdataviewmodel viewModel)
        {
            if (viewModel == null || viewModel.DataTable == null || viewModel.SingleEntryData == null)
            {
                return BadRequest("Invalid data.");
            }

            // Convert data into DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("SubjectId", typeof(int));
            dt.Columns.Add("SubjectName", typeof(string));  // Add SubjectName column
            dt.Columns.Add("StudentClass", typeof(string));
            dt.Columns.Add("StudentMarks", typeof(decimal));

            foreach (var row in viewModel.DataTable)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["SubjectId"] = row.SubjectId;
                dataRow["SubjectName"] = row.SubjectName;  // Assign SubjectName
                dataRow["StudentClass"] = row.StudentClass;
                dataRow["StudentMarks"] = row.StudentMarks;
                dt.Rows.Add(dataRow);
            }
            DataSet ds = new DataSet();
            ds = _dbOperations.ByProcedure("USP_InsertStdReg_Details", new string[] { "StudentRollNo", "StudentName", "StudentEmail", "StudentMobNo" },
                new string[] { viewModel.SingleEntryData.StudentRollNo, viewModel.SingleEntryData.StudentName, viewModel.SingleEntryData.StudentEmail, viewModel.SingleEntryData.StudentMobNo }, new string[] { "type_StudentRegDetails" }, new DataTable[] { dt });
            return Ok("Data saved successfully.");
        }
    }
}
