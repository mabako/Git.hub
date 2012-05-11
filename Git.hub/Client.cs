using System;
using System.Collections.Generic;
using RestSharp;

namespace Git.hub
{
    /// <summary>
    /// Git.hub client, start here.
    /// </summary>
    public class Client
    {
        private RestClient client = new RestClient("https://api.github.com");

        /// <summary>
        /// Sets the client to use username and password with GitHub
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="password">password</param>
        public void setCredentials(string user, string password)
        {
            if (user != null && password != null)
                client.Authenticator = new HttpBasicAuthenticator(user, password);
            else
                client.Authenticator = null;
        }

        /// <summary>
        /// Sets the client to use oauth2 with GitHub
        /// </summary>
        /// <param name="token">oauth2-token</param>
        public void setOAuth2Token(string token)
        {
            if (token != null)
                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token);
            else
                client.Authenticator = null;
        }

        /// <summary>
        /// Lists all repositories for the logged in user
        /// </summary>
        /// <returns>list of repositories</returns>
        public IList<Repository> getRepositories()
        {
            if (client.Authenticator == null)
                throw new ArgumentException("no authentication details");

            var request = new RestRequest("/user/repos?type=all");

            var list = client.Get<List<Repository>>(request).Data;
            list.ForEach(r => r._client = client);
            return list;
        }

        /// <summary>
        /// Lists all repositories for a particular user
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>list of repositories</returns>
        public IList<Repository> getRepositories(string username)
        {
            var request = new RestRequest("/users/{name}/repos");
            request.AddUrlSegment("name", username);

            var list = client.Get<List<Repository>>(request).Data;
            list.ForEach(r => r._client = client);
            return list;
        }

        /// <summary>
        /// Fetches a single repository from github.com/username/repositoryName.
        /// </summary>
        /// <param name="username">repository owner</param>
        /// <param name="repositoryName">name of the repository</param>
        /// <returns>fetched repository</returns>
        public Repository getRepository(string username, string repositoryName)
        {
            var request = new RestRequest("/repos/{name}/{repo}");
            request.AddUrlSegment("name", username);
            request.AddUrlSegment("repo", repositoryName);

            var repo = client.Get<Repository>(request).Data;
            if (repo == null)
                return null;

            repo._client = client;
            repo.Detailed = true;
            return repo;
        }

        /// <summary>
        /// Fetches all repositories of an organization
        /// </summary>
        /// <param name="organization">name of the organization</param>
        /// <returns></returns>
        public IList<Repository> getOrganizationRepositories(string organization)
        {
            var request = new RestRequest("/orgs/{org}/repos");
            request.AddUrlSegment("org", organization);

            var list = client.Get<List<Repository>>(request).Data;
            list.ForEach(r => r._client = client);
            return list;
        }
    }
}
