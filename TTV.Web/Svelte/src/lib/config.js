import {
  PUBLIC_OIDC_AUTHORITY,
  PUBLIC_API_BASEURL,
  PUBLIC_ANNOUNCEMENT_ACTIVE,
  PUBLIC_ANNOUNCEMENT_ID,
  PUBLIC_ANNOUNCEMENT_TITLE,
  PUBLIC_ANNOUNCEMENT_MESSAGE,
  PUBLIC_ANNOUNCEMENT_ENDDATE,
  PUBLIC_ISTESTENVIRONMENT,
  PUBLIC_BANKACCOUNT_BANKNAME,
  PUBLIC_BANKACCOUNT_ACCOUNTNUMBER,
  PUBLIC_BANKACCOUNT_BRANCHCODE
} from '$env/static/public';
import { WebStorageStateStore } from 'oidc-client';

export const config = {
  api: {
    baseUrl: PUBLIC_API_BASEURL
  },
  oidc: {
    authority: PUBLIC_OIDC_AUTHORITY,
    client_id: 'ttv_web_blazor',
    redirect_uri: `${window.location.origin}/authentication/login-callback`,
    response_type: 'code',
    scope: 'openid profile email ttv_web_api',
    post_logout_redirect_uri: `${window.location.origin}/authentication/logout-callback`,
    automaticSilentRenew: true,
    silent_redirect_uri: `${window.location.origin}/authentication/silent-renew.html`,
    userStore: new WebStorageStateStore({ store: window.localStorage })
  },
  contactInfo: {
    phoneNumber: '+27 66 444 5850',
    supportEmail: 'support@teslatuitionvideos.co.za',
    address: '158 Milner Avenue, Franklin Roosevelt Park, Johannesburg 2194, South Africa'
  },
  bankAccount: {
    bankName: PUBLIC_BANKACCOUNT_BANKNAME,
    accountNumber: PUBLIC_BANKACCOUNT_ACCOUNTNUMBER,
    branchCode: PUBLIC_BANKACCOUNT_BRANCHCODE
  },
  announcement: {
    active: PUBLIC_ANNOUNCEMENT_ACTIVE === 'true',
    id: PUBLIC_ANNOUNCEMENT_ID,
    title: PUBLIC_ANNOUNCEMENT_TITLE,
    message: PUBLIC_ANNOUNCEMENT_MESSAGE,
    endDate: PUBLIC_ANNOUNCEMENT_ENDDATE
  },
  isTestEnvironment: PUBLIC_ISTESTENVIRONMENT === 'true'
};

export const defaultErrorMessage = 'Something went wrong. Please refresh the page or try again.';
