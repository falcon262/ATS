import { Environment } from '@abp/ng.core';

// Production Environment Configuration
// Update these URLs with your actual production domain before deployment
const baseUrl = 'https://talentflow.yourdomain.com';

const oAuthConfig = {
  issuer: 'https://api.talentflow.yourdomain.com/',
  redirectUri: baseUrl,
  clientId: 'ATS_App',
  responseType: 'code',
  scope: 'offline_access ATS',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TalentFlow ATS',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://api.talentflow.yourdomain.com',
      rootNamespace: 'ATS',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
