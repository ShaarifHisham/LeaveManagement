using LeaveTrack.Models;
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
            var credential = leaveManagementContext.Authentications.FirstOrDefault(x => x.UserName == username && x.Password == password);
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
    }
}
