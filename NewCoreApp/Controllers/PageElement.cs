using ClassDAL;
using Data;
using Data.Migrations;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NewCoreApp.Controllers
{
    public class PageElementController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dbOperations;
        public PageElementController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dbOperations = dbRepository;
        }
        // GET: Page/AddElement
        public IActionResult AddElement(int pageId)
        {
            ViewBag.PageId = pageId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddElement(int pageId, string elementType, string[] options)
        {
            if (ModelState.IsValid)
            {
                // Debugging: Check options array
                if (options != null && options.Any())
                {
                    foreach (var option in options)
                    {
                        Console.WriteLine(option); // Or use Debug.WriteLine(option);
                    }
                }

                var pageElement = new Entity.Modal.Tbl_PageElements
                {
                    PageId = pageId,
                    ElementType = elementType
                };

                // If the element is a Dropdown, RadioButton, or Checkbox, save options
                if (elementType == "Dropdown" || elementType == "RadioButton" || elementType == "Checkbox")
                {
                    pageElement.Content = JsonConvert.SerializeObject(options);
                }
                else if (elementType == "Textbox")
                {
                    pageElement.Content = options?.FirstOrDefault(); // If options is null, get the first one.
                }

                _context.Tbl_PageElements.Add(pageElement);
                _context.SaveChanges();

                return RedirectToAction("Edit", new { pageId });
            }

            return View();
        }

        // GET: Page/Edit
        public IActionResult Edit(int pageId)
        {
            var pageElements = _context.Tbl_PageElements.Where(e => e.PageId == pageId).ToList();
            return View(pageElements);
        }
    }
}
