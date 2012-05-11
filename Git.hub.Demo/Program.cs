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
            
            client.setCredentials("mabako", "");
            //client.setOAuth2Token("");
            Console.WriteLine("Logged in as: {0}", client.getCurrentUser());
            client.getRepositories().ToList().ForEach(repo => Console.WriteLine("  {0}", repo.Name));

            Console.WriteLine();
            Console.WriteLine("Repositories of mabako?");
            client.getRepositories("mabako").ToList().ForEach(repo => Console.WriteLine("  {0}", repo.Name));

            Console.WriteLine();
            Console.WriteLine("Branches of mabako/zwickau-mensa?");
            client.getRepository("mabako", "zwickau-mensa").GetBranches().ToList().ForEach(branch => Console.WriteLine("  {0} at {1}", branch.Name, branch.Commit.Sha));

            Console.WriteLine();
            Console.WriteLine("Parent of mabako/Android-Terminal-Emulator?");
            Console.WriteLine("  {0}", client.getRepository("mabako", "Android-Terminal-Emulator").Parent);
            Console.ReadLine();
        }
    }
}
