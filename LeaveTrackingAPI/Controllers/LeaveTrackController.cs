using LeaveTrack.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeaveTrack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTrackController : Controller
    {
        private readonly ILeaveTrackingManager leaveTrackManager;

        public LeaveTrackController(ILeaveTrackingManager leaveTrackManager)
        {
            this.leaveTrackManager = leaveTrackManager;
        }

        /// <summary>
        /// Creates a leave request
        /// </summary>
        /// <param name="leaveReport">Leave Report</param>
        /// <response code="200">created Leave Request.</response>
        /// <response code="400">Request does not have the required values.</response>
        /// <response code="401">Access token is missing or invalid.</response>
        /// <response code="403">Action not permitted.</response>
        [HttpPost("leaveRequest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        public IActionResult CreateLeaveRequest([FromBody][Required] EmployeeLeaveRequest leaveReport)
        {
            try
            {
                leaveTrackManager.CreateLeaveRequest(leaveReport);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Retrieves list of leave request.
        /// </summary>
        /// <param name="managerId">Manager ID</param>
        /// <response code="200">list of leave Request.</response>
        /// <response code="404">Not found.</response>
        /// <response code="403">Action not permitted for specified API key.</response>
        /// <response code="401">Access token is missing or invalid.</response>
        [HttpGet("leaveReport/{managerId}")]
        [ProducesResponseType(typeof(IEnumerable<LeaveReportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public IActionResult GetLeavesList([FromRoute][Required] int managerId)
        {
            var leave = leaveTrackManager.GetLeavesList(managerId);

            if (leave == null)
                return this.NotFound();
            
            return this.Ok(leave);
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
    }
}
