# TalentFlow ATS - Comprehensive Testing Guide

## 🎯 Prerequisites

Before testing, ensure:
- ✅ Backend API is running on `https://localhost:44328` (or your configured port)
- ✅ Angular frontend is running on `http://localhost:4200`
- ✅ Database migrations have been applied (`ATS.DbMigrator`)
- ✅ You have admin credentials to login

---

## 🚀 Complete User Journey Testing

### **Journey 1: Job Posting Lifecycle (Admin)**

#### Step 1: Login
1. Navigate to `http://localhost:4200`
2. Login with admin credentials (default: `admin` / your password)
3. You should see the Dashboard

#### Step 2: Create a Job Posting
1. Click **"Jobs"** in the left sidebar
2. Click **"+ Post New Job"** button (top right)
3. Fill in the form:
   
   **Basic Information:**
   - Job Title: `Junior Java Developer` (or any title)
   - Department: Select from dropdown (e.g., "Engineering")
   - Location: `Accra, Ghana`
   - ☑️ Remote Position (optional)
   - Employment Type: `Full-Time`
   - Experience Level: `Entry Level`

   **Job Description:**
   - Description: Write a detailed job description (at least 50 words)
   - Requirements: List requirements (at least 30 words)
   - Responsibilities: List responsibilities (optional)
   - Benefits: List benefits (optional)

   **Additional Details:**
   - Salary Range: Min: `500`, Max: `1000`
   - Application Deadline: Select a future date (e.g., `2025-11-15`)
   - Required Skills: `Python, JavaScript, Java, C#` (comma-separated)
   - Hiring Manager Name: `John Doe`
   - Hiring Manager Email: `thompmpson19@gmail.com`

4. Click **"Create Job"** button
5. ✅ **Expected Result**: 
   - You should be redirected to the job detail page
   - Job status should be **"DRAFT"**
   - A success notification should appear

#### Step 3: Publish the Job
1. On the job detail page, scroll down to **"Job Status"** section
2. Click **"Publish Job"** button
3. Confirm the action in the dialog
4. ✅ **Expected Result**:
   - Job status changes to **"ACTIVE"**
   - **Public Application Link** section appears with a shareable URL
   - Success notification: "Job published successfully!"
   - The public slug is automatically generated (e.g., `/apply/junior-java-developer-abc123`)

#### Step 4: Copy Public Link
1. In the **"Public Application Link"** section:
2. Click **"Copy Link"** button
3. ✅ **Expected Result**:
   - Success notification: "Public application link copied to clipboard!"
   - Link format: `http://localhost:4200/apply/{slug}`

#### Step 5: View Job Statistics
1. Note the statistics on the job detail page:
   - Applications: 0
   - Views: (should increment when page is loaded)
2. Refresh the page - Views count should increase

---

### **Journey 2: Public Job Application (Candidate - No Login)**

#### Step 6: Access Public Job Page
1. Open a new **incognito/private browser window**
2. Paste the public link you copied (e.g., `http://localhost:4200/apply/junior-java-developer-abc123`)
3. ✅ **Expected Result**:
   - Job details are displayed
   - Application form is visible
   - No login required

#### Step 7: Fill Application Form
Fill in the application form with test data:

**Personal Information:**
- First Name: `Jane`
- Last Name: `Applicant`
- Email: `jane.applicant@example.com`
- Phone: `+233501234567`

**Current Position:**
- Current Job Title: `Junior Developer`
- Current Company: `TechCorp`
- Years of Experience: `2`

**Location:**
- City: `Accra`
- State/Region: `Greater Accra`
- Country: `Ghana`

**Skills:**
- Skills: `JavaScript, Python, React` (comma-separated)

**Education & Experience:**
- Education Summary: `BSc Computer Science from University of Ghana`
- Experience Summary: `2 years as Junior Developer at TechCorp working on web applications`

**Social Profiles (Optional):**
- LinkedIn: `https://linkedin.com/in/jane-applicant`
- GitHub: `https://github.com/jane-applicant`

**Resume Upload:**
1. Click **"Choose File"** or drag & drop
2. Upload a PDF, DOC, or DOCX file (max 5MB)
3. ✅ File name should appear after selection

**Cover Letter:**
- Write a brief cover letter (at least 50 words)

**Consent:**
- ☑️ Check: "I consent to the processing of my personal data"

#### Step 8: Submit Application
1. Click **"Submit Application"** button
2. ✅ **Expected Result**:
   - Loading spinner appears
   - Redirected to success page: `/apply/success`
   - Success message displayed
   - Option to register/track application shown

---

### **Journey 3: Candidate Registration & Portal**

#### Step 9: Register from Application
1. On the success page, click **"Create Account to Track Application"**
2. Fill registration form:
   - Email: `jane.applicant@example.com` (should be pre-filled)
   - Password: `Password123!` (meet requirements)
   - Confirm Password: `Password123!`
   - ☑️ Accept terms and conditions
3. Click **"Register"** button
4. ✅ **Expected Result**:
   - Success message: "Registration successful!"
   - Redirected to login page

#### Step 10: Login as Candidate
1. Login with:
   - Email: `jane.applicant@example.com`
   - Password: `Password123!`
2. ✅ **Expected Result**:
   - Redirected to Candidate Dashboard
   - Your application(s) listed with status and stage

#### Step 11: View Application Details
1. Click **"View Details"** on your application
2. ✅ **Expected Result**:
   - Full application information displayed
   - Job details visible
   - Current status and stage shown
   - Timeline (if available)

---

### **Journey 4: Admin Application Management**

#### Step 12: View Applications (Admin)
1. Switch back to admin account (or login as admin in another window)
2. Click **"Applications"** in sidebar
3. ✅ **Expected Result**:
   - List of all applications (including Jane's)
   - Filter and search options available

#### Step 13: View Application from Job Page
1. Navigate to **Jobs** → Click the job you created
2. Click **"View Applications"** button
3. ✅ **Expected Result**:
   - Applications filtered by this job
   - Jane's application should appear with status "New"

---

### **Journey 5: Job Status Management**

#### Step 14: Close a Job
1. Navigate to an Active job
2. Click **"Close Job"** in the **"Job Status"** section
3. Confirm the action
4. ✅ **Expected Result**:
   - Status changes to **"CLOSED"**
   - Public link remains but applications may be disabled
   - Success notification appears

#### Step 15: Try Creating Another Job
1. Repeat Steps 2-3 to create another job
2. This time, leave it in **DRAFT** status
3. Create a third job and publish it (will be **ACTIVE**)

---

## 📊 Testing Different Job Statuses

Now you should have jobs in different states:
- **DRAFT** - Not publicly visible, no public link
- **ACTIVE** - Publicly visible, has public link, accepts applications
- **CLOSED** - No longer accepts applications

**Note:** There is NO "Paused" status in the system. Only Draft (0), Active (1), and Closed (2).

---

## 🧪 Additional Test Scenarios

### Test: Form Validation
1. Try creating a job with missing required fields
2. ✅ **Expected**: Red error messages appear below fields
3. ✅ **Expected**: "Create Job" button is clickable but form won't submit if invalid

### Test: Duplicate Application
1. Try applying to the same job twice with the same email
2. ✅ **Expected**: Error message: "You have already applied to this job"

### Test: Invalid Resume Upload
1. Try uploading a file > 5MB
2. ✅ **Expected**: Error: "Resume file size must not exceed 5MB"
3. Try uploading wrong file type (e.g., .exe)
4. ✅ **Expected**: Only PDF, DOC, DOCX accepted

### Test: Expired Job Application
1. Create a job with a closing date in the past
2. Try applying to it via public link
3. ✅ **Expected**: Error message about deadline passed

### Test: Dashboard Statistics
1. Navigate to Dashboard
2. ✅ **Expected**:
   - Open Positions count (Active jobs)
   - Total Applications count
   - Statistics cards with numbers
   - Recent applications table

### Test: Candidate Portal Permissions
1. As a candidate, try accessing `/jobs/new`
2. ✅ **Expected**: Access denied or redirected
3. Candidates should only access:
   - `/candidate/dashboard`
   - `/candidate/applications/:id`
   - Public job pages

---

## 🔍 What to Check in Each Test

### Visual Checks:
- ✅ Forms are properly aligned and responsive
- ✅ Buttons have proper states (disabled when loading)
- ✅ Validation messages appear in red below fields
- ✅ Loading spinners appear during API calls
- ✅ Toast notifications appear for success/error
- ✅ Status badges show correct colors (Draft=gray, Active=green, Closed=orange)

### Functional Checks:
- ✅ All API requests succeed (check Network tab)
- ✅ Data is saved to database
- ✅ Navigation works correctly
- ✅ Forms reset after successful submission
- ✅ Filtering and searching work
- ✅ Pagination works (if applicable)

### Console Checks:
- ✅ No JavaScript errors in browser console
- ✅ No 404 errors in Network tab
- ✅ API responses are proper JSON
- ✅ No authentication errors

---

## 🛠️ Troubleshooting Common Issues

### Issue 1: "Create Job" Button Not Working
**Symptoms:** Button doesn't make API request

**Solutions:**
1. Open Browser Console (F12) → Check for JavaScript errors
2. Check Network tab → See if request is made
3. Verify all required fields are filled:
   - Title, Department, Location, Employment Type, Experience Level, Description, Requirements
4. Check if departments loaded (dropdown has options)
5. Hard refresh the page (Ctrl+Shift+R)

### Issue 2: No Public Link Visible
**Cause:** Job is in DRAFT status

**Solution:**
1. Publish the job using "Publish Job" button
2. Public link only appears for ACTIVE jobs

### Issue 3: Application Form Won't Submit
**Possible Causes:**
1. Missing required fields (name, email, consent checkbox)
2. Invalid email format
3. Resume file too large (>5MB)
4. Job is closed or inactive

**Solution:**
- Check browser console for error messages
- Verify all required fields are filled
- Check file size

### Issue 4: Candidate Can't See Applications
**Possible Causes:**
1. Not logged in
2. Wrong email used for registration
3. Application not yet submitted

**Solution:**
- Ensure you're logged in with the same email used for application
- Check if application was successfully submitted

### Issue 5: Departments Dropdown is Empty
**Cause:** No departments in database

**Solution:**
1. Run `ATS.DbMigrator` to seed initial data
2. Or manually create departments via API/Admin panel

---

## 🎯 Success Criteria

After completing all test journeys, you should have:
- ✅ At least 3 jobs: 1 Draft, 1 Active, 1 Closed
- ✅ At least 1 public application submitted
- ✅ 1 candidate registered and able to view their application
- ✅ Public links working for active jobs
- ✅ Job statistics updating correctly
- ✅ All forms validating properly
- ✅ No console errors

---

## 📝 Testing Checklist

Use this checklist to track your testing progress:

### Core Features:
- [ ] Create job posting (all required fields)
- [ ] Edit job posting
- [ ] Publish job (Draft → Active)
- [ ] Close job (Active → Closed)
- [ ] Copy public link
- [ ] Submit public application (without login)
- [ ] Upload resume during application
- [ ] Register candidate account from application
- [ ] Login as candidate
- [ ] View applications in candidate portal
- [ ] View application details

### Admin Features:
- [ ] View all jobs list
- [ ] Search/filter jobs
- [ ] View job details
- [ ] View job statistics (applications, views)
- [ ] View applications list
- [ ] Filter applications by job

### Validation & Security:
- [ ] Form validation works on all forms
- [ ] Can't create job without required fields
- [ ] Can't submit application without consent
- [ ] Resume file size validation (5MB max)
- [ ] Duplicate application prevention
- [ ] Candidate can only see their own applications
- [ ] Public links only work for active jobs

### UI/UX:
- [ ] Responsive design on mobile/tablet
- [ ] Loading states during API calls
- [ ] Success/error notifications
- [ ] Proper navigation/breadcrumbs
- [ ] Status badges with correct colors
- [ ] Icons display correctly

---

## 🔐 Test Accounts

### Admin Account:
- Email: `admin@abp.io` (or your configured admin)
- Password: Your admin password
- Access: Full system access

### Test Candidate Account:
- Email: `jane.applicant@example.com`
- Password: `Password123!`
- Access: Candidate portal only

---

## 📊 Expected Database State After Testing

After completing all tests:

**Jobs Table:**
- 3-5 job records with varying statuses
- At least 1 with public slug

**Candidates Table:**
- At least 1 candidate record (Jane Applicant)
- Resume URL stored (blob reference)

**Applications Table:**
- At least 1 application record
- Status: New
- Stage: Applied
- Linked to job and candidate

**Identity Users:**
- At least 1 user with "Candidate" role

---

## 🚨 Known Limitations (Not Bugs)

1. **No AI Analysis** - AI features are not implemented yet
2. **No Email Notifications** - Confirmations are not sent
3. **No Drag-Drop Pipeline** - Pipeline UI is placeholder
4. **No Interview Scheduling** - Feature not implemented
5. **Reports Page Empty** - Analytics not implemented
6. **Resume stored in DB** - Production should use blob storage

---

## 📞 Getting Help

If you encounter issues not covered here:
1. Check browser console (F12) for errors
2. Check Network tab for failed API requests
3. Check backend logs in Visual Studio
4. Verify database has seeded data
5. Restart both backend and frontend

---

**Happy Testing! 🎉**

Last Updated: October 28, 2025
Version: 1.0

