Param(
    [string]$ConfigPath = "scripts/e2e/config.json",
    [int]$TimeoutSec = 10
)

# Environment variable E2E_BASE_URL can override the host used in endpoints
if (Test-Path $ConfigPath -PathType Leaf) {
    $config = Get-Content $ConfigPath -Raw | ConvertFrom-Json
} else {
    Write-Error "Config file not found: $ConfigPath"
    exit 2
}

$baseOverride = $Env:E2E_BASE_URL

$results = @()
$allPassed = $true

foreach ($ep in $config.endpoints) {
    $url = $ep.url
    if ($baseOverride) {
        try {
            $uri = [System.Uri]$url
            $newBase = [System.Uri]$baseOverride
            $url = ($newBase.Scheme + '://' + $newBase.Host + (if ($newBase.Port -ne 80 -and $newBase.Port -ne 443) { ':' + $newBase.Port } else { '' }) + $uri.AbsolutePath + $uri.Query)
        } catch {
            # ignore malformed override
        }
    }

    $start = Get-Date
    try {
        $resp = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec $TimeoutSec -ErrorAction Stop
        $status = $resp.StatusCode
        $success = ($status -ge 200 -and $status -lt 300)
        $elapsed = (Get-Date) - $start
        $results += [pscustomobject]@{
            name = $ep.name
            url = $url
            status = $status
            success = $success
            timeMs = [int]$elapsed.TotalMilliseconds
        }
        if (-not $success -and $ep.required) { $allPassed = $false }
        Write-Output "[OK] $($ep.name) -> $status ($($elapsed.TotalMilliseconds)ms)"
    } catch {
        $elapsed = (Get-Date) - $start
        $errorMsg = $_.Exception.Message
        $results += [pscustomobject]@{
            name = $ep.name
            url = $url
            status = 0
            success = $false
            timeMs = [int]$elapsed.TotalMilliseconds
            error = $errorMsg
        }
        Write-Output "[FAIL] $($ep.name) -> $errorMsg"
        if ($ep.required) { $allPassed = $false }
    }
}

# Summary
Write-Output "\nE2E Summary:"
$results | Format-Table -AutoSize

if ($allPassed) {
    Write-Output "All required checks passed."
    exit 0
} else {
    Write-Output "One or more required checks failed."
    exit 1
}
