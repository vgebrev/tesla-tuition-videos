import { writable } from 'svelte/store';
import { UserManager } from 'oidc-client';
import { config } from '$lib/config';
import { goto } from '$app/navigation';

/** OIDC user manager
 * @type {UserManager} */
export const userManager = new UserManager(config.oidc);

/** Auth store state
 * @type {import('svelte/store').Writable<import('$lib/types').AuthStoreState>} */
export const authStore = writable({
  user: null,
  isAuthenticated: false,
  origin: null,
  isLoading: false
});

/** Auth store actions */
export const authActions = {
  updateStoreWithCurrentUser,
  silentSignin,
  fullSignin
};

userManager.events.addUserLoaded((user) => {
  authStore.set({ user, isAuthenticated: true, origin: 'user-loaded-event', isLoading: false });
});

userManager.events.addUserUnloaded(() => {
  authStore.update((state) => {
    state.user = null;
    state.isAuthenticated = false;
    state.origin = 'user-unloaded-event';
    return state;
  });
});

userManager.events.addSilentRenewError(() => {
  userManager.clearStaleState().finally(() => {
    authStore.update((state) => {
      state.isLoading = false;
      return state;
    });
  });
});
userManager.events.addAccessTokenExpiring(async () => {
  await silentSignin();
});

userManager.events.addAccessTokenExpired(async () => {
  await silentSignin();
});

/**
 * Update the auth store with the current user
 * @param {'login-callback' | 'user-loaded-event' | 'user-unloaded-event' | null} origin
 * @returns {Promise<void>}
 */
async function updateStoreWithCurrentUser(origin) {
  const user = await userManager.getUser();
  authStore.set({ user, isAuthenticated: (user && !user.expired) || false, origin, isLoading: false });
}

async function silentSignin(isLoading = false) {
  try {
    authStore.update((state) => {
      state.isLoading = isLoading;
      return state;
    });
    await userManager.signinSilent();
  } catch {
    // We'll leave it to the user to explicitly sign in
    await userManager.clearStaleState();
    authStore.update((state) => {
      state.isLoading = false;
      return state;
    });
  }
}

async function fullSignin() {
  authStore.update((state) => {
    state.isLoading = true;
    return state;
  });
  try {
    await userManager.signinSilent();
  } catch {
    sessionStorage.setItem('redirect', window.location.pathname);
    try {
      await userManager.signinRedirect();
    } catch {
      authStore.update((state) => {
        state.isLoading = false;
        return state;
      });
      await goto('/authentication/error', { replaceState: true });
    }
  }
}

/** Authorization policies */
export const authorizationPolicies = {
  /**
   * Check if the user has the 'admin' role
   * @param {import('oidc-client').User | null} user
   * @returns {boolean}
   */
  admin: (user) => user?.profile?.role === 'admin',

  /**
   * Check if the user has the 'vouchers.issue' permission
   * @param {import('oidc-client').User | null} user
   * @returns {boolean}
   */
  canIssueVouchers: (user) => user?.profile?.permission.indexOf('vouchers.issue') > -1
};
