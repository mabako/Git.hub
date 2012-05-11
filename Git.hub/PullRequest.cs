using System;

namespace Git.hub
{
    public class PullRequestInfo
    {
        public User User { get; private set; }
        public Repository Repo { get; private set; }
        public string Ref { get; private set; }
        public string Sha { get; private set; }
    }

    /// <summary>
    /// GitHub pull request
    /// </summary>
    public class PullRequest
    {
        /// <summary>
        /// ID of the pull request
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Title, i.e. what is shown in the list of pull requests
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Message of the original creator of the pull request
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// When was it created?
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// When was it updated?
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Internally used URL for this pull request.
        /// 
        /// https://api.github.com/{user}/{repo}/pulls/{id}
        /// </summary>
        public string Url { get; private set; }

        public PullRequestInfo Base { get; private set; }
        public PullRequestInfo Head { get; private set; }

        public User User { get; private set; }

        /*
        /// <summary>
        /// User who merged this pull request.
        /// 
        /// Only set in single pull requests, not lists of.
        /// </summary>
        public User MergedBy { get; private set; }
        */
    }
}
