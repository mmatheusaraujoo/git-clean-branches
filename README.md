# GitCleanBranches

🧹 A lightweight C# terminal application to clean up outdated local Git branches.

## 📖 Overview

Over time, local Git repositories can accumulate dozens of old branches that have already been merged or deleted from the remote.  
**GitCleanBranches** is a simple terminal utility built with .NET to help developers maintain a clean workspace by automatically removing these local branches.

It scans your local repository and removes branches that:

- Have already been merged into `main` or `master`
- No longer exist on the remote (`origin`)
- Are not currently checked out

---

## 🚀 Features

- Lists merged or orphaned branches
- Safely removes only outdated branches
- Supports both `main` and `master` as the default branch
- Easy to use via terminal
- Cross-platform (Windows, Linux, macOS)

---

## 💻 How to Use

### Run from source

```bash
dotnet run --project GitCleanBranches
```
