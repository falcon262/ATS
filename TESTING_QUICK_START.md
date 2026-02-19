# AI Feature Testing - Quick Start Guide

## Pre-Test Validation (5 minutes)

### Step 1: Run Validation Script
```powershell
cd C:\Users\User\source\repos\ATS
.\test-ai-setup.ps1
```

This will check:
- ✅ Tesseract OCR installation
- ✅ OpenAI API key configuration
- ✅ AI settings in appsettings.json
- ✅ Required NuGet packages
- ✅ Database schema

**Fix any issues before proceeding!**

---

## Quick Smoke Test (10 minutes)

### Step 1: Start Backend
```powershell
cd src\ATS.HttpApi.Host
dotnet run
```

Wait for: `Now listening on: https://localhost:44328`

### Step 2: Start Frontend (New Terminal)
```powershell
cd angular
npm start
```

Wait for: `Compiled successfully`

### Step 3: Create Test Resume
Create a file named `test-resume.pdf` with this content (use Word/Google Docs, save as PDF):

```
John Smith
Senior Software Engineer
john.smith@test.com | +1-555-0100

SUMMARY
Experienced full-stack developer with 5 years of experience in JavaScript, React, and Node.js.
Proven track record of building scalable web applications.

EXPERIENCE
Senior Developer | Tech Corp | 2020-Present
- Built React-based dashboard serving 10,000+ users
- Implemented Node.js microservices
- Led team of 3 developers

Software Engineer | StartupXYZ | 2018-2020
- Developed RESTful APIs with Express.js
- Created responsive UI components with React
- Collaborated with product team

EDUCATION
BS Computer Science | State University | 2018

SKILLS
JavaScript, React, Node.js, TypeScript, MongoDB, PostgreSQL, AWS, Docker, Git
```

### Step 4: Submit Test Application

1. **Find an Active Job**:
   - Login as admin: `http://localhost:4200`
   - Go to Jobs → Find a job in "Active" status
   - Click "View" → Copy the public link (e.g., `/apply/senior-developer-xyz`)
   - Logout

2. **Submit Application**:
   - Navigate to: `http://localhost:4200/apply/<job-slug>`
   - Fill out form:
     - First Name: John
     - Last Name: Smith
     - Email: john.smith@test.com
     - Phone: +1-555-0100
     - Current Job Title: Senior Software Engineer
     - Years of Experience: 5
     - Skills: JavaScript, React, Node.js (type and press Enter after each)
     - Upload: `test-resume.pdf`
   - Click "Submit Application"
   - ✅ Should see success message with application ID

### Step 5: Monitor Backend Logs

Watch the backend console for these messages (within 30 seconds):

```
[INF] Enqueued AI analysis for Application {ApplicationId}
[INF] Executing AI analysis for Application {ApplicationId}
[INF] Using PdfTextExtractor for test-resume.pdf
[INF] Extracted 450 characters from PDF
[INF] Calling OpenAI API with model gpt-4o-mini
[INF] AI analysis completed for Application {ApplicationId} with score 82, band Strong
```

**If you see errors**:
- Check OpenAI API key is valid
- Check Tesseract is in PATH
- Check backend logs for details

### Step 6: Verify in Dashboard

1. Login as admin: `http://localhost:4200`
2. Go to Dashboard
3. Find "Recent Applications" table
4. Look for John Smith's application

**Expected Results**:
- ✅ AI Score column shows a percentage (e.g., "82%")
- ✅ Score badge is colored (green for 80+, orange for 60-79, red for <60)
- ✅ Clicking "View" shows candidate detail with AI score in sidebar

### Step 7: Verify in Database

```sql
-- Open SQL Server Management Studio or Azure Data Studio
-- Connect to: (LocalDb)\MSSQLLocalDB
-- Database: ATS

SELECT TOP 1
    a.Id,
    c.FirstName + ' ' + c.LastName as CandidateName,
    j.Title as JobTitle,
    a.AIScore,
    a.AIMatchSummary,
    LEN(a.AIAnalysisDetailsJson) as AnalysisJsonLength,
    a.CreationTime,
    a.LastModificationTime
FROM Applications a
JOIN Candidates c ON a.CandidateId = c.Id
JOIN Jobs j ON a.JobId = j.Id
ORDER BY a.CreationTime DESC;
```

**Expected Results**:
- ✅ `AIScore` is NOT NULL (value between 0-100)
- ✅ `AIMatchSummary` contains text (e.g., "Strong candidate with relevant experience...")
- ✅ `AnalysisJsonLength` > 500 (full JSON analysis stored)
- ✅ `LastModificationTime` is recent (updated after analysis)

---

## ✅ Success Criteria

If all of the following are true, the AI feature is working:

1. ✅ Application submitted successfully
2. ✅ Backend logs show "Enqueued AI analysis"
3. ✅ Backend logs show "AI analysis completed with score X"
4. ✅ Dashboard displays AI score with colored badge
5. ✅ Candidate detail page shows AI score
6. ✅ Database has AIScore and AIAnalysisDetailsJson populated

---

## 🔧 Troubleshooting

### Issue: "Enqueued" but no "Completed" log

**Possible Causes**:
1. Background jobs not enabled
2. OpenAI API error
3. Resume parsing failed

**Solutions**:
```powershell
# Check if background jobs are enabled
# In ATSApplicationModule.cs, verify:
Configure<AbpBackgroundJobOptions>(options =>
{
    options.IsJobExecutionEnabled = true;  # Must be true
});

# Check OpenAI API key
# Test manually:
curl https://api.openai.com/v1/models `
  -H "Authorization: Bearer sk-proj-..." `
  -H "Content-Type: application/json"

# Should return list of models (not 401 error)
```

### Issue: "OpenAI API Key is not configured"

**Solution**:
1. Check `src\ATS.HttpApi.Host\appsettings.secrets.json` exists
2. Verify it contains:
```json
{
  "OpenAI": {
    "ApiKey": "sk-proj-...",
    "OrganizationId": "org-...",
    "ProjectId": "proj_..."
  }
}
```
3. Restart backend

### Issue: AI Score is 0 or NULL

**Possible Causes**:
1. Analysis hasn't completed yet (wait 30 seconds)
2. Background job failed
3. Database not updated

**Solutions**:
1. Check backend logs for errors
2. Refresh dashboard page
3. Check database directly (SQL query above)

### Issue: "Low text yield from PDF, falling back to OCR"

**This is normal** for image-based PDFs. If OCR fails:
1. Verify Tesseract is installed: `tesseract --version`
2. Check Tesseract is in PATH
3. Restart backend after installing Tesseract

### Issue: Rate Limit Error (429)

**Solution**:
- Wait 60 seconds
- Reduce `ConcurrencyLimit` in appsettings.json from 4 to 2
- Check OpenAI account has credits

---

## 📊 Understanding the AI Score

### Score Ranges
- **80-100 (Green)**: Strong match - High priority candidate
  - Most/all required skills present
  - Experience level matches
  - Education relevant
  
- **60-79 (Orange)**: Consider - Worth interviewing
  - Some required skills present
  - Experience close to requirements
  - May have transferable skills
  
- **0-59 (Red)**: Weak match - Significant gaps
  - Missing key required skills
  - Experience level mismatch
  - Education not relevant

### What the AI Evaluates

1. **Technical Skills (40%)**:
   - Does resume mention required skills?
   - How many years of experience with each skill?
   - Depth of expertise indicated?

2. **Experience (25%)**:
   - Total years of relevant experience
   - Seniority level (Junior/Mid/Senior)
   - Career progression
   - Project complexity

3. **Education (15%)**:
   - Degree level (BS/MS/PhD)
   - Field of study relevance
   - Certifications

4. **Cultural Fit (10%)**:
   - Leadership indicators
   - Collaboration mentions
   - Communication skills

5. **Logistics (10%)**:
   - Location match
   - Remote work preference
   - Salary expectations
   - Availability

---

## 🎯 Next Steps

### For Full Testing
See `AI_FEATURE_TEST_PLAN.md` for comprehensive test cases covering:
- Document parsing (PDF, DOCX, OCR)
- Error handling
- Performance testing
- Security validation
- Edge cases

### For Production Deployment
1. ✅ Complete all tests in test plan
2. ✅ Monitor OpenAI costs for 1 week
3. ✅ Adjust `MaxBudgetPerAnalysisCents` if needed
4. ✅ Set up monitoring/alerts for failed analyses
5. ✅ Document for team (how to interpret scores)

---

## 📞 Support

If you encounter issues:

1. **Check Logs**: `src\ATS.HttpApi.Host\Logs\logs-*.txt`
2. **Check Backend Console**: Real-time errors
3. **Check Browser Console**: F12 → Console tab
4. **Check Database**: Run SQL queries to verify data
5. **Test OpenAI API**: Use curl to test API key directly

**Common Log Locations**:
- Backend: Console output + `src\ATS.HttpApi.Host\Logs\`
- Frontend: Browser DevTools → Console
- Database: SQL Server Management Studio

---

**Good luck with testing! 🚀**

