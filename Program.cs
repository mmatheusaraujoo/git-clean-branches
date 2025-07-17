using GitBranchCleaner;
using GitBranchCleaner.Services;
using GitBranchCleaner.views;


UserInterface.InitializeInterface();

var repoPath = UserInterface.GetPathToRepository();
if (string.IsNullOrEmpty(repoPath))
{
    UserInterface.ShowError("Invalid repository path provided.");
    return;
}
var gitService = new GitService(repoPath);
var branchManager = new BranchManager(gitService);

var branches = branchManager.FilterBranches();
branches = branchManager.RemoveRemotes(branches);
if (branches.Length == 0)
{
    Console.WriteLine("No branches to delete.");
    return;
}
if (!UserInterface.ShowConfirmation(branches))
{
    Console.WriteLine("Operation cancelled by user.");
    return;
}

foreach (var branch in branches)
{
    try
    {
        gitService.DeleteBranch(branch);
        Console.WriteLine($"Branch '{branch}' deleted successfully.");
    }
    catch (Exception ex)
    {
        UserInterface.ShowError($"Failed to delete branch '{branch}'.", ex.Message);
    }
}

