using LeaveTrack.Model;
using LeaveTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LeaveTrack
{
    public class LeaveTrackingManager : ILeaveTrackingManager
    {
        private readonly IConfiguration _configuration;
        private readonly LeaveManagementContext leaveManagementContext;
        public LeaveTrackingManager(IConfiguration configuration, LeaveManagementContext leaveManagementContext)
        {
            this._configuration = configuration;
            this.leaveManagementContext = leaveManagementContext;
        }

        public string Authenticate(string username, string password)
        {
            var credential = leaveManagementContext.Employees.FirstOrDefault(x => x.UserName == username && x.Password == password && x.IsDeleted != true);
            if (credential != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.ASCII.GetBytes(_configuration["TokenKey"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                           new Claim(ClaimTypes.Name, username),
                           new Claim("role","Manager")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenkey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return null;
            }
        }

        public IActionResult CreateLeaveRequest(EmployeeLeaveRequest leaveRequest)
        {
            var validate = leaveManagementContext.Employees.FirstOrDefault(x => x.EmployeeId == leaveRequest.EmployeeId && x.IsDeleted != true);
            if (validate != null)
            {
                var leave = new LeaveRequest
                {
                    EmployeeId = leaveRequest.EmployeeId,
                    ApproverId = leaveRequest.ApproverId,
                    FromDate = leaveRequest.FromDate,
                    ToDate = leaveRequest.ToDate,
                    Reason = leaveRequest.Reason ?? null
                };
                leaveManagementContext.LeaveReports.Add(leave);
                leaveManagementContext.SaveChanges();
            }
            return null;
        }

        public IEnumerable<LeaveReportResponse> GetLeavesList(int managerId)
        {
            var leaveRequests = leaveManagementContext.LeaveReports.Where(x => x.ApproverId == managerId);
            var leaveResponse = new List<LeaveReportResponse>();
            if (leaveRequests.Any())
            {
                foreach (var leave in leaveRequests)
                {
                    leaveResponse.Add(new LeaveReportResponse()
                    {
                        EmployeeId = leave.EmployeeId,
                        FromDate = leave.FromDate,
                        ToDate = leave.ToDate,
                        Reason = leave.Reason,
                    });
                }
                return leaveResponse;
            }
            return null;
        }
    }
}
