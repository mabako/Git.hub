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
            
            // List all repositories of the user 'mabako'
            client.getRepositories("mabako").ToList().ForEach(repo => Console.WriteLine(repo.Name));

            Console.WriteLine();
            Console.WriteLine("Branches of zwickau-mensa?");
            client.getRepository("mabako", "zwickau-mensa").GetBranches().ToList().ForEach(branch => Console.WriteLine("  {0} at {1}", branch.Name, branch.Commit.Sha));
            
            Console.ReadLine();
        }
    }
}
