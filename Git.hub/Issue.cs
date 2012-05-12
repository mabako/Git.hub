using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.hub
{
    public class IssueComment
    {
        public int Id { get; private set; }
        public string Body { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public User User { get; private set; }

        /// <summary>
        /// api.github.com/repos/{user}/{repo}/issues/{issue}/comments/{id}
        /// </summary>
        public string Url { get; private set; }
    }
}
