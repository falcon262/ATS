# TalentFlow ATS - API Testing Helper Script
# This script helps you test API endpoints and update job statuses

# Configuration
$baseUrl = "https://localhost:44328"
$username = "admin"  # Change this to your admin username
$password = "1q2w3E*"  # Change this to your admin password

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "TalentFlow ATS - API Test Helper" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Ignore SSL certificate errors for localhost testing
add-type @"
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    public class TrustAllCertsPolicy : ICertificatePolicy {
        public bool CheckValidationResult(
            ServicePoint svcPoint, X509Certificate certificate,
            WebRequest request, int certificateProblem) {
            return true;
        }
    }
"@
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

# Function to get access token
function Get-AccessToken {
    Write-Host "Logging in to get access token..." -ForegroundColor Yellow
    
    $tokenUrl = "$baseUrl/connect/token"
    $body = @{
        grant_type = "password"
        username = $username
        password = $password
        client_id = "ATS_App"
        scope = "ATS"
    }
    
    try {
        $response = Invoke-RestMethod -Uri $tokenUrl -Method Post -Body $body -ContentType "application/x-www-form-urlencoded"
        Write-Host "✓ Login successful!" -ForegroundColor Green
        return $response.access_token
    }
    catch {
        Write-Host "✗ Login failed: $_" -ForegroundColor Red
        Write-Host "Please check your credentials in the script" -ForegroundColor Red
        exit 1
    }
}

# Function to get all jobs
function Get-AllJobs {
    param($token)
    
    Write-Host "`nFetching all jobs..." -ForegroundColor Yellow
    
    $headers = @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    }
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/api/app/job?MaxResultCount=100" -Method Get -Headers $headers
        return $response.items
    }
    catch {
        Write-Host "✗ Failed to fetch jobs: $_" -ForegroundColor Red
        return @()
    }
}

# Function to publish a job
function Publish-Job {
    param($token, $jobId)
    
    Write-Host "`nPublishing job $jobId..." -ForegroundColor Yellow
    
    $headers = @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    }
    
    $body = @{
        jobId = $jobId
    } | ConvertTo-Json
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/api/app/job/publish" -Method Post -Headers $headers -Body $body
        Write-Host "✓ Job published successfully!" -ForegroundColor Green
        Write-Host "  Status: $($response.status)" -ForegroundColor Cyan
        Write-Host "  Public Slug: $($response.publicSlug)" -ForegroundColor Cyan
        return $response
    }
    catch {
        Write-Host "✗ Failed to publish job: $_" -ForegroundColor Red
        return $null
    }
}

# Function to close a job
function Close-Job {
    param($token, $jobId)
    
    Write-Host "`nClosing job $jobId..." -ForegroundColor Yellow
    
    $headers = @{
        "Authorization" = "Bearer $token"
        "Content-Type" = "application/json"
    }
    
    try {
        $response = Invoke-RestMethod -Uri "$baseUrl/api/app/job/$jobId/close" -Method Post -Headers $headers
        Write-Host "✓ Job closed successfully!" -ForegroundColor Green
        Write-Host "  Status: $($response.status)" -ForegroundColor Cyan
        return $response
    }
    catch {
        Write-Host "✗ Failed to close job: $_" -ForegroundColor Red
        return $null
    }
}

# Function to display job info
function Show-JobInfo {
    param($job, $index)
    
    $statusText = switch ($job.status) {
        0 { "DRAFT" }
        1 { "ACTIVE" }
        2 { "CLOSED" }
        default { "UNKNOWN" }
    }
    
    $statusColor = switch ($job.status) {
        0 { "Gray" }
        1 { "Green" }
        2 { "Yellow" }
        default { "White" }
    }
    
    Write-Host "[$index] " -NoNewline
    Write-Host "$($job.title)" -ForegroundColor White -NoNewline
    Write-Host " - " -NoNewline
    Write-Host "$statusText" -ForegroundColor $statusColor -NoNewline
    Write-Host " (Applications: $($job.applicationCount))"
    Write-Host "    ID: $($job.id)"
    if ($job.publicSlug) {
        Write-Host "    Public Link: http://localhost:4200/apply/$($job.publicSlug)" -ForegroundColor Cyan
    }
}

# Main Script
Write-Host "Starting API helper..." -ForegroundColor Cyan
Write-Host ""

# Get access token
$token = Get-AccessToken

# Get all jobs
$jobs = Get-AllJobs -token $token

if ($jobs.Count -eq 0) {
    Write-Host "`nNo jobs found. Please create some jobs first through the UI." -ForegroundColor Yellow
    exit 0
}

Write-Host "`n==================================" -ForegroundColor Cyan
Write-Host "Available Jobs ($($jobs.Count))" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan

for ($i = 0; $i -lt $jobs.Count; $i++) {
    Show-JobInfo -job $jobs[$i] -index ($i + 1)
}

Write-Host "`n==================================" -ForegroundColor Cyan
Write-Host "Quick Actions" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host "This script will automatically:"
Write-Host "1. Publish the first DRAFT job → ACTIVE"
Write-Host "2. Close the first ACTIVE job → CLOSED"
Write-Host "3. Leave remaining jobs as-is"
Write-Host ""
Write-Host "Press Enter to continue or Ctrl+C to cancel..."
Read-Host

# Find and publish first draft job
$draftJobs = $jobs | Where-Object { $_.status -eq 0 }
if ($draftJobs.Count -gt 0) {
    $jobToPublish = $draftJobs[0]
    Write-Host "`nAction 1: Publishing first DRAFT job..." -ForegroundColor Cyan
    Write-Host "Job: $($jobToPublish.title)" -ForegroundColor White
    $published = Publish-Job -token $token -jobId $jobToPublish.id
    
    if ($published) {
        Write-Host "✓ Public link: http://localhost:4200/apply/$($published.publicSlug)" -ForegroundColor Green
    }
} else {
    Write-Host "`nNo DRAFT jobs found to publish." -ForegroundColor Yellow
}

# Find and close first active job (but not the one we just published)
Write-Host "`nRefetching jobs..." -ForegroundColor Yellow
$jobs = Get-AllJobs -token $token
$activeJobs = $jobs | Where-Object { $_.status -eq 1 }

if ($activeJobs.Count -gt 1) {
    # Close the second active job (not the one we just published)
    $jobToClose = $activeJobs[1]
    Write-Host "`nAction 2: Closing an ACTIVE job..." -ForegroundColor Cyan
    Write-Host "Job: $($jobToClose.title)" -ForegroundColor White
    $closed = Close-Job -token $token -jobId $jobToClose.id
} elseif ($activeJobs.Count -eq 1) {
    Write-Host "`nOnly one ACTIVE job found. Keeping it active for testing." -ForegroundColor Yellow
} else {
    Write-Host "`nNo ACTIVE jobs found to close." -ForegroundColor Yellow
}

# Show final status
Write-Host "`n==================================" -ForegroundColor Cyan
Write-Host "Final Job Status" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan

$jobs = Get-AllJobs -token $token

$draftCount = ($jobs | Where-Object { $_.status -eq 0 }).Count
$activeCount = ($jobs | Where-Object { $_.status -eq 1 }).Count
$closedCount = ($jobs | Where-Object { $_.status -eq 2 }).Count

Write-Host "DRAFT jobs: $draftCount" -ForegroundColor Gray
Write-Host "ACTIVE jobs: $activeCount" -ForegroundColor Green
Write-Host "CLOSED jobs: $closedCount" -ForegroundColor Yellow
Write-Host ""

for ($i = 0; $i -lt $jobs.Count; $i++) {
    Show-JobInfo -job $jobs[$i] -index ($i + 1)
}

Write-Host "`n==================================" -ForegroundColor Cyan
Write-Host "✓ Done!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "You can now test the application with jobs in different statuses." -ForegroundColor White
Write-Host "Active jobs have public links that you can share for applications." -ForegroundColor White
Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host

