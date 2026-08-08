$conn = Get-NetTCPConnection -LocalPort 5081 -ErrorAction SilentlyContinue | Select-Object -First 1
if ($conn) {
    $procId = $conn.OwningProcess
    Write-Output ("Found process listening on 5081: {0}" -f $procId)
    try { Stop-Process -Id $procId -Force; Write-Output ("Stopped process {0}" -f $procId) } catch { Write-Output ("Failed to stop process {0}: {1}" -f $procId, $_.Exception.Message) }
} else {
    Write-Output "No process listening on 5081"
}