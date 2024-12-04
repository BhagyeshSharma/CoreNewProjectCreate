using ClassDAL;
using Data;
using Entity.Modal;
using Entity.ViewModal;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace NewCoreApp.Controllers
{
    public class DivisionDistrictBlockController : Controller
    {
        private readonly UserMgMtContext _context; // Replace with your actual DbContext
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dbOperations;
        public DivisionDistrictBlockController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbOperations = dbRepository;
        }
        // GET: Create View (For cascading dropdowns and file upload)
        public IActionResult Create()
        {
            var divisions = _context.tblDivision
                .Select(d => new SelectListItem { Value = d.DivisionId.ToString(), Text = d.DivisionName })
                .ToList();

            var model = new UploadViewModel
            {
                Divisions = divisions
            };

            return View(model);
        }

        // GET: Districts based on selected Division
        public IActionResult GetDistricts(int divisionId)
        {
            var districts = _context.tblDistrict.Where(d => d.DivisionId == divisionId)
                                         .Select(d => new { value = d.DistrictId, text = d.DistrictName })
                                         .ToList();
            return Json(districts);
        }

        // GET: Blocks based on selected District
        public IActionResult GetBlocks(int districtId)
        {
            var blocks = _context.tblBlock.Where(d => d.DistrictId == districtId)
                                         .Select(d => new { value = d.BlockId, text = d.BlockName })
                                         .ToList();
            return Json(blocks);
        }

        // POST: Submit form with selected Division, District, Block, and uploaded file
        [HttpPost]
        public async Task<IActionResult> Create(UploadViewModel model)
        {
            string fileName = "";
            string filePath = "";
            //if (ModelState.IsValid)
            //{
            // Handle the uploaded file
            if (model.UploadedFile != null && model.UploadedFile.Length > 0)
            {
                // Define a custom folder path outside wwwroot
                var customFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadUserFiles");

                // Ensure the directory exists
                if (!Directory.Exists(customFolder))
                {
                    Directory.CreateDirectory(customFolder); // Create the folder if it doesn't exist
                }
                // Get the file name
                fileName = Path.GetFileName(model.UploadedFile.FileName);

                // Combine the custom folder path and file name
                filePath = Path.Combine(customFolder, fileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UploadedFile.CopyToAsync(stream);
                }
                Console.WriteLine($"File saved at: {filePath}");
            }
            else
            {
                Console.WriteLine("No file uploaded.");
            }
            // Save the data into the database
            var Trn_DivisonDistrictBlock = new Trn_DivisonDistrictBlock
            {
                DivisionId = model.DivisionId,
                DistrictId = model.DistrictId,
                BlockId = model.BlockId,
                UserUploadfile = fileName
                //FileName = fileName,
                //FilePath = filePath,
                //UploadedDate = DateTime.Now
            };

            _context.Trn_DivisonDistrictBlocks.Add(Trn_DivisonDistrictBlock);
            await _context.SaveChangesAsync();
            var divisions = _context.tblDivision
        .Select(d => new SelectListItem { Value = d.DivisionId.ToString(), Text = d.DivisionName })
        .ToList();
            // Populate the model again for returning the view
            var updatedModel = new UploadViewModel
            {
                Divisions = divisions
            };
            return View(updatedModel);
        }
        public IActionResult FileGrid()
        {
            string manualPath = "~/uploaduserfiles/";
            var data = (from file in _context.Trn_DivisonDistrictBlocks
                        join div in _context.tblDivision on file.DivisionId equals div.DivisionId
                        join dist in _context.tblDistrict on file.DistrictId equals dist.DistrictId
                        join blk in _context.tblBlock on file.BlockId equals blk.BlockId
                        select new UserFilegridViewModal
                        {
                            Id = file.ID,
                            FileName = manualPath + file.UserUploadfile,
                            DivisionName = div.DivisionName,
                            DistrictName = dist.DistrictName,
                            BlockName = blk.BlockName
                        }).ToList();

            return View(data); // Pass the data to the view
        }
    }
}
