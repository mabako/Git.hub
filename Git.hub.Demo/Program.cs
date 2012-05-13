using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Git.hub;

namespace Git.hub.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            
            //client.setCredentials("mabako", "");
            //client.setOAuth2Token("");
            //Console.WriteLine("Logged in as: {0}", client.getCurrentUser());
            //client.getRepositories().ToList().ForEach(repo => Console.WriteLine("  {0}", repo.Name));
            
            Console.WriteLine();
            Console.WriteLine("Repositories of mabako?");
            client.getRepositories("mabako").ToList().ForEach(repo => Console.WriteLine("  {0}", repo.Name));

            Console.WriteLine();
            Console.WriteLine("Branches of mabako/zwickau-mensa?");
            client.getRepository("mabako", "zwickau-mensa").GetBranches().ToList().ForEach(branch => Console.WriteLine("  {0} at {1}", branch.Name, branch.Commit.Sha));

            Console.WriteLine();
            Console.WriteLine("Parent of mabako/Android-Terminal-Emulator?");
            Console.WriteLine("  {0}", client.getRepository("mabako", "Android-Terminal-Emulator").Parent);

            Console.WriteLine();
            var gitext = client.getRepository("spdr870", "gitextensions");
            Console.WriteLine("Pull Requests of " + gitext.ToString());
            gitext.GetPullRequests().ToList().ForEach(pr => Console.WriteLine("  #{0}: {1} by {2}", pr.Number, pr.Title, pr.User.Login));

            Console.WriteLine();
            var pullrequest = gitext.GetPullRequest(599);
            Console.WriteLine(pullrequest.Title + " by " + pullrequest.User.Login);
            var commits = pullrequest.GetCommits();
            var comments = pullrequest.GetIssueComments();
            commits.ToList().ForEach(commit => Console.WriteLine("  has Commit by {0}", commit.AuthorName));
            comments.ToList().ForEach(comment => Console.WriteLine("  has Comment by {0}", comment.User.Login));

            /*
            Console.WriteLine();
            var apitest = client.getRepository("mabako", "apitest");
            var apitest_pr = apitest.GetPullRequest(3);
            apitest_pr.ToIssue().CreateComment("This is a sample comment from the API");
            //Console.WriteLine(apitest_pr.CreatePullRequest("mabako:tex", "master", "title", "body"));
            */

            Console.WriteLine();
            Console.WriteLine("Query for Git Extensions?");
            var search = client.searchRepositories("Git Extensions");
            search.ForEach(repo =>
            {
                Console.WriteLine("  {0} by {1}", repo.Name, repo.Owner.Login);
                Console.WriteLine("   -> {0}", repo.GitUrl);
            });

            Console.ReadLine();
        }
    }
}
