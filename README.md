# What it does

Merges Visual Studio solutions into a single .sln file

# Merge Solutions UI application features

- Support of excluding of unnecessary projects in a merged solution 
- Merge plan document with editor. 
  - Merge plan document stores a list of input solutions and excluded projects.
  - UI editor allows to open and save a merge plan, append and remove solutions, rename a solution node ie. target solution folder in a merged solution.
- Appropriate for GIT super projects.
  - Relative paths. Merge plan is recalculated on every Save so the included solutions paths are relative to the saved merge plan document.
  - Idempotent solution generation (so re-generation of merged solution will produce the same content):
    - Guids of per-solution folders in a merged solution are the same as source solutions guids. 
    - A guid of merged solution is a xor function of root level folders guids.
- Read only for source solutions and projects. 
  - UI application do not change original input solutions and projects so guid conflict terminates a solution merging. Use a command line tool to fix dups.
- Better support of solution sections merging than original project does
  - Every solution has own solution folder in a merged one with complete solution folder structure. 
  - Empty solution folders are excluded.
  - Handle a special folder Solution Items and move into per-solution subfolders as Inner Solution Items
  - Merge platforms and project configuration sections
  - Handle guid of merged solution 

# Command line tool usage
```
merge-solutions.exe [/nonstop] [/fix] [/config solutionlist.txt] [/out merged.sln] [solution1.sln solution2.sln ...]
  /fix              regenerates duplicate project guids and replaces them in corresponding project/solution files,
                    requires write-access to project and solution files
  /config list.txt  takes list of new-line separated solution paths from list.txt file
  /out merged.sln   path to output solution file. Default is 'merged.sln'
  /nonstop          do not prompt for keypress if there were errors/warnings
  solution?.sln     list of solutions to be merged
```


# Original project

https://code.google.com/archive/p/merge-solutions/
