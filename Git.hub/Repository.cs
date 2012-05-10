using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Git.hub
{
    public class Repository
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Homepage { get; private set; }
        public User Owner { get; private set; }
        public bool Fork { get; private set; }
        public int Forks { get; private set; }
        public bool Private { get; private set; }

        public Repository Parent { get; private set; }
        internal RestClient _client { private get; set; }
        
        /// <summary>
        /// Forks this repository into your own account.
        /// </summary>
        /// <returns></returns>
        public Repository CreateFork()
        {
            RestRequest request = new RestRequest("/repos/{user}/{repo}/forks", Method.POST);
            request.AddUrlSegment("user", Owner.Login);
            request.AddUrlSegment("repo", Name);

            Repository forked = _client.Execute<Repository>(request).Data;
            forked._client = _client;
            return forked;
        }

        /// <summary>
        /// Lists all branches
        /// </summary>
        /// <remarks>Not really sure if that's even useful, mind the 'git branch'</remarks>
        /// <returns>list of all branches</returns>
        public IList<Branch> GetBranches()
        {
            RestRequest request = new RestRequest("/repos/{user}/{repo}/branches", Method.GET);
            request.AddUrlSegment("user", Owner.Login);
            request.AddUrlSegment("repo", Name);

            return _client.Execute<List<Branch>>(request).Data;
        }

        /// <summary>
        /// Lists all open pull requests
        /// </summary>
        /// <returns>llist of all open pull requests</returns>
        public IList<PullRequest> GetPullRequests()
        {
            var request = new RestRequest("/repos/{user}/{repo}/pulls", Method.GET);
            request.AddUrlSegment("user", Owner.Login);
            request.AddUrlSegment("repo", Name);

            return _client.Execute<List<PullRequest>>(request).Data;
        }

        /// <summary>
        /// Returns a single pull request.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>the single pull request</returns>
        public PullRequest GetPullRequest(int id)
        {
            var request = new RestRequest("/repos/{user}/{repo}/pulls/{pull}", Method.GET);
            request.AddUrlSegment("user", Owner.Login);
            request.AddUrlSegment("repo", Name);
            request.AddUrlSegment("pull", id.ToString());

            return _client.Execute<PullRequest>(request).Data;
        }

#if _
        /// <summary>
        /// Creates a new pull request
        /// </summary>
        /// <param name="headBranch">branch in the own repository, like mabako:new-awesome-thing</param>
        /// <param name="baseBranch">branch it should be merged into in the original repository, like master</param>
        /// <param name="title">title of the request</param>
        /// <param name="body">body</param>
        /// <returns></returns>
        public PullRequest CreatePullRequest(string headBranch, string baseBranch, string title, string body)
        {
            /*
            var request = new RestRequest("/repos/{name}/{repo}/pulls", Method.POST);
            request.AddUrlSegment("name", Owner.Login);
            request.AddUrlSegment("repo", Name);

            request.RequestFormat = DataFormat.Json;
            request.AddParameter("title", title);
            request.AddParameter("body", body);
            request.AddParameter("head", headBranch);
            request.AddParameter("base", baseBranch);

            var pullrequest = _client.Execute<PullRequest>(request);
            Console.WriteLine(pullrequest.Content);
            return pullrequest.Data;
            */
            return null;
        }
#endif

        public override bool Equals(object obj)
        {
            return obj is Repository && GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() + ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Owner.Login + "/" + Name;
        }
    }
}
