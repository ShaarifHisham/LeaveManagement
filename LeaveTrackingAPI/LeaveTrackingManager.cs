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
using System.Threading.Tasks;

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

        public IActionResult CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            var validate = leaveManagementContext.Employees.FirstOrDefault(x => x.EmployeeId == leaveRequest.EmployeeId);
            if (validate != null)
            {
                leaveManagementContext.LeaveReports.Add(leaveRequest);
                leaveManagementContext.SaveChanges();
            }
            return null;
        }
    }
}
