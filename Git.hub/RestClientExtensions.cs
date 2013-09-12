namespace Git.hub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using RestSharp;

    internal static class RestClientExtensions
    {
        private static readonly Regex LinkHeaderFormat = new Regex(@"<(?<Link>[^>]*)>; rel=""(?<Rel>\w*)""", RegexOptions.Compiled);

        public static List<T> GetList<T>(this IRestClient client, IRestRequest request)
        {
            List<T> result = new List<T>();
            while (true)
            {
                IRestResponse<List<T>> pageResponse = client.Get<List<T>>(request);
                if (pageResponse.Data == null)
                    return null;

                result.AddRange(pageResponse.Data);

                Parameter linkHeader = pageResponse.Headers.FirstOrDefault(i => string.Equals(i.Name, "Link", StringComparison.OrdinalIgnoreCase));
                if (linkHeader == null)
                    break;

                bool hasNext = false;
                foreach (Match match in LinkHeaderFormat.Matches(linkHeader.Value.ToString()))
                {
                    if (string.Equals(match.Groups["Rel"].Value, "next", StringComparison.OrdinalIgnoreCase))
                    {
                        request = new RestRequest(new Uri(match.Groups["Link"].Value));
                        hasNext = true;
                        break;
                    }
                }

                if (!hasNext)
                    break;
            }

            return result;
        }
    }
}
