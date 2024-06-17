param (
    [Parameter(Mandatory=$true)]
    [ValidateSet("Compile", "LintCheck", "RunUnitTests", "RunMutationTests")]
    [string]$Task
)

function ExecSafe([scriptblock] $cmd) {
    & $cmd
    if ($LASTEXITCODE) { exit $LASTEXITCODE }
}

$srcDir = "$PSScriptRoot\src"

# Lấy danh sách các project động từ các thư mục con bên trong thư mục src
$projects = Get-ChildItem -Path $srcDir -Directory | ForEach-Object { $_.FullName }

foreach ($project in $projects) {
    switch ($Task) {
        "Compile" {
            Write-Output "Compiling $project..."
            ExecSafe { & dotnet build $project /nodeReuse:false /p:UseSharedCompilation=false -nologo -clp:NoSummary --verbosity quiet }
        }
        "LintCheck" {
            Write-Output "Lint checking $project..."
            ExecSafe { & dotnet tool run dotnet-format -v diag --check --exclude .git --exclude bin --exclude obj --folder $project }
        }
        "RunUnitTests" {
            Write-Output "Running unit tests for $project..."
            ExecSafe { & dotnet test $project --no-build --verbosity normal }
        }
        "RunMutationTests" {
            Write-Output "Running mutation tests for $project..."
            ExecSafe { & dotnet stryker --project $project --verbosity normal }
        }
    }
}
