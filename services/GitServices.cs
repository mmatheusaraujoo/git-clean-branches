using System.Diagnostics;
using GitBranchCleaner.Helpers;

namespace GitBranchCleaner.Services;

public class GitService
{
    private readonly string _repositoryPath;

    public GitService(string repositoryPath)
    {
        _repositoryPath = repositoryPath;
    }

    public IEnumerable<string> GetBranches()
    {
        ExecuteGitCommand(_repositoryPath, "fetch --all");

        return ExecuteGitCommand(_repositoryPath, "branch -a").Split('\n')
        .Select(b => b.Trim())
        .Where(b => !string.IsNullOrEmpty(b));
    }

    public bool DeleteBranch(string branch)
    {
        try
        {
            ExecuteGitCommand(_repositoryPath, $"branch -d {branch}");
            return true;
        }
        catch (GitCommandException ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting branch '{branch}': {ex.Message}");
            return false;
        }
    }


    private string ExecuteGitCommand(string repoPath, string command)
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
            throw new GitCommandException(command, error);
        }

        return output;
    }

}