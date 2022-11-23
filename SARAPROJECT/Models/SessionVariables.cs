namespace SARAPROJECT.Models
{
    public class SessionVariables
    {
        public const string SessionKeyUserName = "SessionKeyUserName";
        public const string SessionKeySessionId = "SessionKeySessionId";

        public enum SessionKeyEnum
        {
            SessionKeyUserName=0,
            SessionKeySessionId=1
        }
    }
}
