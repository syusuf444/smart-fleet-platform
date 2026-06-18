Write-Host "===========================================" -ForegroundColor Cyan
Write-Host " SMART FLEET PLATFORM - STARTING SYSTEM " -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan

# Kill existing dotnet processes
Write-Host "Stopping existing .NET processes..." -ForegroundColor Yellow

taskkill /IM dotnet.exe /F 2>$null

Start-Sleep -Seconds 3

# Root Path
$rootPath = "C:\Users\YUSUF SAYED\AI Project 070526\smart-fleet-platform"

# Start Docker Infrastructure
Write-Host "Starting Docker Infrastructure..." -ForegroundColor Yellow

Set-Location $rootPath

docker-compose up -d

Write-Host "Waiting for Kafka and SQL Server to start..." -ForegroundColor Yellow

Start-Sleep -Seconds 20

# Run FleetService API
Write-Host "Starting FleetService API..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\services\fleet-service\FleetService.API'; dotnet run"

# Run IdentityService API
Write-Host "Starting IdentityService API..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\services\identity-service\IdentityService.API'; dotnet run"

Start-Sleep -Seconds 10

Write-Host "===========================================" -ForegroundColor Cyan
Write-Host " ALL SERVICES STARTED SUCCESSFULLY " -ForegroundColor Green
Write-Host "===========================================" -ForegroundColor Cyan

Write-Host "\nSwagger URLs:" -ForegroundColor Magenta

Write-Host "FleetService API:" -ForegroundColor White
Write-Host "http://localhost:5081/swagger" -ForegroundColor Cyan

Write-Host "IdentityService API:" -ForegroundColor White
Write-Host "https://localhost:7093/swagger" -ForegroundColor Cyan
Write-Host "http://localhost:5093/swagger" -ForegroundColor Cyan

Write-Host "\nInfrastructure:" -ForegroundColor Magenta

Write-Host "Kafka:" -ForegroundColor White
Write-Host "localhost:9092" -ForegroundColor Cyan

Write-Host "SQL Server:" -ForegroundColor White
Write-Host "localhost:1433" -ForegroundColor Cyan

Write-Host "\nDocker Containers:" -ForegroundColor Magenta

docker ps