using GitBranchCleaner.Services;

namespace GitBranchCleaner;

public class BranchManager
{
    private readonly GitService _gitService;

    private readonly IEnumerable<string> _branches = [];

    public BranchManager(GitService gitService)
    {
        _gitService = gitService;
        _branches = _gitService.GetBranches();
    }


    public string[] FilterBranches(string[]? filters = null)
    {
        filters ??= ["master", "main", "develop", "homolog", "release", "*"];
        return [.. _branches.Where(branch => !filters.Any(branch.Contains))];
    }

    public string[] RemoveRemotes(string[] branches)
    {
        return branches.Where(b => !b.Contains("remotes")).ToArray();
    }

}





