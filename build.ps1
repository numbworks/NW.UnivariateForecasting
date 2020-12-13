function Get-CurrentDirectory() {

    [string]$currentDir = $null
    if ($PSISE) { 
        $currentDir = (Split-Path -Path $psISE.CurrentFile.FullPath) 
    }
    elseif ($profile.Contains("VSCode")) {
        $currentDir = (Split-Path $PSEditor.GetEditorContext().CurrentFile.Path) 
    }
    elseif (-not $PSScriptRoot) {
        $currentDir = (Get-ChildItem | ForEach-Object { $_.DirectoryName } | Select-Object -Unique)
    }
    else { 
        $currentDir = $PSScriptRoot 
    }

    return $currentDir

}

Clear-Host
Write-Host -Object "Build process started." 

[string]$currentDir = Get-CurrentDirectory
Write-Host -Object "The current directory is: '$currentDir'." 

[int]$solutionFiles = 
    (Get-ChildItem -Path "$currentDir\*" -Include @("*.sln")) | 
        Measure-Object | 
        Select-Object -ExpandProperty Count
if ($solutionFiles.Equals(1).Equals($false))
{
    Write-Host -Object "An unexpected amount of solution files have been found in the current directory. Aborting."  
    break
}

[System.IO.FileInfo]$solutionFileName = (Get-ChildItem -Path "$currentDir\*" -Include @("*.sln"))
Write-Host -Object "The following solution file has been found: '$($solutionFileName.Name)'."    

[System.IO.DirectoryInfo]$artifactsDir = [System.IO.Path]::Combine($currentDir, "artifacts")
if ($artifactsDir.Exists.Equals($false)) 
{
    Write-Host -Object "The '$artifactsDir' sub-directory doesn't exist. Aborting."
    break
}

Write-Host -Object "The following artifacts directory has been found: '$($artifactsDir.FullName)'."   
Write-Host -Object "Creating NuGet package(s) using 'dotnet pack'..." 
Write-Host -Object ""

dotnet pack $solutionFileName --output $artifactsDir

Write-Host -Object ""
Write-Host -Object "Build process completed." 