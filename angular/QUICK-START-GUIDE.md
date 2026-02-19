# TalentFlow ATS - Quick Start Guide

## 🚀 Getting Started in 5 Minutes

### Prerequisites
- ✅ Node.js 18+ installed
- ✅ Angular CLI installed (`npm install -g @angular/cli`)
- ✅ Backend API running on `https://localhost:44328`

---

## 📦 Step 1: Install Dependencies

```bash
cd angular
npm install --legacy-peer-deps
```

> **Note:** We use `--legacy-peer-deps` due to minor version conflicts with karma and Angular build tools.

---

## 🏃 Step 2: Run Development Server

```bash
npm start
```

The application will open at `http://localhost:4200`

---

## 🔑 Step 3: Login

1. Navigate to `http://localhost:4200`
2. You'll be redirected to the ABP login page
3. Use your backend credentials to login
4. Upon successful login, you'll be redirected to the Dashboard

---

## 🎯 Step 4: Explore Features

### Dashboard (`/dashboard`)
- View key statistics (Open Positions, Applications, Interviews, Offers)
- See recent applications
- Quick actions to post jobs or browse candidates

### Jobs Management (`/jobs`)
- **View Jobs:** Browse all job postings with search
- **Create Job:** Click "Post New Job" button
  - Fill in job details, requirements, responsibilities
  - Set salary range and closing date
  - Add required skills (comma-separated)
- **Edit Job:** Click "..." menu on any job card → Edit
- **Publish Job:** Draft jobs can be published from the card menu
- **View Details:** Click "View Details" to see full job information

### Candidates (`/candidates`)
- **Browse Candidates:** Search and filter candidates
- **View Details:** Click "View" to see candidate profile
- **AI Scores:** View AI matching scores for each candidate

### Applications (`/applications`)
- Placeholder for viewing all applications
- Will show filtered applications by job

### Pipeline (`/pipeline`)
- Placeholder for Kanban board
- Will enable drag-drop management of application stages

### Public Job Applications

#### For Administrators
1. **Create and Publish a Job** (`/jobs/new`)
   - Fill in job details
   - Publish the job (Draft → Active)
   - A unique public slug is automatically generated

2. **Get Shareable Link** (`/jobs/:id`)
   - Open any published job detail page
   - Find the "Public Application Link" section
   - Copy the link using the "Copy Link" button
   - Share the link with candidates via email, social media, or job boards

3. **Manage Public Applications**
   - Applications from public links appear in the same Applications list
   - No difference between internal and public applications in the pipeline

#### For Candidates
1. **Apply via Public Link** (`/apply/:slug`)
   - Navigate to the public job link (e.g., `/apply/senior-software-engineer-123`)
   - View full job details without login
   - Fill out the application form:
     - Personal information (name, email, phone)
     - Current position and experience
     - Skills, education, and experience summary
     - Upload resume (PDF, DOC, DOCX up to 5MB)
     - Cover letter
   - Check the data processing consent checkbox
   - Submit application

2. **Create Candidate Account** (`/register`)
   - After successful application, click "Create Account to Track Application"
   - Enter email (pre-filled from application) and password
   - Accept terms and conditions
   - Submit registration
   - Login with new credentials

3. **Track Applications** (`/candidate/dashboard`)
   - View all your submitted applications
   - See current status and stage for each application
   - View AI match scores (if available)
   - Click to see detailed application information

4. **View Application Details** (`/candidate/applications/:id`)
   - View full application timeline
   - See interview schedules (if scheduled)
   - View job offers (if made)
   - Read your submitted cover letter

---

## 🛠️ Development Commands

### Start Development Server
```bash
npm start
```

### Build for Production
```bash
npm run build:prod
```

### Run Tests
```bash
npm test
```

### Lint Code
```bash
npm run lint
```

### Analyze Bundle Size
```bash
npm run build:stats
npm run analyze
```

---

## 📂 Key Files to Know

### Configuration
- `src/environments/environment.ts` - Development environment config
- `src/environments/environment.prod.ts` - Production environment config
- `tsconfig.json` - TypeScript configuration with proxy paths
- `angular.json` - Angular project configuration

### Routing
- `src/app/app.routes.ts` - Application routes (standalone components)
- `src/app/route.provider.ts` - Navigation menu configuration

### Styling
- `src/styles.scss` - Global styles and variables
- `src/app/shared/styles/_responsive.scss` - Responsive utilities

---

## 🔧 Customization

### Change Backend URL
Edit `src/environments/environment.ts`:
```typescript
apis: {
  default: {
    url: 'https://your-backend-url',
    rootNamespace: 'ATS',
  }
}
```

### Add Navigation Menu Item
Edit `src/app/route.provider.ts`:
```typescript
{
  path: '/your-feature',
  name: 'Your Feature',
  iconClass: 'fas fa-icon',
  order: 6,
  layout: eLayoutType.application,
}
```

### Add Custom Styles
Edit `src/styles.scss` or create new SCSS files in `src/app/shared/styles/`

---

## 🐛 Troubleshooting

### Build Errors

**Issue: "Cannot find module '@proxy/...'"**
```bash
# Solution: Ensure tsconfig paths are configured
# Already configured in tsconfig.json
```

**Issue: "Karma version conflict"**
```bash
# Solution: Use legacy peer deps
npm install --legacy-peer-deps
```

### Runtime Errors

**Issue: "401 Unauthorized"**
- Check if backend is running
- Verify OAuth configuration in environment files
- Clear browser cache and cookies

**Issue: "CORS Error"**
- Check backend CORS configuration
- Ensure frontend URL is whitelisted in backend

**Issue: "Route not found"**
- Verify route is configured in `app.routes.ts`
- Check if auth guard is properly configured

---

## 📱 Mobile Testing

### Test Responsive Design
1. Open browser DevTools (F12)
2. Toggle device toolbar (Ctrl+Shift+M)
3. Select device (iPhone, iPad, etc.)
4. Test navigation and features

### Breakpoints
- Mobile: < 768px
- Tablet: 768px - 991px
- Desktop: ≥ 992px

---

## 🔐 Environment Setup for Production

### 1. Update Production URLs
Edit `src/environments/environment.prod.ts`:
```typescript
const baseUrl = 'https://talentflow.yourdomain.com';

const oAuthConfig = {
  issuer: 'https://api.talentflow.yourdomain.com/',
  redirectUri: baseUrl,
  clientId: 'ATS_App',
  ...
};
```

### 2. Build for Production
```bash
npm run build:prod
```

### 3. Deploy
Deploy contents of `dist/ATS` folder to your web server

---

## 🧪 Testing the Build

### Test Production Build Locally
```bash
# Build
npm run build:prod

# Serve using a simple HTTP server
npx http-server dist/ATS -p 8080 -o
```

---

## 📊 Feature Checklist

### ✅ Implemented & Working
- [x] Dashboard with statistics
- [x] Jobs list, detail, create, edit
- [x] Job publishing and closing
- [x] Candidates list and detail
- [x] Search and filtering
- [x] Responsive design
- [x] Authentication & routing
- [x] ABP.io integration
- [x] **Public job application links**
- [x] **Public application form with resume upload**
- [x] **Candidate registration and portal**
- [x] **Candidate application tracking**

### 🚧 Placeholder (Future Implementation)
- [ ] Full applications management (admin view)
- [ ] Pipeline drag-drop board
- [ ] AI analysis dashboard
- [ ] Reports and analytics
- [ ] Settings and configuration
- [ ] Email notifications
- [ ] CAPTCHA for public forms
- [ ] Rate limiting for public endpoints

---

## 💡 Pro Tips

### 1. Use Browser DevTools
- Network tab to debug API calls
- Console for errors and logs
- Elements tab for styling issues

### 2. Check Backend Logs
- Always check backend logs for API errors
- Verify data is being saved correctly

### 3. Use ABP Suite (if available)
- Generate additional CRUD pages
- Customize entity management

### 4. Hot Module Replacement
- Changes auto-reload during development
- No need to restart server for most changes

---

## 🎓 Learning Resources

### Angular
- [Official Angular Docs](https://angular.io/docs)
- [Standalone Components Guide](https://angular.io/guide/standalone-components)
- [Reactive Forms](https://angular.io/guide/reactive-forms)

### ABP.io
- [ABP Angular Tutorial](https://docs.abp.io/en/abp/latest/Tutorials/Angular)
- [ABP Angular UI](https://docs.abp.io/en/abp/latest/UI/Angular/Quick-Start)

### RxJS
- [RxJS Documentation](https://rxjs.dev/)
- [Learn RxJS](https://www.learnrxjs.io/)

---

## ❓ Common Questions

**Q: How do I add a new feature module?**
```bash
ng generate component features/my-feature/my-component --standalone
```

**Q: How do I update proxy services?**
```bash
# In the root of your solution
abp generate-proxy -t ng
```

**Q: How do I add a new dependency?**
```bash
npm install package-name --legacy-peer-deps
```

**Q: Where are the API calls made?**
- All API calls use ABP proxy services in `src/app/proxy/`
- Services are auto-generated from backend
- Services use RestService from @abp/ng.core

---

## 🆘 Getting Help

1. **Check Documentation**
   - `IMPLEMENTATION-SUMMARY.md` - Detailed implementation notes
   - `src/assets/angular-implementation-roadmap.md` - Full roadmap

2. **ABP.io Support**
   - [Community Forums](https://community.abp.io/)
   - [Documentation](https://docs.abp.io/)
   - [GitHub Issues](https://github.com/abpframework/abp)

3. **Angular Support**
   - [Stack Overflow](https://stackoverflow.com/questions/tagged/angular)
   - [Angular Discord](https://discord.gg/angular)

---

## 🎉 You're Ready!

Your TalentFlow ATS Angular frontend is now set up and ready for development. Start exploring the features and build amazing recruitment experiences!

**Happy Coding! 🚀**

