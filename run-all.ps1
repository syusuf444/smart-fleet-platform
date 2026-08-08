Write-Host "===========================================" -ForegroundColor Cyan
Write-Host " SMART FLEET PLATFORM - STARTING SYSTEM " -ForegroundColor Cyan
Write-Host "===========================================" -ForegroundColor Cyan

# Kill existing dotnet processes
Write-Host "Stopping existing .NET processes..." -ForegroundColor Yellow

taskkill /IM dotnet.exe /F 2>$null

Start-Sleep -Seconds 3

# Root Path
$rootPath = Split-Path -Parent $MyInvocation.MyCommand.Path

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

# Run AIService API
Write-Host "Starting AIService API..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\services\ai-service\AiService.API'; dotnet run"

# Run NotificationService API
Write-Host "Starting NotificationService API..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\services\notification-service\NotificationService.API'; `$env:ASPNETCORE_URLS='http://localhost:5085'; dotnet run"

# Run ApiGateway
Write-Host "Starting API Gateway..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\gateway\ApiGateway'; dotnet run"

# Run React Frontend
Write-Host "Starting React Frontend..." -ForegroundColor Green

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$rootPath\frontend\fleet-portal'; npm run dev"

Start-Sleep -Seconds 15

Write-Host "===========================================" -ForegroundColor Cyan
Write-Host " ALL SERVICES STARTED SUCCESSFULLY " -ForegroundColor Green
Write-Host "===========================================" -ForegroundColor Cyan

Write-Host "`nApplication URLs:" -ForegroundColor Magenta

Write-Host "Fleet Portal (Frontend):" -ForegroundColor White
Write-Host "http://localhost:5173" -ForegroundColor Cyan

Write-Host "API Gateway (Ocelot):" -ForegroundColor White
Write-Host "http://localhost:5000" -ForegroundColor Cyan

Write-Host "FleetService API:" -ForegroundColor White
Write-Host "http://localhost:5081/swagger" -ForegroundColor Cyan

Write-Host "IdentityService API:" -ForegroundColor White
Write-Host "http://localhost:5057/swagger" -ForegroundColor Cyan

Write-Host "AIService API:" -ForegroundColor White
Write-Host "http://localhost:5091/health" -ForegroundColor Cyan

Write-Host "NotificationService API:" -ForegroundColor White
Write-Host "http://localhost:5085/health" -ForegroundColor Cyan

Write-Host "`nKey Gateway Routes:" -ForegroundColor Magenta
Write-Host "Dashboard Stats: GET http://localhost:5000/fleet/Dashboard/stats" -ForegroundColor Cyan
Write-Host "Vehicles: GET http://localhost:5000/fleet/vehicles" -ForegroundColor Cyan

Write-Host "`nInfrastructure:" -ForegroundColor Magenta

Write-Host "Kafka:" -ForegroundColor White
Write-Host "localhost:9092" -ForegroundColor Cyan

Write-Host "SQL Server:" -ForegroundColor White
Write-Host "localhost:1433" -ForegroundColor Cyan

Write-Host "`nDocker Containers:" -ForegroundColor Magenta

docker ps
