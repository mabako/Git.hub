namespace Git.hub
{
    /// <summary>
    /// References a single commit.
    /// </summary>
    public class BranchCommitRef
    {
        /// <summary>
        /// SHA-reference to the latest commit
        /// </summary>
        public string Sha { get; private set; }
    }

    public class Branch
    {
        /// <summary>
        /// Name of the branch
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Information about the commit the branch is at
        /// </summary>
        public BranchCommitRef Commit { get; private set; }
    }
}
