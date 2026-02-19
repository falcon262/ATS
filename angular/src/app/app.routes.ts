import { authGuard, permissionGuard } from '@abp/ng.core';
import { Routes } from '@angular/router';

export const APP_ROUTES: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/home'
  },
  {
    path: 'home',
    loadComponent: () => import('./home/home.component').then(c => c.HomeComponent),
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./features/dashboard/dashboard-page/dashboard-page').then(c => c.DashboardPage),
    canActivate: [authGuard]
  },
  {
    path: 'jobs',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/jobs/job-list/job-list').then(c => c.JobList)
      },
      {
        path: 'new',
        loadComponent: () => import('./features/jobs/job-form/job-form').then(c => c.JobForm)
      },
      {
        path: ':id',
        loadComponent: () => import('./features/jobs/job-detail/job-detail').then(c => c.JobDetail)
      },
      {
        path: ':id/edit',
        loadComponent: () => import('./features/jobs/job-form/job-form').then(c => c.JobForm)
      }
    ]
  },
  {
    path: 'candidates',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/candidates/candidate-list/candidate-list').then(c => c.CandidateList)
      },
      {
        path: 'new',
        loadComponent: () => import('./features/candidates/candidate-form/candidate-form').then(c => c.CandidateForm)
      },
      {
        path: ':id',
        loadComponent: () => import('./features/candidates/candidate-detail/candidate-detail').then(c => c.CandidateDetail)
      },
      {
        path: ':id/edit',
        loadComponent: () => import('./features/candidates/candidate-form/candidate-form').then(c => c.CandidateForm)
      }
    ]
  },
  {
    path: 'departments',
    loadComponent: () => import('./features/departments/department-list/department-list.component').then(c => c.DepartmentListComponent),
    canActivate: [authGuard]
  },
  {
    path: 'applications',
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./features/applications/application-list/application-list').then(c => c.ApplicationList)
      },
      {
        path: ':id',
        loadComponent: () => import('./features/applications/application-detail/application-detail').then(c => c.ApplicationDetail)
      }
    ]
  },
  {
    path: 'pipeline',
    loadComponent: () => import('./features/pipeline/pipeline-board/pipeline-board').then(c => c.PipelineBoard),
    canActivate: [authGuard]
  },
  // Public job application routes (no auth required)
  // Note: More specific routes must come before parameterized routes
  {
    path: 'apply/success',
    loadComponent: () => import('./features/public/application-success/application-success.component').then(c => c.ApplicationSuccessComponent)
  },
  {
    path: 'apply/:slug',
    loadComponent: () => import('./features/public/public-job-detail/public-job-detail.component').then(c => c.PublicJobDetailComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./features/candidate-auth/candidate-register/candidate-register.component').then(c => c.CandidateRegisterComponent)
  },
  // Candidate portal routes (auth required)
  {
    path: 'candidate',
    canActivate: [authGuard],
    children: [
      {
        path: 'dashboard',
        loadComponent: () => import('./features/candidate-portal/candidate-dashboard/candidate-dashboard.component').then(c => c.CandidateDashboardComponent)
      },
      {
        path: 'applications/:id',
        loadComponent: () => import('./features/candidate-portal/application-detail-view/application-detail-view.component').then(c => c.ApplicationDetailViewComponent)
      }
    ]
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(c => c.createRoutes()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(c => c.createRoutes()),
  },
  {
    path: 'tenant-management',
    loadChildren: () => import('@abp/ng.tenant-management').then(c => c.createRoutes()),
  },
  {
    path: 'setting-management',
    loadChildren: () => import('@abp/ng.setting-management').then(c => c.createRoutes()),
  },
];
