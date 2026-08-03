import { env } from '$env/dynamic/public';
import { WebStorageStateStore } from 'oidc-client';

export const config = {
  api: {
    baseUrl: env.PUBLIC_API_BASEURL
  },
  oidc: {
    authority: env.PUBLIC_OIDC_AUTHORITY,
    client_id: 'ttv_web_frontend',
    redirect_uri: `${window.location.origin}/authentication/login-callback`,
    response_type: 'code',
    scope: 'openid profile email role ttv_web_api',
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
    bankName: env.PUBLIC_BANKACCOUNT_BANKNAME,
    accountNumber: env.PUBLIC_BANKACCOUNT_ACCOUNTNUMBER,
    branchCode: env.PUBLIC_BANKACCOUNT_BRANCHCODE
  },
  announcement: {
    active: env.PUBLIC_ANNOUNCEMENT_ACTIVE === 'true',
    id: env.PUBLIC_ANNOUNCEMENT_ID,
    title: env.PUBLIC_ANNOUNCEMENT_TITLE,
    message: env.PUBLIC_ANNOUNCEMENT_MESSAGE,
    endDate: env.PUBLIC_ANNOUNCEMENT_ENDDATE
  },
  isTestEnvironment: env.PUBLIC_ISTESTENVIRONMENT === 'true'
};

export const defaultErrorMessage = 'Something went wrong. Please refresh the page or try again.';
export const defaultPageSize = 12;
