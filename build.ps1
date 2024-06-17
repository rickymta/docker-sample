param (
    [Parameter(Mandatory=$true)]
    [ValidateSet("All", "Compile", "LintCheck", "RunUnitTests", "RunMutationTests")]
    [string]$Task
)

function ExecSafe([scriptblock] $cmd) {
    & $cmd
    if ($LASTEXITCODE) { exit $LASTEXITCODE }
}

$srcDir = "$PSScriptRoot\src"

# Get a list of dynamic projects from subfolders inside the src folder
$projects = Get-ChildItem -Path $srcDir -Directory | ForEach-Object { $_.FullName }

foreach ($project in $projects) {
    #Compiling the project
    Write-Output "Compiling $project..."
    ExecSafe { & dotnet build $project /nodeReuse:false /p:UseSharedCompilation=false -nologo -clp:NoSummary --verbosity quiet }
    
    #Lint checking the project
    Write-Output "Lint checking $project..."
    ExecSafe { & dotnet tool run dotnet-format -v diag --check --exclude .git --exclude bin --exclude obj --folder $project }
    
    #Running unit tests for the project
    Write-Output "Running unit tests for $project..."
    ExecSafe { & dotnet test $project --no-build --verbosity normal }
    
    #Running mutation tests for the project
    Write-Output "Running mutation tests for $project..."
    ExecSafe { & dotnet stryker --project $project --verbosity normal }
}
