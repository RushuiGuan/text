$working = $PSScriptRoot;

Get-ChildItem $working\docs | Remove-Item -Recurse -Force

xmldoc2md $working\Albatross.Text\bin\Debug\net8.0\Albatross.Text.dll `
	-o $working\docs\ `
	--github-pages `
	--structure tree `
	--back-button

xmldoc2md $working\Albatross.Text.Table\bin\Debug\net8.0\Albatross.Text.Table.dll `
	-o $working\docs\ `
	--github-pages `
	--structure tree `
	--back-button `
	--index-page-name $working\docs\index2
	
	xmldoc2md $working\Albatross.Text.CliFormat\bin\Debug\net8.0\Albatross.Text.CliFormat.dll `
    	-o $working\docs\ `
    	--github-pages `
    	--structure tree `
    	--back-button `
    	--index-page-name $working\docs\index3

Get-Content $working\docs\index2.md >> $PSScriptRoot\docs\index.md
Get-Content $working\docs\index3.md >> $PSScriptRoot\docs\index.md

Remove-Item $working\docs\index2.md
Remove-Item $working\docs\index3.md