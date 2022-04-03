$EfCoreFolder = '..\src\Api\Catalog.Api.EfCore'

$CurrentLocation = Get-Location

cd $EfCoreFolder

if (Test-Path -Path Migrations) {    
    Remove-Item Migrations -Recurse -Force -Confirm:$false
}

dotnet ef database drop -s ..\Catalog.Api -p . -f -c CatalogContext -- --provider sqlite
dotnet ef migrations add InitialCreate -s ..\Catalog.Api -p .  -c CatalogContext --no-build -- --provider sqlite
dotnet ef database update -s ..\Catalog.Api -p .  -c CatalogContext -- --provider sqlite
Remove-Item Migrations -Recurse -Force -Confirm:$false
cd $CurrentLocation
Write-Output "Database generation created with success"
Start-Sleep -Seconds 5