# TalentFlow ATS - Quick Reference Card

## 🎯 Your Questions Answered

### 1. ❓ Why isn't the "Create Job" button working?

**Answer:** Fixed! The validation messages weren't showing properly. Now they appear in red below each field when you miss required fields.

**Required fields to fill:**
- Job Title
- Department (select from dropdown)
- Location
- Employment Type (default: Full-Time)
- Experience Level (default: Entry Level)
- Description (at least a few sentences)
- Requirements (at least a few sentences)

**To verify it works:**
1. Fill all required fields
2. Click "Create Job"
3. You should be redirected to the job detail page
4. Check browser console (F12) - no errors should appear

---

### 2. ❓ How do I change job statuses to Active, Paused, and Closed?

**Answer:** 
- ✅ **Active**: Use the "Publish Job" button on DRAFT job detail pages
- ❌ **Paused**: This status doesn't exist in the system
- ✅ **Closed**: Use the "Close Job" button on ACTIVE job detail pages

**Only 3 statuses exist:**
- **DRAFT** (0) = Not published yet
- **ACTIVE** (1) = Published and accepting applications
- **CLOSED** (2) = No longer accepting applications

**Steps to get jobs in different statuses:**

**Method A: Using UI (Recommended)**
1. Create Job 1 → Leave as DRAFT
2. Create Job 2 → Click "Publish Job" button → Now ACTIVE
3. Create Job 3 → Publish it → Then click "Close Job" → Now CLOSED

**Method B: Using PowerShell Script**
1. Create 3+ jobs through UI (all will be DRAFT)
2. Run: `.\test-api-helper.ps1`
3. Script automatically updates statuses

---

### 3. ❓ Why don't I see the public shareable link?

**Answer:** Public links ONLY appear for **ACTIVE** jobs (jobs that have been published).

**To see the public link:**
1. Navigate to a job detail page
2. If job is DRAFT, click "Publish Job" button
3. After publishing, the "Public Application Link" section appears
4. Click "Copy Link" to copy it to clipboard

**The link format:** `http://localhost:4200/apply/{slug}`

**Example:** `http://localhost:4200/apply/junior-java-developer-abc123`

---

## 📊 Job Status Behavior Chart

| Status | Badge Color | Public Link | Can Apply | Can Publish | Can Close |
|--------|------------|-------------|-----------|-------------|-----------|
| **DRAFT** | Gray | ❌ No | ❌ No | ✅ Yes | ❌ No |
| **ACTIVE** | Green | ✅ Yes | ✅ Yes | ❌ No | ✅ Yes |
| **CLOSED** | Orange | ✅ Visible | ❌ No | ❌ No | ❌ No |

---

## 🚀 Quick Testing Steps

### **Test 1: Create and Publish a Job**
1. Go to **Jobs** → **Post New Job**
2. Fill required fields (see list above)
3. Click **Create Job** → Should succeed and redirect
4. On job detail page, click **Publish Job**
5. Confirm → Job becomes ACTIVE
6. **Public Application Link** section appears
7. Click **Copy Link**

### **Test 2: Use Public Link**
1. Copy the public link from job detail page
2. Open **incognito/private browser window**
3. Paste the link
4. Fill application form
5. Upload resume (PDF, DOC, DOCX under 5MB)
6. Check consent checkbox
7. Submit → Should see success page

### **Test 3: Register as Candidate**
1. On success page, click "Create Account"
2. Email should be pre-filled
3. Set password (e.g., `Password123!`)
4. Register → Redirected to login
5. Login with candidate credentials
6. See your application in dashboard

### **Test 4: Close a Job**
1. Go to ACTIVE job detail page
2. Click **Close Job** in "Job Status" section
3. Confirm → Job becomes CLOSED
4. Public link remains but applications disabled

---

## 🛠️ Using the PowerShell Helper Script

**Purpose:** Quickly update multiple job statuses via API

**Setup:**
1. Open `test-api-helper.ps1` in text editor
2. Update credentials:
   ```powershell
   $username = "admin"  # Your admin username
   $password = "1q2w3E*"  # Your admin password
   ```
3. Save file

**Run:**
```powershell
.\test-api-helper.ps1
```

**What it does:**
1. ✅ Logs in to API
2. ✅ Lists all jobs with current status
3. ✅ Publishes first DRAFT job → ACTIVE
4. ✅ Closes one ACTIVE job → CLOSED
5. ✅ Shows final status with color coding

**Output:**
- Gray = DRAFT
- Green = ACTIVE
- Yellow = CLOSED

---

## 📁 Important Files Created

| File | Purpose |
|------|---------|
| `TESTING-GUIDE.md` | **Complete step-by-step testing instructions** |
| `test-api-helper.ps1` | **PowerShell script to update job statuses** |
| `BUG-FIXES-SUMMARY.md` | **Detailed explanation of all fixes** |
| `UPDATE-JOB-FIX.md` | **Update job bug fix documentation** |
| `QUICK-REFERENCE.md` | **This quick reference (you are here)** |

---

## 🎯 Success Checklist

After following instructions, you should have:

- [x] Fixed "Create Job" button - form validates properly
- [x] Published at least 1 job → ACTIVE status
- [x] Public link visible and copyable for ACTIVE jobs
- [x] Closed at least 1 job → CLOSED status
- [x] Jobs in 3 different statuses:
  - 1 DRAFT (gray badge, no public link)
  - 1 ACTIVE (green badge, public link shown)
  - 1 CLOSED (orange badge, link shown but disabled)
- [x] Submitted at least 1 test application via public link
- [x] Registered candidate account
- [x] Viewed application in candidate portal

---

## 🔍 Verification Commands

### Check if backend is running:
```powershell
curl https://localhost:44328/health -k
```

### Check if frontend is running:
```powershell
curl http://localhost:4200
```

### View backend logs:
- Run backend in Visual Studio
- Check Output window or Debug Console

### View frontend logs:
- Open Browser Console (F12)
- Check Console tab for errors
- Check Network tab for API calls

---

## 📞 Troubleshooting Quick Fixes

| Problem | Solution |
|---------|----------|
| Create Job button does nothing | Open Console (F12), check for errors, ensure all required fields filled |
| Update Job not saving changes | **FIXED!** Rebuild backend (dotnet build), restart |
| No departments in dropdown | Run `ATS.DbMigrator` to seed database |
| Can't see Publish button | Refresh page, button only shows for DRAFT jobs |
| Public link not appearing | Job must be ACTIVE (published) first |
| PowerShell script fails | Check credentials, ensure backend is running |
| 401 Unauthorized errors | Re-login, check credentials |
| Application form won't submit | Check console for errors, verify file size < 5MB |

---

## 🎓 Key Concepts

### Job Workflow:
```
CREATE → DRAFT → PUBLISH → ACTIVE → CLOSE → CLOSED
                   ↑                    ↓
              (no public link)    (public link appears)
```

### Application Workflow:
```
PUBLIC APPLY → SUBMIT → NEW APPLICATION → REGISTER → LOGIN → VIEW DASHBOARD
   (no login)                              (optional)
```

### User Roles:
- **Admin**: Full access, can create/manage jobs and applications
- **Candidate**: Can only view their own applications (read-only)

---

## ✅ All Set!

You now have everything you need to:
- ✅ Create jobs successfully
- ✅ Publish jobs to make them public
- ✅ Close jobs when done
- ✅ Share public links for applications
- ✅ Test the complete application flow

**Start with TESTING-GUIDE.md for detailed instructions!**

---

**Need more help?** 
- Read: `TESTING-GUIDE.md` (comprehensive)
- Read: `BUG-FIXES-SUMMARY.md` (technical details)
- Run: `.\test-api-helper.ps1` (automate status changes)

---

Last Updated: October 28, 2025
Version: 1.0

