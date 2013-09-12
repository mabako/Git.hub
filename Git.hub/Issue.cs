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
            var request = new RestRequest("/repos/{user}/{repo}/issues/{issue}/comments");
            request.AddUrlSegment("user", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("issue", Number.ToString());

            return _client.GetList<IssueComment>(request);
        }

        public IssueComment CreateComment(string body)
        {
            if (_client.Authenticator == null)
                throw new ArgumentException("no authentication details");

            var request = new RestRequest("/repos/{user}/{repo}/issues/{issue}/comments");
            request.AddUrlSegment("user", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("issue", Number.ToString());

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new {
                body = body
            });
            return _client.Post<IssueComment>(request).Data;
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
