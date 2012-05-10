namespace Git.hub
{
    public class User
    {
        /// <summary>
        /// The GitHub username
        /// </summary>
        public string Login { get; private set; }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() + Login.GetHashCode();
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
