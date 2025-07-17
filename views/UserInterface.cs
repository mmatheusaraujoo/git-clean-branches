namespace GitBranchCleaner.views;

public static class UserInterface
{
    public static string GetPathToRepository()
    {
        Console.WriteLine("Please provide the path to the git repository:");
        string? repoPath = Console.ReadLine();
        if (string.IsNullOrEmpty(repoPath) || !Directory.Exists(repoPath))
        {
            Console.WriteLine("The provided path does not exist or is invalid.");
            return string.Empty;
        }
        return repoPath;
    }

    public static bool ShowConfirmation(string[] branchesToDelete)
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



    public static void InitializeInterface()
    {
        Console.WriteLine("Welcome to Git Branch Cleaner!");
        Console.WriteLine("This tool will help you clean up your git branches.");
        Console.WriteLine("Please follow the prompts to proceed.");
        Console.WriteLine("---------------------------------------------------");
    }

    public static void ShowError(string message, string? details = null)
    {
        if (!string.IsNullOrEmpty(details))
        {
            Console.WriteLine($"Error: {message} Details: {details}");
            return;
        }
        else
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}
