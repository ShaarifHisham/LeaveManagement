using LeaveTrack.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LeaveTrack
{
    public interface ILeaveTrackingManager
    {
        string Authenticate(string username, string password);

        IActionResult CreateLeaveRequest(EmployeeLeaveRequest leaveRequest);

        IEnumerable<LeaveReportResponse> GetLeavesList(int managerId);
    }
}
