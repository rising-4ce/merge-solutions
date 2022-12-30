param(
    [Parameter(Mandatory=$True)]
    [ValidateSet('y','n')]
    [string]$CreateTag
)

$ErrorActionPreference = "Stop"
$ProjectName = "MergeSolutionsUI"
$Sln = "merge-solutions.sln"
cd $PSScriptRoot

if ($CreateTag -eq "y")
{
    "Ensure tagging constraints..."
    if(git status --porcelain |Where {$_ -match '^\?\?'}){
        git status --porcelain
        throw "Cannot tag: untracked files exist"
    } 
    elseif(git status --porcelain |Where {$_ -notmatch '^\?\?'}) {
        git status --porcelain
        throw "Cannot tag: uncommitted changes found"
    }
}

dotnet restore $Sln
# Build a Debug configuration of Common to create a SharedAssemblyInfo.generated.cs
dotnet msbuild $Sln /verbosity:m /target:"MergeSolutions_Core" /p:Configuration=Debug

$Version = Get-Item "SharedAssemblyInfo.generated.cs" | Select-String -Pattern 'AssemblyVersion' 
$Version = ($Version -split '"')[1]

$BuildDir = "$PSScriptRoot\bin\$ProjectName $Version" -replace "`n|`r"
Remove-Item "$BuildDir" -Recurse -ErrorAction SilentlyContinue

$Projects = "merge-solutions;MergeSolutions_UI"
$Targets = ""
foreach($Project in $Projects.Split(";", [System.StringSplitOptions]::RemoveEmptyEntries))
{
    $Targets += ";${Project}:Publish"
}
$Targets = $Targets.TrimStart(";")

dotnet msbuild $Sln /verbosity:m /target:"Clean" /p:Configuration=Release
dotnet msbuild $Sln /target:$Targets /p:SelfContained=False /p:PublishProtocol=FileSystem /p:Configuration=Release /p:PublishReadyToRun=False /p:PublishTrimmed=False /p:PublishDir="$BuildDir"
if (! $?)
{
    Remove-Item "$BuildDir" -Recurse -ErrorAction SilentlyContinue
    throw 'Solution build and file publish failed'
}

function ZipFiles( $zipfilename, $sourcedir)
{
  Add-Type -Assembly System.IO.Compression.FileSystem
  try 
  {
    Remove-Item $zipfilename -ErrorAction SilentlyContinue
    $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
    [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $true)
    "Success"
  }
  catch
  {
    Remove-Item $zipfilename
    "Failure: $_"
  }
}

$ZipName = "$PSScriptRoot\..\$ProjectName $Version.zip" -replace "`n|`r"
"$ZipName < $BuildDir"
ZipFiles $ZipName $BuildDir
Remove-Item "$BuildDir" -Recurse -ErrorAction SilentlyContinue

Write-Host "Built version: $Version"

if ($CreateTag -eq "y")
{
    "Tagging ..."
    git tag "release-$Version"
    if (-not $?)
    {
        throw 'Tagging failed'
    }

    git.exe push origin refs/tags/"release-$Version" --porcelain
}
