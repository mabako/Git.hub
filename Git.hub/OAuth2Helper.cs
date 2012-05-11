using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace Git.hub
{
    class OAuth2Data
    {
        public string AccessToken { get;  private set; }
        public string TokenType { get; private set; }
    }

    public class OAuth2Helper
    {
        /// <summary>
        /// Requests a Github API token, given you received a code first, i.e. user allowed that.
        /// 
        /// See OAuth documentation for how to do that.
        /// Your App needs to be registered first.
        /// </summary>
        /// <param name="client_id">your app's client_id</param>
        /// <param name="client_secret">your app's secret</param>
        /// <param name="code">code you got from github</param>
        /// <returns>oauth token if successful, null otherwise</returns>
        public static string requestToken(string client_id, string client_secret, string code)
        {
            // Not on api.github.com
            var client = new RestClient("https://github.com");

            var request = new RestRequest("/login/oauth/access_token");
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("code", code);

            var response = client.Post<OAuth2Data>(request);
            if (response.Data != null)
                return response.Data.AccessToken;
            return null;
        }
    }
}
