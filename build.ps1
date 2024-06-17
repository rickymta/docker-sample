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

# List of projects
$projects = (
    "src/Services/Draft.Services.AuthService/Draft.Services.AuthService.csproj",
    "src/Presentations/Draft.Presentations.ApiGateway/Draft.Presentations.ApiGateway.csproj"
)

foreach ($project in $projects) {
    #Compiling the project
    Write-Output "Compiling $project..."
    ExecSafe { & dotnet build $project /nodeReuse:false /p:UseSharedCompilation=false -nologo -clp:NoSummary --verbosity quiet }
    
    #Lint checking the project
    Write-Output "Lint checking $project..."
    ExecSafe { & dotnet format whitespace --folder --include ./src/ ./tests/ --exclude ./src/submodule-a/ --verify-no-changes }
    
    #Running unit tests for the project
    Write-Output "Running unit tests for $project..."
    ExecSafe { & dotnet test $project --no-build --verbosity normal }
}
    
#Running mutation tests for the project
Write-Output "Running mutation tests for sollution"
ExecSafe { & dotnet-stryker --verbosity trace --log-to-file }
