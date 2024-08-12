import { UserManager } from 'oidc-client';
import { config } from '$lib/config';

export const userManager = new UserManager(config.oidc);
