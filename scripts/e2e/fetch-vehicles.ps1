$loginUrl = 'http://localhost:5057/api/Auth/login'
$email = 'e2e_test_user@example.local'
$password = 'E2EPass123!'

# Try login
try {
    $resp = Invoke-RestMethod -Uri $loginUrl -Method Post -Body (ConvertTo-Json @{ Email = $email; Password = $password }) -ContentType 'application/json' -ErrorAction Stop
    $token = $resp.Token
    Write-Output "Login succeeded for $email"
} catch {
    # Try register then login
    $registerUrl = 'http://localhost:5057/api/Auth/register'
    try {
        Invoke-RestMethod -Uri $registerUrl -Method Post -Body (ConvertTo-Json @{ FullName = 'E2E User'; Email = $email; Password = $password; Role = 'Dispatcher' }) -ContentType 'application/json' -ErrorAction Stop
        Write-Output "Registered $email"
        $resp = Invoke-RestMethod -Uri $loginUrl -Method Post -Body (ConvertTo-Json @{ Email = $email; Password = $password }) -ContentType 'application/json' -ErrorAction Stop
        $token = $resp.Token
        Write-Output "Login succeeded after register for $email"
    } catch {
        Write-Error "Auth failed: $($_.Exception.Message)"
        exit 2
    }
}

if (-not $token) {
    Write-Error 'No token received'
    exit 2
}

# Call gateway with token
try {
    $vehicles = Invoke-RestMethod -Uri 'http://localhost:5000/fleet/Vehicles' -Headers @{ Authorization = "Bearer $token" } -Method Get -ErrorAction Stop
    $vehicles | ConvertTo-Json -Depth 5
} catch {
    Write-Error "Failed to fetch vehicles: $($_.Exception.Message)"
    exit 3
}
