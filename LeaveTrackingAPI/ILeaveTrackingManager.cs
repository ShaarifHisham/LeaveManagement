using LeaveTrack.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveTrack
{
    public interface ILeaveTrackingManager
    {
        string Authenticate(string username, string password);
        IActionResult CreateLeaveRequest(LeaveRequest leaveRequest);
    }
}
