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
        public User Author { get; private set; }
        public User Committer { get; private set; }
        public List<Ref> Parents { get; private set; }
        public Commit Commit { get; private set; }
    }

    public class CommitAuthor
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Date { get; private set; }
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
