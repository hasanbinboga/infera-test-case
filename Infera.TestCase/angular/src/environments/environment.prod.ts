import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'TestCase',
    logoUrl: 'https://www.ccnholding.com/medya/2023/08/ccn-holding-logo.png',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44372/',
    redirectUri: baseUrl,
    clientId: 'TestCase_App',
    responseType: 'code',
    scope: 'offline_access TestCase',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44399',
      rootNamespace: 'Infera.TestCase',
    },
  },
} as Environment;
