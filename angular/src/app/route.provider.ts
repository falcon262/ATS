import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
      {
        path: '/dashboard',
        name: 'Dashboard',
        iconClass: 'fas fa-chart-line',
        order: 1,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Dashboard',
      },
      {
        path: '/jobs',
        name: 'Jobs',
        iconClass: 'fas fa-briefcase',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Jobs',
      },
      {
        path: '/candidates',
        name: 'Candidates',
        iconClass: 'fas fa-users',
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Candidates',
      },
      {
        path: '/departments',
        name: 'Departments',
        iconClass: 'fas fa-sitemap',
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Departments',
      },
      {
        path: '/applications',
        name: 'Applications',
        iconClass: 'fas fa-file-alt',
        order: 5,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Applications',
      },
      {
        path: '/pipeline',
        name: 'Pipeline',
        iconClass: 'fas fa-tasks',
        order: 6,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.Pipeline',
      },
      {
        path: '/candidate/dashboard',
        name: 'My Applications',
        iconClass: 'fas fa-user-circle',
        order: 7,
        layout: eLayoutType.application,
        requiredPolicy: 'ATS.CandidatePortal.ViewApplications',
      },
      {
        path: '/home',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 100,
        layout: eLayoutType.application,
      },
  ]);
}
