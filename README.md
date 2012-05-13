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

### Login
The recommended way to login is to use OAuth tokens provided by Github,
as detailed on [the API docs](http://developer.github.com/v3/oauth/).
```csharp
client.setOAuth2Token("0fd...");
```
You could, for example, display a webbrowser control to navigate to the
site and listen to the related events for when a redirect to your
given site happens, use the ?code=<code> with the following line:
```csharp
OAuth2Helper.requestToken(client_id, client_secret, <code>
```

Albeit not recommended, logging in with Username and Password is possible:
```csharp
client.setCredentials("mabako", "super duper password");
```

To retrieve the currently logged in user, use the following:
```csharp
User user = client.getUser();
```


### Repositories
To fetch repositories, the following lines of code should suffice your needs:
```csharp
IList<Repository> repos = client.getRepositories("mabako");
IList<Repository> orgRepos = client.getOrganizationRepositories("github");

Repository repo = client.getRepository("mabako", "Git.hub");

/* Requires login */
IList<Repository> ownRepos = client.getRepositories();
```
Please take note that the latter includes all repositories you have access to,
if you certainly want your own repos only filter for Repository.Owner.Login.

### Simple Repository Actions
Fork the repo:
```csharp
Repository forked = repo.CreateFork();
```

List branches:
```csharp
IList<Branch> branches = repo.GetBranches();
```

### Pull Requests
You can fetch all of the repo's pull requests or just one, use as fit.
```csharp
IList<PullRequest> pullrequests = repo.GetPullRequests();
PullRequest pullrequest = repo.GetPullRequest(1);
```

Alternatively, a new pull request may be created with
```csharp
var pullrequest = repo.CreatePullRequest("mabako:new-feature", "master", "Subject", "Details...");
```
Take note that 'repo' is the repo in which the pull request is created,
your own username is usually in the first parameter, along with your branch.

A few basic actions on pull requests are defined:
```csharp
pullrequest.GetCommits();
pullrequest.Open();
pullrequest.Close();

/* This is the same */
var issue = pullrequest.ToIssue();
issue.GetComments();
pullrequest.GetIssueComments()
```

To reply to a pull request, fetch the issue first with ToIssue() and use
```csharp
issue.CreateComment("My comment");
```