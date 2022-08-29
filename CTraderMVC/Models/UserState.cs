namespace CTraderMVC.Models
{
    public sealed class UserState
    {
        private static UserState userState = null;

        public bool isLoggedIn = false;
        public string? userName = null;

        private UserState()
        {
            this.isLoggedIn = false;
            this.userName = null;
        }

        private UserState(string userName)
        {
            this.isLoggedIn = true;
            this.userName = userName;
        }

        public static UserState PostUserState(string userName)
        {
            if (userState is null && !string.IsNullOrEmpty(userName))
            {
                userState = new UserState(userName);
            }
            else
            {
                userState = new UserState();
            }

            return userState;
        }

        public static UserState GetUserState()
        {
            return userState;
        }
    }
}
