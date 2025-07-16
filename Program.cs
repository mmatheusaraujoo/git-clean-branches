using System.Diagnostics;

Console.WriteLine("Please provide the path to the git repository.");
string? repoPath = Console.ReadLine();

if (!Directory.Exists(repoPath))
{
    Console.WriteLine("The provided path does not exist.");
    return;
}

try
{
    var branches = GetBranches(repoPath);
    var branchesToDelete = FilterBranches(branches);
    branchesToDelete = RemoveRemotes(branchesToDelete);

    var continueDelete = ShowConfirmation(branchesToDelete);
    var deletedCout = 0;
    if (continueDelete)
    {
        foreach (var branch in branchesToDelete)
        {
            Console.WriteLine($"Deleting branch: {branch}");
            if (DeleteBranch(repoPath, branch))
            {
                deletedCout++;
            }
            ;
        }
    }
    Console.WriteLine($"Deleted {deletedCout} local branches.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

static bool ShowConfirmation(string[] branchesToDelete)
{
    Console.WriteLine($"The following ({branchesToDelete.Length}) branches will be deleted:");
    foreach (var branch in branchesToDelete)
    {
        Console.WriteLine(branch);
    }

    Console.WriteLine("Do you want to continue? (y/n)");
    var response = Console.ReadLine();

    return response != null && response.ToLower().Equals("y");
}

static string[] GetBranches(string repoPath)
{
    return ExecuteGitCommand(repoPath, "branch -a").Split('\n')
        .Select(b => b.Trim())
        .Where(b => !string.IsNullOrEmpty(b))
        .ToArray();
}

static string[] FilterBranches(string[] branches, string[]? filters = null)
{
    filters ??= ["master", "main", "develop", "homolog", "release", "*"];
    return branches.Where(branch => !filters.Any(branch.Contains)).ToArray();
}

static string[] RemoveRemotes(string[] branches)
{
    return branches.Where(b => !b.Contains("remotes")).ToArray();
}

static bool DeleteBranch(string repoPath, string branch)
{
    try
    {
        ExecuteGitCommand(repoPath, $"branch -d {branch}");
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to delete branch: {branch} \n Error: {ex.Message}");
        return false;
    }
}

static string ExecuteGitCommand(string repoPath, string command)
{
    var processInfo = new ProcessStartInfo("git", command)
    {
        WorkingDirectory = repoPath,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using var process = new Process();
    process.StartInfo = processInfo;
    process.Start();

    string output = process.StandardOutput.ReadToEnd();
    string error = process.StandardError.ReadToEnd();

    process.WaitForExit();

    if (process.ExitCode != 0)
    {
        throw new Exception($"Git command failed: {error}");
    }

    return output;
}
