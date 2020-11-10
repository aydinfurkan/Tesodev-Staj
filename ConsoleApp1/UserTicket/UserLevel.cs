namespace ConsoleApp3
{
    public abstract class UserLevel
    {
        public string State;
        public string Description;

        protected UserLevel(string state)
        {
            State = state;
            if (state == "Admin")
                Description = "Administrator";
            if (state == "User")
                Description = "UserDescription";
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                UserLevel o = (UserLevel) obj;
                return State == o.State;
            }

            return false;
        }
    }
}