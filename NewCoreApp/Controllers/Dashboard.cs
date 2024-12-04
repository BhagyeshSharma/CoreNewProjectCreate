using ClassDAL;
using Data;
using Entity.ViewModal;
using InfraStucture.Contract;
using InfraStucture.Repository;
using Microsoft.AspNetCore.Mvc;

namespace NewCoreApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dboperations;
        public DashboardController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dboperations = dbRepository;
        }
        public IActionResult Index()
        {
            // Create a list of cards with dynamic data (you can replace this with data from DB or API)
            List<DashboardData> dashboardData = new List<DashboardData>
        {
            new DashboardData { Title = "Total Users", Value = 1000, Icon = "bi bi-person", BackgroundColor = "bg-primary",AdditionalDetails="basic information about a user that is collected for various purposes, including: subscription management, activity logging, technical support, and communications" },
            new DashboardData { Title = "Total Sales", Value = 4500, Icon = "bi bi-bag", BackgroundColor = "bg-success",AdditionalDetails="A category of business intelligence that includes information about the sales process. Sales data can be used to improve sales performance, understand customers, and make decisions. Some examples of sales data include" },
            new DashboardData { Title = "Total Revenue", Value = 80000, Icon = "bi bi-currency-dollar", BackgroundColor = "bg-warning",AdditionalDetails="Revenue-based financing is a way that firms can raise capital by pledging a percentage of future ongoing revenues in exchange for money invested" },
            new DashboardData { Title = "Pending Orders", Value = 23, Icon = "bi bi-clock", BackgroundColor = "bg-danger",AdditionalDetails="Pending details refer to information that is not yet decided or settled, or is imminent or impending. Here are some examples of pending details" }
        };

            return View(dashboardData);
        }
    }
}
