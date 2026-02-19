# AI Feature Setup Validation Script
# Run this script to validate your AI setup before testing

Write-Host "=== AI Feature Setup Validation ===" -ForegroundColor Cyan
Write-Host ""

# Test 1: Check Tesseract Installation
Write-Host "[1/5] Checking Tesseract OCR..." -ForegroundColor Yellow
try {
    $tesseractVersion = tesseract --version 2>&1 | Select-String "tesseract" | Select-Object -First 1
    if ($tesseractVersion) {
        Write-Host "  Pass: Tesseract installed: $tesseractVersion" -ForegroundColor Green
    } else {
        Write-Host "  Fail: Tesseract not found in PATH" -ForegroundColor Red
        Write-Host "    Install: winget install --id=UB-Mannheim.TesseractOCR" -ForegroundColor Yellow
    }
} catch {
    Write-Host "  Fail: Tesseract not found" -ForegroundColor Red
}
Write-Host ""

# Test 2: Check OpenAI Configuration
Write-Host "[2/5] Checking OpenAI Configuration..." -ForegroundColor Yellow
try {
    $secretsPath = "src\ATS.HttpApi.Host\appsettings.secrets.json"
    if (Test-Path $secretsPath) {
        $secrets = Get-Content $secretsPath | ConvertFrom-Json
        if ($secrets.OpenAI.ApiKey) {
            $keyPreview = $secrets.OpenAI.ApiKey.Substring(0, [Math]::Min(15, $secrets.OpenAI.ApiKey.Length)) + "..."
            Write-Host "  Pass: API Key configured: $keyPreview" -ForegroundColor Green
            Write-Host "  Pass: Organization ID: $($secrets.OpenAI.OrganizationId)" -ForegroundColor Green
            Write-Host "  Pass: Project ID: $($secrets.OpenAI.ProjectId)" -ForegroundColor Green
        } else {
            Write-Host "  Fail: API Key not found in secrets file" -ForegroundColor Red
        }
    } else {
        Write-Host "  Fail: appsettings.secrets.json not found" -ForegroundColor Red
    }
} catch {
    Write-Host "  Fail: Error reading secrets file: $_" -ForegroundColor Red
}
Write-Host ""

# Test 3: Check AI Settings
Write-Host "[3/5] Checking AI Settings..." -ForegroundColor Yellow
try {
    $settingsPath = "src\ATS.HttpApi.Host\appsettings.json"
    if (Test-Path $settingsPath) {
        $settings = Get-Content $settingsPath | ConvertFrom-Json
        if ($settings.AI) {
            Write-Host "  Pass: AI Provider: $($settings.AI.Provider)" -ForegroundColor Green
            Write-Host "  Pass: Model: $($settings.AI.Model)" -ForegroundColor Green
            Write-Host "  Pass: Enabled: $($settings.AI.Enabled)" -ForegroundColor Green
            Write-Host "  Pass: Max Budget: $($settings.AI.MaxBudgetPerAnalysisCents) cents" -ForegroundColor Green
        } else {
            Write-Host "  Fail: AI configuration not found in appsettings.json" -ForegroundColor Red
        }
    } else {
        Write-Host "  Fail: appsettings.json not found" -ForegroundColor Red
    }
} catch {
    Write-Host "  Fail: Error reading settings: $_" -ForegroundColor Red
}
Write-Host ""

# Test 4: Check NuGet Packages
Write-Host "[4/5] Checking Required NuGet Packages..." -ForegroundColor Yellow
try {
    $csprojPath = "src\ATS.Application\ATS.Application.csproj"
    if (Test-Path $csprojPath) {
        $csproj = Get-Content $csprojPath -Raw
        $packages = @("OpenAI", "PdfSharpCore", "DocumentFormat.OpenXml", "Tesseract")
        foreach ($package in $packages) {
            if ($csproj -match $package) {
                Write-Host "  Pass: $package installed" -ForegroundColor Green
            } else {
                Write-Host "  Fail: $package not found" -ForegroundColor Red
            }
        }
    } else {
        Write-Host "  Fail: ATS.Application.csproj not found" -ForegroundColor Red
    }
} catch {
    Write-Host "  Fail: Error checking packages: $_" -ForegroundColor Red
}
Write-Host ""

# Test 5: Check Database
Write-Host "[5/5] Checking Database Schema..." -ForegroundColor Yellow
try {
    $connectionString = "Server=(LocalDb)\MSSQLLocalDB;Database=ATS;Trusted_Connection=True;TrustServerCertificate=true"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    
    $command = $connection.CreateCommand()
    $command.CommandText = "SELECT CASE WHEN EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Applications') AND name = 'AIScore') THEN 1 ELSE 0 END as HasAIScore, CASE WHEN EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Applications') AND name = 'AIAnalysisDetailsJson') THEN 1 ELSE 0 END as HasAnalysisJson"
    $reader = $command.ExecuteReader()
    if ($reader.Read()) {
        if ($reader.GetInt32(0) -eq 1) {
            Write-Host "  Pass: AIScore column exists" -ForegroundColor Green
        } else {
            Write-Host "  Fail: AIScore column missing - run migrations" -ForegroundColor Red
        }
        if ($reader.GetInt32(1) -eq 1) {
            Write-Host "  Pass: AIAnalysisDetailsJson column exists" -ForegroundColor Green
        } else {
            Write-Host "  Fail: AIAnalysisDetailsJson column missing - run migrations" -ForegroundColor Red
        }
    }
    $reader.Close()
    $connection.Close()
} catch {
    Write-Host "  Warning: Could not connect to database (this is OK if not started yet)" -ForegroundColor Yellow
    Write-Host "    Error: $_" -ForegroundColor DarkGray
}
Write-Host ""

# Summary
Write-Host "=== Validation Complete ===" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "1. If any checks failed, fix them before testing"
Write-Host "2. Start backend: cd src\ATS.HttpApi.Host; dotnet run"
Write-Host "3. Start frontend: cd angular; npm start"
Write-Host "4. Follow AI_FEATURE_TEST_PLAN.md for comprehensive testing"
Write-Host ""
Write-Host "Quick Test:" -ForegroundColor Yellow
Write-Host "- Navigate to an active job public page"
Write-Host "- Submit an application with a resume"
Write-Host "- Wait 30 seconds and check dashboard for AI score"
Write-Host ""
