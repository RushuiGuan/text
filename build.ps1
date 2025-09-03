param(
	[Parameter(Position=0)]
	[string]$project,
	[switch][bool]$skip,
	[switch][bool]$run
)
$InformationPreference = "Continue";
$install = $env:InstallDirectory;

function KillProcess([System.Diagnostics.Process]$process) {
	[System.Diagnostics.Process]$parentProcess = $process.Parent;
	if ($parentProcess -and $parentProcess.ProcessName -eq "WindowsTerminal") {
		FindAndKillTerminalTab $process;
	}
	else {
		Write-Information "Stopping process $($process.ProcessName)";
		Stop-Process -Id $process.Id -Force;
		Start-Sleep -Seconds 0.1;
		Write-Output $process.ProcessName;
	}
}

function FindAndKillTerminalTab([System.Diagnostics.Process]$process) {
	$diff = [datetime]::MaxValue.Ticks;
	[System.Diagnostics.Process]$openConsoleProcess;
	Get-Process "OpenConsole" | ForEach-Object {
		$gap = $process.StartTime.Ticks - $_.StartTime.Ticks;
		if ($gap -ge 0 -and $diff -gt $gap -and $gap -lt 1000000) {
			$diff = $gap;
			$openConsoleProcess = $_;
		}
	}
	if ($openConsoleProcess) {
		Write-Information "Killing the terminal window tab $($openConsoleProcess.ProcessName) $($openConsoleProcess.Id)";
		Stop-Process -Id $openConsoleProcess.Id -Force;
		Start-Sleep -Seconds 0.1;
	}
	Write-Information "Stopping process $($process.ProcessName)";
	Stop-Process -Id $process.Id -Force;
	Start-Sleep -Seconds 0.1;
}

$projects = @(
	"Sample"
);
	
$serviceProjects =@(
);
if (-not [string]::IsNullOrEmpty($project)) {
	$projects = $projects | Where-Object { $_ -like "*$project" }
}

if (-not $skip) {
	foreach ($item in $projects) {
		if ($serviceProjects -contains $item) {
			Get-Process $item -ErrorAction Ignore | ForEach-Object { 
				KillProcess $_;
			} 
		}

		if (Test-Path "$install/$item" -type Container) {
			Write-Information "Deleting $item";
			Get-ChildItem $install\$item | Remove-Item -Recurse -Force;
		}
	}
}

dotnet restore $PSScriptRoot
foreach ($project in $projects) {
	dotnet publish $PSScriptRoot\$project\$project.csproj -o $install\$project -c release
}

$projects2Run = @();
if ($run) {
	$projects2Run = $serviceProjects;
}

if ($projects2Run.Length -gt 0) {
	$hasTab = $false;
	$projects2Run | ForEach-Object {
		Write-Information "Starting $_";
		if ($hasTab) {
			wt -w 0 split-pane --title $_ -d $PSScriptRoot $env:InstallDirectory\$_\$_.exe
		} else {
			wt -w 0 new-tab --title $_ -d $PSScriptRoot $env:InstallDirectory\$_\$_.exe
			$hasTab = $true;
		}
	}
}
set-alias -name sample -Value $env:InstallDirectory\Sample\sample.exe