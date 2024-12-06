using ClassDAL;
using Data;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NewCoreApp.Controllers
{
    [Route("[controller]/[action]")]
    public class GridDrilldownController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dboperations;
        private readonly IWebHostEnvironment _environment;
        public GridDrilldownController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository, IWebHostEnvironment environment, SchoolEducationDBContext sEDContext)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dboperations = dbRepository;
            _environment = environment;
        }
        public IActionResult GetDivisions()
        {
            var divisions = _context.tblDivision.ToList();
            return View(divisions);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(int divisionId)
        {
            var districts = await _context.tblDistrict
                .Where(d => d.DivisionId == divisionId)
                .ToListAsync();
            return Json(districts);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlocks(int districtId)
        {
            var blocks = await _context.tblBlock
                .Where(b => b.DistrictId == districtId)
                .ToListAsync();
            return Json(blocks);
        }
    }
}


