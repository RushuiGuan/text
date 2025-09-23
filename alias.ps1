set-alias -name sample -Value (join $env:InstallDirectory, "Sample", ($IsMacOS ? "Sample": "Sample.exe"))
