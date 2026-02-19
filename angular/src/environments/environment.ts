import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44328/',
  redirectUri: baseUrl,
  clientId: 'ATS_App',
  responseType: 'code',
  scope: 'offline_access ATS',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TalentFlow ATS',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44328',
      rootNamespace: 'ATS',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
