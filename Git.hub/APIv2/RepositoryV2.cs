using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Git.hub.APIv2
{
    internal class RepositoryV2
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Homepage { get; private set; }

        // not in v3 api
        public string Owner { get; private set; }

        public bool Fork { get; private set; }
        public int Forks { get; private set; }
        public bool Private { get; private set; }
        public Repository ToV3(RestClient client)
        {
            return new Repository
            {
                Name = Name,
                Description = Description,
                Homepage = Homepage,
                Owner = new User
                {
                    Login = Owner
                },
                Fork = Fork,
                Forks = Forks,
                Private = Private,

                GitUrl = string.Format("git://github.com/{0}/{1}.git", Owner, Name),
                SshUrl = string.Format("git@github.com:{0}/{1}.git", Owner, Name),
                CloneUrl = string.Format("https://github.com/{0}/{1}.git", Owner, Name),
                Detailed = false,
                _client = client
            };
        }
    }

    internal class RepositoryListV2
    {
        public List<RepositoryV2> Repositories { get; private set; }
    }
}
