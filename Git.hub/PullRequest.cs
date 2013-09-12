using System;
using RestSharp;
using System.Collections.Generic;

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
        /// Repository to which this pull request belongs.
        /// </summary>
        public Repository Repository { get; internal set; }

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
        /// URL for the unified diff file
        /// </summary>
        public string DiffUrl { get; private set; }

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

        internal RestClient _client;

        /// <summary>
        /// Retrieves all Commits associated with this pull request.
        /// </summary>
        /// <returns></returns>
        public List<PullRequestCommit> GetCommits()
        {
            var request = new RestRequest("repos/{user}/{repo}/pulls/{pull}/commits");
            request.AddUrlSegment("user", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("pull", Number.ToString());

            return _client.GetList<PullRequestCommit>(request);
        }

        public Issue ToIssue()
        {
            return new Issue { _client = _client, Repository = Repository, Number = Number };
        }

        public List<IssueComment> GetIssueComments()
        {
            return ToIssue().GetComments();
        }

        public bool Open()
        {
            return UpdateState("open");
        }

        public bool Close()
        {
            return UpdateState("closed");
        }

        private bool UpdateState(string state)
        {
            var request = new RestRequest("repos/{user}/{repo}/pulls/{pull}");
            request.AddUrlSegment("user", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("pull", Number.ToString());

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                state = state
            });
            return _client.Patch(request).StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
