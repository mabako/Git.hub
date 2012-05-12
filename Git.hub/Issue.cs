using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Git.hub
{
    public class Issue
    {
        public int Number;

        internal RestClient _client;
        public Repository Repository { get; internal set; }


        public List<IssueComment> GetComments()
        {
            var request = new RestRequest("/repos/{user}/{repo}/issues/{pull}/comments");
            request.AddUrlSegment("user", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("pull", Number.ToString());

            return _client.Get<List<IssueComment>>(request).Data;
        }
    }

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
