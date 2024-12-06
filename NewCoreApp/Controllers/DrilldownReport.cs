using ClassDAL;
using Data;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NewCoreApp.Controllers
{
    public class DrilldownReportController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dboperations;
        private readonly IWebHostEnvironment _environment;
        public DrilldownReportController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository, IWebHostEnvironment environment, SchoolEducationDBContext sEDContext)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dboperations = dbRepository;
            _environment = environment;
        }
        // Main action to display departments
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Tbl_Department
                .Include(d => d.Teams)
                    .ThenInclude(t => t.Employees)
                .ToListAsync();

            return View(departments);
        }

        // Get teams by department
        public async Task<IActionResult> GetTeams(int departmentId)
        {
            var teams = await _context.Tbl_Team
                .Where(t => t.DepartmentId == departmentId)
                .Select(t => new
                {
                    id = t.TeamId,
                    name = t.TeamName
                })
                .ToListAsync();

            return Json(teams);
        }

        // Get employees by team
        public async Task<IActionResult> GetEmployees(int teamId)
        {
            var employees = await _context.Tbl_Employee
                .Where(e => e.TeamId == teamId)
                .Select(e => new
                {
                    id = e.EmployeeID,
                    name = e.EmployeeName
                })
                .ToListAsync();

            return Json(employees);
        }
    }
}
