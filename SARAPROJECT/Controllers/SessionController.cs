using Microsoft.AspNetCore.Mvc;
using SARAPROJECT.Models;

namespace SARAPROJECT.Controllers
{
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable <String> GetSessionInfo()
        {
            List<String> sessionInfo = new List<String>();
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVariables.SessionKeyUserName))){
                HttpContext.Session.SetString(SessionVariables.SessionKeyUserName, "Current User");
                HttpContext.Session.SetString(SessionVariables.SessionKeySessionId,Guid.NewGuid().ToString());
            }
            var userName = HttpContext.Session.GetString(SessionVariables.SessionKeyUserName);
            var SessionId = HttpContext.Session.GetString(SessionVariables.SessionKeySessionId);
            sessionInfo.Add(userName);
            sessionInfo.Add(SessionId);
            return sessionInfo;
        }
    }
}
/*Esta clase no es necesaria*/