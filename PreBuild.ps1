param(
	[Parameter(Mandatory=$True)]
	[string]$ProjectDir,
	
	[Parameter(Mandatory=$True)]
	[string]$Action
)

#-- Check the required PowerShell version
if ($PSVersionTable.PSVersion.Major -lt 2)
{
    $MyInvocation.MyCommand.Definition + "(12) : error: Please install PowerShell 2.0 or later"
    exit 1
}

echo ("Path to project: " + $ProjectDir)
echo ("Action: " + $Action)

$ScriptDir = (split-path -parent $MyInvocation.MyCommand.Definition)
$GitRootDir = $ScriptDir + "/.."
[xml]$Versions = Get-Content ($ScriptDir + "/Versions.xml")
$ServiceVersion = $Versions.Versions.ServiceVersion.Value
$SharedAssemblyInfoPath = ($ScriptDir + "/SharedAssemblyInfo.generated.cs")

function Execute-Git([string] $Arguments)
{
	$info = New-Object System.Diagnostics.ProcessStartInfo
	$info.FileName = "git"
	$info.Arguments = $Arguments
	$info.UseShellExecute = $false
	$info.CreateNoWindow = $true
	$info.RedirectStandardOutput = $true
	$info.RedirectStandardError = $true
	$process = New-Object System.Diagnostics.Process
	$process.StartInfo = $info
	$process.Start() | Out-Null
	$process.WaitForExit();
	$stdout = $process.StandardOutput.ReadToEnd()
	$stderr = $process.StandardError.ReadToEnd()
	echo $stdout
}

function Get-RevisionNumber()
{
    $output = Execute-Git ("--work-tree=""" + $GitRootDir + """ rev-list HEAD --count")
	echo ($output -replace "[\n\r\t ]","")
}

function Get-RevisionShortHash()
{
    $output = Execute-Git ("--work-tree=""" + $GitRootDir + """ rev-parse HEAD")
    [Convert]::ToUInt32(($output.Substring(0,4) -replace "[\n\r\t ]",""), 16).ToString()
}

function Out-FileChangedContent([string]$Content, [string]$OutFileName)
{
	$OldContent = (Get-Content -ErrorAction Ignore $OutFileName) -join "`n"
	if (![string]::Equals($OldContent, $Content))
	{
		$Content | Out-File -FilePath $OutFileName -Encoding utf8 -Force
	}
}

function Read-Revision()
{
    $Content = Get-Content $SharedAssemblyInfoPath | select-string 'assembly: AssemblyVersion' -SimpleMatch
	$Content = $Content -replace ".*\(""","" -replace """.*",""
	$Version = New-Object System.Version ($Content)
    $Version.Build
}

function Generate-AssemblyInfo([string]$InFileName, [string]$OutFileName)
{
	[string]$Template = (Get-Content $InFileName) -join "`n"
	$Version = New-Object System.Version ($ServiceVersion)
	$Version = New-Object System.Version ($Version.Major, $Version.Minor, (Get-RevisionNumber), (Get-RevisionShortHash))
	$Template = $Template -replace "assembly: AssemblyVersion.*\)",("assembly: AssemblyVersion(""" + $Version.ToString() + """)")
	$Template = $Template -replace "assembly: AssemblyFileVersion.*\)",("assembly: AssemblyFileVersion(""" + $Version.ToString() + """)")
	$Template = $Template -replace "MinClientWindowsVersion.*;",("MinClientWindowsVersion = """ + $MinClientWindowsVersion + """;")
	$Template = $Template -replace "MinClientWindowsPhoneVersion.*;",("MinClientWindowsPhoneVersion = """ + $MinClientWindowsPhoneVersion + """;")
	Out-FileChangedContent -Content $Template -OutFileName $OutFileName
    [string]::Format("Version: {0} Git hash: {1:x4}", $Version.ToString(), $Version.Revision)
}

switch ($Action)
{
	"AssemblyInfo"
	{
		Generate-AssemblyInfo ($ScriptDir + "/SharedAssemblyInfo.Template.cs") ($SharedAssemblyInfoPath)
	}
	
	default 
	{
		"Unknown Action"
		exit 1
	}
}
