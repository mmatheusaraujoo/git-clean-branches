namespace GitBranchCleaner.Helpers;

public class GitCommandException : Exception
{
    public string Command { get; }
    public string Error { get; }

    public GitCommandException(string command, string error)
        : base($"Git command '{command}' failed with error: {error}")
    {
        Command = command;
        Error = error;
    }

    public override string ToString()
    {
        return $"{base.ToString()}\nCommand: {Command}\nError: {Error}";
    }
}