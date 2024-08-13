import { PUBLIC_AUTHORITY_URL, PUBLIC_API_BASE_URL } from '$env/static/public';

export const config = {
  api: {
    baseUrl: PUBLIC_API_BASE_URL
  },
  oidc: {
    authority: PUBLIC_AUTHORITY_URL,
    client_id: 'ttv_web_blazor',
    redirect_uri: `${window.location.origin}/authentication/login-callback`,
    response_type: 'code',
    scope: 'openid profile email ttv_web_api',
    post_logout_redirect_uri: `${window.location.origin}/authentication/logout-callback`
  },
  contactInfo: {
    phoneNumber: '+27 66 444 5850',
    supportEmail: 'support@teslatuitionvideos.co.za',
    address: '158 Milner Avenue, Franklin Roosevelt Park, Johannesburg 2194, South Africa'
  },
  announcement: {
    id: 'unique-announcement-id',
    title: 'Welcome to Tesla Tuition Videos',
    message:
      'We are excited to have you on board. Please feel free to reach out to us if you have any questions or need assistance.',
    endDate: '2022-12-31T23:59:59.999Z'
  },
  isTestEnvironment: true
};
