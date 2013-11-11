using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Git.hub
{
    public class GitHubTree
    {
        private RestClient _Client;
        internal RestClient Client
        {
            get
            {
                return _Client;
            }

            set
            {
                _Client = value;
                foreach (var entry in Tree)
                    entry._client = _Client;
            }
        }
        
        private Repository _Repository;
        public Repository Repository
        {
            get
            {
                return _Repository;
            }

            internal set
            {
                _Repository = value;
                foreach (var entry in Tree)
                    entry.Repository = _Repository;
            }
        }

        public string Url { get; private set; }
        public string Sha { get; private set; }
        public List<GitHubTreeEntry> Tree { get; set; }

    }

    public class GitHubBlob
    { 
        public string Content { get; set; }
        public string Encoding { get; set; }
        public string Sha { get; set; }
        public Int64 Size { get; set; }

        public string GetContent()
        {
            if (Encoding.Equals("Base64", StringComparison.InvariantCultureIgnoreCase))
            {
                byte[] data = Convert.FromBase64String(Content);
                string decodedString = System.Text.Encoding.UTF8.GetString(data);
                return decodedString;
            }
            else
            {
                try
                {
                    Encoding fromEncoding = System.Text.Encoding.GetEncoding(Encoding);
                    if (fromEncoding == null)
                        return Content;

                    byte[] bytes = fromEncoding.GetBytes(Content);
                    return System.Text.Encoding.UTF8.GetString(bytes);
                }
                catch(Exception)
                {
                    return Content;
                }
            }
        }
    }

    public class GitHubTreeEntry
    {
        internal RestClient _client;
        public Repository Repository { get; internal set; }

        public string Path { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }
        public string Url { get; private set; }
        public string Sha { get; private set; }

        public readonly Lazy<GitHubBlob> Blob;

        public GitHubTreeEntry()
        {
            Blob = new Lazy<GitHubBlob>(() =>
               {
                   var request = new RestRequest("/repos/{owner}/{repo}/git/blobs/{sha}");
                   request.AddUrlSegment("owner", Repository.Owner.Login);
                   request.AddUrlSegment("repo", Repository.Name);
                   request.AddUrlSegment("sha", Sha);

                   var ghBlob = _client.Get<GitHubBlob>(request).Data;
                   if (ghBlob == null)
                       return null;

                   return ghBlob;
               });
        }

    }

    public class GitHubReference
    {
        internal RestClient _client;
        public Repository Repository { get; internal set; }


        public string Ref { get; set; }
        public string URL { get; set; }
        public RefObject Object { get; set; }

        public class RefObject
        {
            public string Type { get; set; }
            public string Sha { get; set; }
            public string URL { get; set; }
        }

        public Commit GetCommit()
        {
            var request = new RestRequest("/repos/{owner}/{repo}/git/commits/{sha}");
            request.AddUrlSegment("owner", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            request.AddUrlSegment("sha", Object.Sha);

            var ghCommit = _client.Get<Commit>(request).Data;
            if (ghCommit == null)
                return null;

            ghCommit._client = _client;
            ghCommit.Repository = Repository;
            return ghCommit;
        }

        public GitHubTree GetTree()
        {
            var request = new RestRequest("/repos/{owner}/{repo}/git/trees/{sha}");
            request.AddUrlSegment("owner", Repository.Owner.Login);
            request.AddUrlSegment("repo", Repository.Name);
            var commit = GetCommit();
            request.AddUrlSegment("sha", commit.Tree.Sha);

            var ghTree = _client.Get<GitHubTree>(request).Data;
            if (ghTree == null)
                return null;

            ghTree.Client = _client;
            ghTree.Repository = Repository;
            return ghTree;
        }
    }

}
