using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.hub
{
    public class Ref
    {
        public string Url { get; private set; }
        public string Sha { get; private set; }
    }

    public class PullRequestCommit
    {
        public string Sha { get; private set; }
        public string Url { get; private set; }

        /// <summary>
        /// Github User, may be null if unbeknown to Github
        /// </summary>
        public User Author { get; private set; }
        public User Committer { get; private set; }
        public List<Ref> Parents { get; private set; }
        public Commit Commit { get; private set; }

        public string AuthorName { get { return Author == null ? Commit.Author.ToString() : Author.Login; } }
    }

    public class CommitAuthor
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Date { get; private set; }

        public override string ToString()
        {
            if (Name != null)
            {
                if (Email != null)
                    return string.Format("{0} <{1}>", Name, Email);
                else
                    return Name;
            }
            else if (Email != null)
                return Email;
            else
                return "";
        }
    }

    // Not too sure this is the same for normal commits.
    public class Commit
    {
        public string Url { get; private set; }
        public CommitAuthor Author { get; private set; }
        public CommitAuthor Committer { get; private set; }
        public string Message { get; private set; }
        public Ref Tree { get; private set; }
    }
}
