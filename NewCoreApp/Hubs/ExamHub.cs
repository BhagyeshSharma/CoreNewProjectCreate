using Entity.Modal;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace NewCoreApp.Hubs
{
    public class ExamHub : Hub
    {
        // Method to report user activity (e.g., mouse movement, time spent on the page)
        public async Task ReportActivity(int sessionId, string eventType)
        {
            // Log the activity (you can insert this into your database if needed)
            var log = new TblUserActivityLogs
            {
                SessionId = sessionId,
                EventType = eventType,
                Timestamp = DateTime.Now
            };

            // Send activity log to all connected clients (for real-time updates)
            await Clients.All.SendAsync("ReceiveActivity", sessionId, eventType);
        }
    }
}
