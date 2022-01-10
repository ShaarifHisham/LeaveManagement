using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTrack
{
    public interface ILeaveTrackingManager
    {
        string Authenticate(string username, string password);
    }
}
