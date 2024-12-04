using Data;
using Entity.ViewModal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewCoreApp.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCoreApp.Controllers
{
    [ApiController]
    [Route("api/exam")]
    public class ExamController : Controller
    {
        private readonly UserMgMtContext _context; // Replace with your actual DbContext
        private readonly IHubContext<ExamHub> _hubContext;

        public ExamController(UserMgMtContext context, IHubContext<ExamHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        // Start exam session by fetching random questions
        [HttpGet("/exam/start/{sessionId}")]
        public IActionResult StartExam(int sessionId)
        {
            var session = _context.TblExamSessions.FirstOrDefault(s => s.ExamSessionId == sessionId);
            if (session == null || session.IsCompleted)
            {
                return NotFound("Invalid or completed session.");
            }

            // Randomize and fetch questions for the exam
            var questions = _context.TblQuestion
                .OrderBy(q => Guid.NewGuid()) // Randomize order
                .Take(10) // Example: Get 10 random questions
                .ToList();

            // Set the exam page model
            var model = new ExamSessionViewModal
            {
                SessionId = sessionId,
                TblQuestion = questions,
                TimeLimit = 600, // Example: 10 minutes
                StartTime = DateTime.Now
            };

            return View("Exam", model); // Return the Exam view with the model
        }

        // Submit answers for the exam
        [HttpPost("/exam/submit")]
        public async Task<IActionResult> SubmitAnswers(int sessionId, List<int> answers)
        {
            var session = _context.TblExamSessions.FirstOrDefault(s => s.ExamSessionId == sessionId);
            if (session == null || session.IsCompleted)
            {
                return BadRequest("Invalid session.");
            }

            // Calculate score and mark session as completed
            session.IsCompleted = true;
            session.EndTime = DateTime.Now;
            _context.SaveChanges();

            // You can also grade the answers here

            // Notify the user about submission completion
            return Ok(new { Message = "Exam submitted successfully!" });
        }

        // Report user activity (used by SignalR)
        [HttpPost("/exam/report-activity")]
        public async Task<IActionResult> ReportActivity(int sessionId, string eventType)
        {
            // Send activity updates to the clients
            await _hubContext.Clients.All.SendAsync("ReceiveActivity", sessionId, eventType);
            return Ok();
        }
    }
}
