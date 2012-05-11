using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.hub
{
    public class Organization
    {
        public string Login { get; internal set; }

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
