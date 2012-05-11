Git.hub
=======

A simple API in terms of querying github with C#, based on RestSharp.

Usage
-----

### Create a client instance
```csharp
using Git.hub;
Client client = new Client();
```

### login
OAuth token:
```csharp
client.setOAuth2Token("0fd...");
```

Username and Password:
```csharp
client.setCredentials("mabako", "super duper password");
```

Retrieve the currently logged in user:
```csharp
User user = client.getUser();
```


### fetch repositories
```csharp
IList<Repository> repos = client.getRepositories("mabako");
IList<Repository> orgRepos = client.getOrganizationRepositories("github");

Repository pluginSource = client.getRepository("mabako", "Git.hub");

/* Requires login */
IList<Repository> ownRepos = client.getRepositories();
```

### repository actions
Fork the repo:
```csharp
Repository forked = pluginSource.CreateFork();
```

List branches:
```csharp
IList<Branch> branches = pluginSource.GetBranches();
```
