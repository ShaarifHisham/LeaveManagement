using LeaveTrack.Model;
using LeaveTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTrack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTrackController : Controller
    {
        private readonly ILeaveTrackingManager leaveTrackManager;
        private readonly LeaveManagementContext leaveManagementContext;

        public LeaveTrackController(ILeaveTrackingManager leaveTrackManager, LeaveManagementContext leaveManagementContext)
        {
            this.leaveTrackManager = leaveTrackManager;
            this.leaveManagementContext = leaveManagementContext;
        }

        /// <summary>
        /// Generates a new token
        /// </summary>
        /// <param name="userCred">User Credentials</param>
        /// <returns>Token</returns>
        /// <response code="201">Successfully created Token.</response>
        /// <response code="400">Request has missing/invalid values.</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        public IActionResult Authenticate([FromBody] UserCredRequest userCred)
        {
            var token = leaveTrackManager.Authenticate(userCred.UserName, userCred.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        /// <summary>
        /// Creates a leave request
        /// </summary>
        /// <param name="leaveReport">Leave Report</param>
        /// <response code="200">created Leave Request.</response>
        /// <response code="400">Request does not have the required values.</response>
        /// <response code="401">Access token is missing or invalid.</response>
        /// <response code="404">Empoyee Id not found.</response>
        [HttpPost("leaveRequest")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult CreateLeaveRequest([FromBody][Required] LeaveReport leaveReport)
        {
            var validate = leaveManagementContext.Employees.FirstOrDefault(x => x.EmployeeId == leaveReport.EmployeeId);
            if (validate != null)
              {
                 leaveManagementContext.LeaveReports.Add(leaveReport);
                 leaveManagementContext.SaveChanges();
              }
            else
              {
                 return NotFound();
              }           
            return Ok();
        }
    }
}
