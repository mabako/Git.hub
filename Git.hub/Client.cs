﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;

namespace Git.hub
{
    /// <summary>
    /// Git.hub client, start here.
    /// </summary>
    public class Client
    {
        private RestClient client;

        /// <summary>
        /// Creates a new client instance for github.com
        /// </summary>
        public Client() : this("https://api.github.com") { }

        /// <summary>
        /// Creates a new client instance
        /// </summary>
        /// <param name="apiEndpoint">the host to connect to, e.g. 'https://api.github.com'</param>
        public Client(string apiEndpoint)
        {
            client = new RestClient(apiEndpoint);
            client.UserAgent = "mabako/Git.hub";
        }

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
                client.Authenticator = new OAuth2AuthHelper(token);
            else
                client.Authenticator = null;
        }

        /// <summary>
        /// Sets the client to use a proxy
        /// </summary>
        /// <param name="address">proxy address</param>
        public void setProxyAddress(string address)
        {
            if (address != null)
                client.Proxy = new WebProxy(address);
            else
                client.Proxy = null;
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

            var repos = client.GetList<Repository>(request);
            if (repos == null)
                throw new Exception("Bad Credentials");

            repos.ForEach(r => r._client = client);
            return repos;
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

            var list = client.GetList<Repository>(request);
            if (list == null)
                throw new InvalidOperationException("User does not exist.");

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

            var list = client.GetList<Repository>(request);

            Organization org = new Organization { Login = organization };
            list.ForEach(r => { r._client = client; r.Organization = org; });
            return list;
        }

        /// <summary>
        /// Retrieves the current user.
        /// 
        /// Requires to be logged in (OAuth/User+Password).
        /// </summary>
        /// <returns>current user</returns>
        public User getCurrentUser()
        {
            if (client.Authenticator == null)
                throw new ArgumentException("no authentication details");

            var request = new RestRequest("/user");

            var user = client.Get<User>(request);
            if (user.Data == null)
                throw new Exception("Bad Credentials");

            return user.Data;
        }

        /// <summary>
        /// Searches through all of Github's repositories, similar to the search on the website itself.
        /// </summary>
        /// <param name="query">what to search for</param>
        /// <returns>(limited) list of matching repositories</returns>
        public List<Repository> searchRepositories(string query)
        {
            var request = new RestRequest("/legacy/repos/search/{query}");
            request.AddUrlSegment("query", query);

            var repos = client.Get<APIv2.RepositoryListV2>(request).Data;
            if (repos == null || repos.Repositories == null)
                throw new Exception(string.Format("Could not search for {0}", query));

            return repos.Repositories.Select(r => r.ToV3(client)).ToList();
        }
    }
}
