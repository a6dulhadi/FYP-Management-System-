namespace VP_PBE.Services
{
    public static class SessionManager
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserRole { get; set; }
        public static string CurrentUsername { get; set; }

        public static void SetSession(int userId, string role, string username)
        {
            CurrentUserId = userId;
            CurrentUserRole = role;
            CurrentUsername = username;
        }

        public static void ClearSession()
        {
            CurrentUserId = 0;
            CurrentUserRole = "";
            CurrentUsername = "";
        }

        public static bool IsLoggedIn()
        {
            return CurrentUserId > 0;
        }
    }
}