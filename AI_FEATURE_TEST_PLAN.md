# AI Analysis Feature - Comprehensive Test Plan

## Test Environment Setup

### Prerequisites
- [x] Tesseract OCR installed and in PATH
- [x] OpenAI API key configured in `appsettings.secrets.json`
- [x] SQL Server LocalDB running
- [x] Node.js and npm installed
- [x] .NET 9.0 SDK installed

### Environment Variables Check
```bash
# Verify Tesseract installation
tesseract --version

# Should output: tesseract 5.x.x
```

---

## Phase 1: Build & Startup Validation (5 minutes)

### Test 1.1: Backend Compilation
**Objective**: Ensure all AI components compile without errors

**Steps**:
```bash
cd C:\Users\User\source\repos\ATS
dotnet build ATS.sln --no-incremental
```

**Expected Result**:
- ✅ Build succeeds with 0 errors
- ⚠️ Warnings about nullable references are acceptable
- ⚠️ SixLabors.ImageSharp vulnerability warning is known

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 1.2: Backend Startup
**Objective**: Verify backend starts and AI services are registered

**Steps**:
```bash
cd src/ATS.HttpApi.Host
dotnet run
```

**Expected Result**:
- ✅ Application starts on `https://localhost:44328`
- ✅ Swagger UI accessible at `https://localhost:44328/swagger`
- ✅ No errors in console about missing AI configuration
- ✅ Log shows: "Configured AI with provider: OpenAI, model: gpt-4o-mini"

**Validation**:
- Check console output for errors
- Open browser to `https://localhost:44328/swagger`
- Look for `/api/app/ai-analysis` endpoints

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 1.3: Frontend Startup
**Objective**: Verify Angular application compiles and runs

**Steps**:
```bash
cd angular
npm start
```

**Expected Result**:
- ✅ Compilation succeeds
- ✅ Application accessible at `http://localhost:4200`
- ✅ No TypeScript errors
- ✅ Dashboard loads without errors

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 2: Configuration Validation (10 minutes)

### Test 2.1: OpenAI API Key Validation
**Objective**: Verify OpenAI credentials are correctly configured

**Steps**:
1. Open Swagger UI: `https://localhost:44328/swagger`
2. Authenticate as admin (use existing admin credentials)
3. Find `POST /api/app/ai-analysis/analyze-application`
4. Use an existing application ID from database
5. Execute request with:
```json
{
  "applicationId": "<existing-guid>",
  "forceReanalysis": true
}
```

**Expected Result**:
- ✅ Returns 200 OK (or 202 Accepted)
- ✅ No "API Key not configured" error
- ✅ No "Invalid API Key" error (401)
- ✅ Response contains analysis data or job ID

**Troubleshooting**:
- If 401 error: Check API key in `appsettings.secrets.json`
- If 429 error: OpenAI rate limit, wait and retry
- If 500 error: Check backend logs

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 2.2: AI Configuration Loading
**Objective**: Verify all AI settings are loaded correctly

**Steps**:
1. Check backend logs on startup
2. Look for configuration values

**Expected Log Entries**:
```
[INF] Configured AI Options:
  Provider: OpenAI
  Model: gpt-4o-mini
  Enabled: True
  MaxBudgetPerAnalysisCents: 5
```

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 3: Document Parsing Tests (15 minutes)

### Test 3.1: PDF Text Extraction
**Objective**: Verify PDF parsing works correctly

**Test Files Needed**:
- Create a simple PDF resume with text (not image-based)
- Name: `test-resume-text.pdf`

**Steps**:
1. Navigate to an active job's public page
2. Fill out application form
3. Upload `test-resume-text.pdf`
4. Submit application
5. Check backend logs

**Expected Result**:
- ✅ Log shows: "Using PdfTextExtractor for test-resume-text.pdf"
- ✅ Log shows: "Extracted X characters from PDF"
- ✅ No "Low text yield" warning (should be >100 chars)
- ✅ Application created successfully

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 3.2: DOCX Text Extraction
**Objective**: Verify Word document parsing works

**Test Files Needed**:
- Create a simple DOCX resume
- Name: `test-resume.docx`

**Steps**:
1. Navigate to an active job's public page
2. Fill out application form
3. Upload `test-resume.docx`
4. Submit application
5. Check backend logs

**Expected Result**:
- ✅ Log shows: "Using DocxTextExtractor for test-resume.docx"
- ✅ Log shows: "Extracted X characters from DOCX"
- ✅ Application created successfully

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 3.3: OCR Fallback (Image-based PDF)
**Objective**: Verify OCR kicks in for image-based PDFs

**Test Files Needed**:
- Create a PDF with scanned/image content (or screenshot saved as PDF)
- Name: `test-resume-image.pdf`

**Steps**:
1. Navigate to an active job's public page
2. Fill out application form
3. Upload `test-resume-image.pdf`
4. Submit application
5. Check backend logs

**Expected Result**:
- ✅ Log shows: "Low text yield from PDF, falling back to OCR"
- ✅ Log shows: "Starting OCR extraction for X pages"
- ✅ Application created successfully

**Note**: OCR may return placeholder text if not fully implemented

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 4: AI Analysis End-to-End Tests (20 minutes)

### Test 4.1: Automatic Analysis on Application Submit
**Objective**: Verify AI analysis is automatically triggered

**Steps**:
1. Ensure backend is running with logs visible
2. Navigate to active job: `http://localhost:4200/apply/<job-slug>`
3. Fill out application form with realistic data:
   - Name: John Doe
   - Email: john.doe@test.com
   - Phone: +1234567890
   - Skills: JavaScript, React, Node.js (matching job requirements)
   - Upload a real-looking resume PDF
4. Submit application
5. Monitor backend logs

**Expected Result**:
- ✅ Application submission succeeds
- ✅ Log shows: "Enqueued AI analysis for Application {ApplicationId}"
- ✅ Within 30 seconds, log shows: "Executing AI analysis for Application {ApplicationId}"
- ✅ Log shows: "AI analysis completed for Application {ApplicationId} with score X, band Y"
- ✅ No errors in logs

**Validation Queries**:
```sql
-- Check application was created
SELECT TOP 1 * FROM Applications ORDER BY CreationTime DESC;

-- Check AI score was saved (wait 30 seconds after submit)
SELECT Id, AIScore, AIMatchSummary, 
       LEN(AIAnalysisDetailsJson) as AnalysisLength,
       LEN(SkillMatchScoresJson) as SkillsLength
FROM Applications 
WHERE Id = '<your-application-id>';
```

**Expected Database State**:
- ✅ `AIScore` is NOT NULL (value between 0-100)
- ✅ `AIMatchSummary` contains text recommendation
- ✅ `AIAnalysisDetailsJson` contains JSON (length > 500)
- ✅ `SkillMatchScoresJson` contains JSON array

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 4.2: Manual Analysis via API
**Objective**: Verify manual analysis endpoint works

**Steps**:
1. Get an application ID from database (preferably one without AI score)
2. Open Swagger UI: `https://localhost:44328/swagger`
3. Authenticate as admin
4. Execute `POST /api/app/ai-analysis/analyze-application`:
```json
{
  "applicationId": "<guid>",
  "forceReanalysis": true
}
```
5. Wait for response

**Expected Result**:
- ✅ Returns 200 OK
- ✅ Response body contains:
  - `overallScore` (0-100)
  - `hireBand` ("Strong", "Consider", or "No")
  - `skillMatchScore`, `experienceScore`, `educationScore`
  - `strengths` array (not empty)
  - `recommendation` text
  - `aiProvider`: "OpenAI"
  - `modelVersion`: "gpt-4o-mini"

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 4.3: Scoring Accuracy Validation
**Objective**: Verify AI produces reasonable scores

**Test Cases**:

#### Test 4.3a: Strong Match
**Setup**:
- Job requires: JavaScript, React, 3+ years experience
- Resume contains: 5 years JavaScript, React expert, multiple projects

**Expected**:
- ✅ Score: 75-95
- ✅ Hire Band: "Strong" or "Consider"
- ✅ Strengths mention matching skills

**Status**: ⬜ Pass / ⬜ Fail

---

#### Test 4.3b: Weak Match
**Setup**:
- Job requires: Python, Django, 5+ years experience
- Resume contains: Junior developer, 1 year experience, different tech stack

**Expected**:
- ✅ Score: 20-50
- ✅ Hire Band: "No" or "Consider"
- ✅ Risk flags mention skill gaps or experience mismatch

**Status**: ⬜ Pass / ⬜ Fail

---

#### Test 4.3c: Partial Match
**Setup**:
- Job requires: Full-stack (React + Node.js), 3 years
- Resume contains: 3 years React experience, no backend

**Expected**:
- ✅ Score: 50-70
- ✅ Hire Band: "Consider"
- ✅ Strengths mention React
- ✅ Risk flags mention missing backend skills

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 4.4: Batch Analysis
**Objective**: Verify batch analysis endpoint

**Steps**:
1. Get a job ID with multiple applications
2. Execute `POST /api/app/ai-analysis/update-rankings/{jobId}`
3. Monitor logs

**Expected Result**:
- ✅ Returns 200 OK
- ✅ Log shows: "Enqueued X applications for re-ranking for Job {JobId}"
- ✅ Multiple background jobs execute
- ✅ All applications for that job get AI scores

**Validation**:
```sql
SELECT COUNT(*) as Total,
       COUNT(AIScore) as Analyzed,
       AVG(AIScore) as AvgScore
FROM Applications
WHERE JobId = '<job-id>';
```

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 4.5: Ranked Applications Query
**Objective**: Verify ranking endpoint returns sorted results

**Steps**:
1. Ensure job has multiple analyzed applications
2. Execute `POST /api/app/ai-analysis/get-ranked-applications`:
```json
{
  "jobId": "<guid>",
  "topCount": 10,
  "minScore": 60
}
```

**Expected Result**:
- ✅ Returns 200 OK
- ✅ Returns array of applications
- ✅ Applications sorted by AIScore descending
- ✅ All returned applications have AIScore >= 60
- ✅ Maximum 10 results returned

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 5: UI Integration Tests (15 minutes)

### Test 5.1: Dashboard AI Score Display
**Objective**: Verify AI scores appear in dashboard

**Steps**:
1. Login as admin
2. Navigate to dashboard: `http://localhost:4200/dashboard`
3. Locate "Recent Applications" table
4. Find application with AI score

**Expected Result**:
- ✅ "AI Score" column exists
- ✅ Scores displayed as percentages (e.g., "85%")
- ✅ Color coding works:
  - Green badge for 80-100
  - Orange badge for 60-79
  - Red badge for 0-59
- ✅ Hover shows full score value

**Visual Check**:
- Take screenshot of dashboard with AI scores

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 5.2: Candidate Detail AI Score
**Objective**: Verify AI score appears on candidate detail page

**Steps**:
1. Navigate to candidates list: `http://localhost:4200/candidates`
2. Click "View" on a candidate with applications
3. Check right sidebar

**Expected Result**:
- ✅ "AI Score" section exists in right sidebar
- ✅ Score displayed as large number with percentage
- ✅ Color coding matches score:
  - Green for high scores
  - Orange for medium
  - Red for low
- ✅ Label says "Overall Match Score"

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 5.3: Application Success Flow
**Objective**: Verify complete user journey from application to score display

**Steps**:
1. As candidate, submit application with resume
2. Note the application ID from success page
3. Wait 30 seconds
4. Login as admin
5. Navigate to dashboard
6. Find the application in "Recent Applications"

**Expected Result**:
- ✅ Application appears in list
- ✅ AI Score is populated (not 0 or null)
- ✅ Score badge is visible and colored
- ✅ Clicking "View" shows candidate detail with score

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 6: Error Handling & Edge Cases (15 minutes)

### Test 6.1: Missing Resume
**Objective**: Verify graceful handling when no resume uploaded

**Steps**:
1. Submit application without uploading resume
2. Check if analysis still runs

**Expected Result**:
- ✅ Application submission succeeds
- ✅ Background job runs
- ✅ Analysis completes with lower score (due to missing resume)
- ✅ No errors in logs

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 6.2: Corrupted/Invalid File
**Objective**: Verify handling of unparseable files

**Steps**:
1. Create a text file and rename it to `.pdf`
2. Try to upload as resume
3. Submit application

**Expected Result**:
- ✅ Upload validation catches invalid file (frontend)
- OR
- ✅ Backend logs error but doesn't crash
- ✅ Application still created
- ✅ Analysis runs with available data (no resume text)

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 6.3: OpenAI API Failure
**Objective**: Verify resilience to API failures

**Steps**:
1. Temporarily set invalid API key in `appsettings.secrets.json`
2. Restart backend
3. Submit application
4. Check logs

**Expected Result**:
- ✅ Background job attempts analysis
- ✅ Error logged: "Failed to call OpenAI API"
- ✅ Application still exists in database
- ✅ AIScore remains NULL
- ✅ System doesn't crash

**Restore**:
- Set correct API key back
- Restart backend

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 6.4: Rate Limiting
**Objective**: Verify concurrency controls work

**Steps**:
1. Submit 10 applications rapidly (within 10 seconds)
2. Monitor logs

**Expected Result**:
- ✅ All applications enqueued
- ✅ Maximum 4 analyses run concurrently (ConcurrencyLimit setting)
- ✅ Remaining jobs wait in queue
- ✅ All eventually complete

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 6.5: Re-analysis (Force Flag)
**Objective**: Verify force re-analysis works

**Steps**:
1. Get application ID with existing AI score
2. Note current score
3. Execute analysis with `forceReanalysis: true`
4. Check if score updates

**Expected Result**:
- ✅ Analysis runs even though score exists
- ✅ New score may differ slightly (AI non-determinism)
- ✅ Timestamp updates

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 7: Performance & Load Tests (10 minutes)

### Test 7.1: Analysis Speed
**Objective**: Measure typical analysis time

**Steps**:
1. Submit application with medium-length resume (~2 pages)
2. Note timestamp when "Enqueued" log appears
3. Note timestamp when "Completed" log appears
4. Calculate duration

**Expected Result**:
- ✅ Analysis completes in 10-30 seconds
- ✅ No timeout errors

**Actual Time**: _______ seconds

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 7.2: Token Usage Validation
**Objective**: Verify token counts are reasonable

**Steps**:
1. Check logs for token usage after analysis
2. Look for: "Tokens used: Input=X, Output=Y"

**Expected Result**:
- ✅ Input tokens: 1,500-3,000 (typical resume + job)
- ✅ Output tokens: 300-800 (structured response)
- ✅ Total cost < 5 cents (MaxBudgetPerAnalysisCents)

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 7.3: Database Performance
**Objective**: Verify no performance degradation

**Steps**:
```sql
-- Check query performance
SET STATISTICS TIME ON;

SELECT TOP 10 
    a.Id, a.AIScore, c.FullName, j.Title
FROM Applications a
JOIN Candidates c ON a.CandidateId = c.Id
JOIN Jobs j ON a.JobId = j.Id
WHERE a.AIScore IS NOT NULL
ORDER BY a.AIScore DESC;

SET STATISTICS TIME OFF;
```

**Expected Result**:
- ✅ Query executes in < 100ms
- ✅ No table scans on large tables

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 8: Security & Privacy Tests (10 minutes)

### Test 8.1: API Key Security
**Objective**: Verify API key is not exposed

**Steps**:
1. Open browser DevTools (F12)
2. Navigate through application
3. Check Network tab for API calls
4. Check Sources tab for JavaScript files

**Expected Result**:
- ✅ API key NOT visible in any frontend code
- ✅ API key NOT in any HTTP responses
- ✅ API key NOT in Swagger UI

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 8.2: Permission Checks
**Objective**: Verify only admins can trigger manual analysis

**Steps**:
1. Logout
2. Try to access `/api/app/ai-analysis/analyze-application` without auth
3. Login as candidate (non-admin)
4. Try to access same endpoint

**Expected Result**:
- ✅ Unauthenticated: 401 Unauthorized
- ✅ Candidate role: 403 Forbidden
- ✅ Admin role: 200 OK

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 8.3: Data Privacy
**Objective**: Verify sensitive data handling

**Steps**:
1. Check logs after analysis
2. Look for PII (emails, phone numbers, addresses)

**Expected Result**:
- ✅ Full resume text NOT logged
- ✅ Candidate email NOT in logs (or redacted)
- ✅ Only analysis results logged

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 9: Integration Tests (15 minutes)

### Test 9.1: Full Recruitment Flow
**Objective**: Test complete candidate journey with AI

**Steps**:
1. **Candidate applies**:
   - Navigate to public job page
   - Submit application with resume
   - Verify success page

2. **AI analyzes** (automatic):
   - Wait 30 seconds
   - Verify background job completes

3. **Recruiter reviews**:
   - Login as admin
   - View dashboard
   - See AI score
   - Click to view candidate detail
   - See full candidate profile with AI score

4. **Recruiter filters**:
   - Use ranked applications API to get top candidates
   - Verify sorting by AI score

**Expected Result**:
- ✅ All steps complete without errors
- ✅ AI score visible at every stage
- ✅ Recruiter can make informed decisions

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 9.2: Multiple Jobs Scenario
**Objective**: Verify AI works correctly with multiple jobs

**Steps**:
1. Create 3 different jobs with different requirements
2. Submit 2 applications to each job
3. Verify each gets analyzed against correct job

**Expected Result**:
- ✅ Each application analyzed against its job's requirements
- ✅ Scores differ based on job fit
- ✅ No cross-contamination of job requirements

**Validation**:
```sql
SELECT j.Title, a.Id, a.AIScore, a.AIMatchSummary
FROM Applications a
JOIN Jobs j ON a.JobId = j.Id
WHERE a.AIScore IS NOT NULL
ORDER BY j.Title, a.AIScore DESC;
```

**Status**: ⬜ Pass / ⬜ Fail

---

## Phase 10: Regression Tests (10 minutes)

### Test 10.1: Existing Features Still Work
**Objective**: Verify AI addition didn't break existing functionality

**Checklist**:
- ⬜ Job creation still works
- ⬜ Job editing still works
- ⬜ Application submission (without AI) still works
- ⬜ Candidate list loads
- ⬜ Dashboard loads
- ⬜ User authentication works
- ⬜ Role-based permissions work

**Status**: ⬜ Pass / ⬜ Fail

---

### Test 10.2: Database Migrations
**Objective**: Verify no migration issues

**Steps**:
```bash
cd src/ATS.DbMigrator
dotnet run
```

**Expected Result**:
- ✅ Migrator runs successfully
- ✅ No errors about missing columns
- ✅ All AI-related columns exist (AIScore, AIAnalysisDetailsJson, etc.)

**Status**: ⬜ Pass / ⬜ Fail

---

## Test Results Summary

### Overall Statistics
- **Total Tests**: 40
- **Passed**: _____
- **Failed**: _____
- **Skipped**: _____
- **Pass Rate**: _____%

### Critical Issues Found
1. _______________________________________________
2. _______________________________________________
3. _______________________________________________

### Non-Critical Issues Found
1. _______________________________________________
2. _______________________________________________
3. _______________________________________________

### Performance Metrics
- **Average Analysis Time**: _____ seconds
- **Average Token Usage**: _____ tokens
- **Average Cost per Analysis**: $_____ 

### Recommendations
1. _______________________________________________
2. _______________________________________________
3. _______________________________________________

---

## Sign-Off

**Tested By**: _____________________
**Date**: _____________________
**Environment**: Development / Staging / Production
**Build Version**: _____________________

**Approval**: ⬜ Approved for Production / ⬜ Needs Fixes

---

## Appendix A: Test Data Templates

### Sample Resume (Plain Text for Testing)
```
John Doe
Senior Software Engineer
Email: john.doe@example.com
Phone: +1-555-0123

SUMMARY
Experienced full-stack developer with 5+ years building scalable web applications.
Proficient in JavaScript, React, Node.js, and cloud technologies.

EXPERIENCE
Senior Software Engineer | Tech Corp | 2020-Present
- Led development of React-based dashboard serving 10K+ users
- Implemented Node.js microservices architecture
- Reduced page load time by 40% through optimization

Software Engineer | StartupXYZ | 2018-2020
- Built RESTful APIs using Express.js
- Developed responsive UI components with React
- Collaborated with cross-functional teams

EDUCATION
BS Computer Science | University of Technology | 2018

SKILLS
JavaScript, React, Node.js, TypeScript, MongoDB, PostgreSQL, AWS, Docker
```

### Sample Job Requirements
```
Title: Senior Full-Stack Developer
Required Skills: JavaScript, React, Node.js, 5+ years experience
Preferred Skills: TypeScript, AWS, Docker
Location: Remote
Employment Type: Full-time
Experience Level: Senior
```

---

## Appendix B: SQL Queries for Validation

### Check AI Analysis Status
```sql
-- Applications with AI scores
SELECT 
    COUNT(*) as TotalApplications,
    COUNT(AIScore) as AnalyzedApplications,
    AVG(CAST(AIScore as FLOAT)) as AverageScore,
    MIN(AIScore) as MinScore,
    MAX(AIScore) as MaxScore
FROM Applications;

-- Score distribution
SELECT 
    CASE 
        WHEN AIScore >= 80 THEN 'Strong (80-100)'
        WHEN AIScore >= 60 THEN 'Consider (60-79)'
        WHEN AIScore < 60 THEN 'No (<60)'
        ELSE 'Not Analyzed'
    END as HireBand,
    COUNT(*) as Count
FROM Applications
GROUP BY 
    CASE 
        WHEN AIScore >= 80 THEN 'Strong (80-100)'
        WHEN AIScore >= 60 THEN 'Consider (60-79)'
        WHEN AIScore < 60 THEN 'No (<60)'
        ELSE 'Not Analyzed'
    END;

-- Recent analyses
SELECT TOP 10
    a.Id,
    c.FirstName + ' ' + c.LastName as CandidateName,
    j.Title as JobTitle,
    a.AIScore,
    LEFT(a.AIMatchSummary, 100) as Summary,
    a.CreationTime,
    a.LastModificationTime
FROM Applications a
JOIN Candidates c ON a.CandidateId = c.Id
JOIN Jobs j ON a.JobId = j.Id
WHERE a.AIScore IS NOT NULL
ORDER BY a.LastModificationTime DESC;
```

### Check Background Job Status
```sql
-- If using ABP Background Jobs table
SELECT TOP 20 *
FROM AbpBackgroundJobs
WHERE JobName LIKE '%AnalyzeApplication%'
ORDER BY CreationTime DESC;
```

---

## Appendix C: Common Issues & Solutions

| Issue | Symptom | Solution |
|-------|---------|----------|
| API Key Invalid | 401 error from OpenAI | Verify key in appsettings.secrets.json |
| Rate Limit Hit | 429 error | Wait 60 seconds, reduce ConcurrencyLimit |
| Tesseract Not Found | OCR fails | Add Tesseract to PATH, restart |
| Background Job Not Running | No AI scores | Check AbpBackgroundJobOptions.IsJobExecutionEnabled |
| Null Reference in Parsing | Exception in logs | Check file format, add null checks |
| Slow Analysis | >60 seconds | Check OpenAI API status, reduce MaxTokens |
| Missing Scores in UI | Scores don't display | Check database, refresh page, check permissions |
| CORS Error | Frontend can't call API | Verify AngularUrl in appsettings.json |

---

**End of Test Plan**

