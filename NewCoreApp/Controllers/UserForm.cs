using ClassDAL;
using Data;
using Data.Migrations;
using Entity.Modal;
using Entity.ViewModal;
using InfraStucture.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace NewCoreApp.Controllers
{
    public class UserFormController : Controller
    {
        private readonly UserMgMtContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly dbRepository _dboperations;
        private readonly IWebHostEnvironment _environment;
        public UserFormController(UserMgMtContext context, IUnitOfWork unitOfWork, dbRepository dbRepository, IWebHostEnvironment environment)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _dboperations = dbRepository;
            _environment = environment; 
        }
        public IActionResult Index()
        {
            var model = new UserFormModel
            {
                UserAddressModel = new UserAddressModel(),
                UserContactModel = new UserContactModel(),
                Userlist = GetUserListFromDatabase() // Fetch dynamic data for the grid
            };
            return View(model);
        }
        // Method to fetch data from database
        private List<UserFormModel> GetUserListFromDatabase()
        {
            // Replace this with your actual database call
            // Example using Entity Framework
            using (var context = new UserMgMtContext())
            {
                return context.Tbl_User
                    .Select(u => new UserFormModel
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        UserEmail = u.UserEmail,
                        UserAddressModel = new UserAddressModel
                        {
                            Street = u.UserStreet,
                            City = u.UserCity,
                            ZipCode = u.ZipCode
                        },
                        UserContactModel = new UserContactModel
                        {
                            Phone = u.Phone,
                            EmergencyContact = u.EmergencyContact
                        }
                    }).ToList();
            }
        }
        // Handle form submission
        [HttpPost]
        public IActionResult SubmitForm(UserFormModel model)
        {
            //if (ModelState.IsValid)
            //{
            var user = new Entity.Modal.Tbl_User
            {
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                UserStreet = model.UserAddressModel.Street,
                UserCity = model.UserAddressModel.City,
                ZipCode = model.UserAddressModel.ZipCode,
                Phone = model.UserContactModel.Phone,
                EmergencyContact = model.UserContactModel.EmergencyContact
            };
            _context.Tbl_User.Add(user);
            _context.SaveChanges();
            // Send structured registration email
            SendRegistrationEmailWithInlineImage(user);
            return RedirectToAction("Index", model);
            //}
            //return View("Index", model);
        }
        private void SendRegistrationEmailWithInlineImage(Entity.Modal.Tbl_User user)
        {
            var fromAddress = new MailAddress("admin@sfatechnologies.com", "Your App Name");
            var toAddress = new MailAddress(user.UserEmail, user.UserName);

            const string subject = "Registration Successful - Welcome to Our Platform!";

            // HTML content for the email
            string body = $@"
    <html>
    <head>
        <style>
            .email-header {{
                background-color: #4CAF50;
                color: white;
                text-align: center;
                padding: 10px 0;
                font-size: 24px;
            }}
            .email-body {{
                font-family: Arial, sans-serif;
                margin: 20px;
            }}
            .email-footer {{
                margin-top: 20px;
                text-align: center;
                font-size: 14px;
                color: gray;
            }}
            .image {{
                width: 200px;
                height: auto;
            }}
        </style>
    </head>
    <body>
        <div class='email-header'>Welcome, {user.UserName}!</div>
        <div class='email-body'>
            <p>Thank you for registering on our platform. Here are your registration details:</p>
            <ul>
                <li><strong>Name:</strong> {user.UserName}</li>
                <li><strong>Email:</strong> {user.UserEmail}</li>
                <li><strong>Address:</strong> {user.UserStreet}, {user.UserCity}, {user.ZipCode}</li>
                <li><strong>Phone:</strong> {user.Phone}</li>
                <li><strong>Emergency Contact:</strong> {user.EmergencyContact}</li>
            </ul>
            <p>If you have any questions, feel free to contact us.</p>
            <img src='cid:myImage' class='image'/>
        </div>
        <div class='email-footer'>Thank you for choosing us!</div>
    </body>
    </html>";

            var smtp = new SmtpClient("mail.smtp2go.com", 587)
            {
                Credentials = new NetworkCredential("admin@sfatechnologies.com", "c7adBEvpYKPMo6qj"),
                EnableSsl = true
            };

            // Absolute path to the image
            var imagePath = Path.Combine(_environment.WebRootPath, "uploaduserfiles", "iwtxaj9d_400x400-removebg-preview.png");

            // Send email with inline image
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                // Inline image attachment
                var inlineImage = new LinkedResource(imagePath)
                {
                    ContentId = "myImage", // This ID will be used in the HTML body
                    TransferEncoding = TransferEncoding.Base64
                };

                var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                htmlView.LinkedResources.Add(inlineImage);

                message.AlternateViews.Add(htmlView);

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Email sending failed: {ex.Message}");
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var user = _context.Tbl_User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserFormModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserAddressModel = new UserAddressModel
                {
                    Street = user.UserStreet,
                    City = user.UserCity,
                    ZipCode = user.ZipCode
                },
                UserContactModel = new UserContactModel
                {
                    Phone = user.Phone,
                    EmergencyContact = user.EmergencyContact
                }
            };

            return View("Edit", model); // Redirect to edit view
        }

        [HttpPost]
        public IActionResult Update(UserFormModel model)
        {
            var user = _context.Tbl_User.Find(model.UserId);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.UserEmail = model.UserEmail;
                user.UserStreet = model.UserAddressModel.Street;
                user.UserCity = model.UserAddressModel.City;
                user.ZipCode = model.UserAddressModel.ZipCode;
                user.Phone = model.UserContactModel.Phone;
                user.EmergencyContact = model.UserContactModel.EmergencyContact;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _context.Tbl_User.Find(id);
            if (user != null)
            {
                _context.Tbl_User.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
