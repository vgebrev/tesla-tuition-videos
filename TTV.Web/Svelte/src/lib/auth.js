import { writable } from 'svelte/store';
import { UserManager } from 'oidc-client';
import { config } from '$lib/config';

/** OIDC user manager
 * @type {UserManager} */
export const userManager = new UserManager(config.oidc);

/** Auth store state
 * @type {Writable<import('$lib/types').AuthStoreState>} */
export const authStore = writable({
  user: null,
  isAuthenticated: false,
  origin: null
});

/** Auth store actions */
export const authActions = {
  updateStoreWithCurrentUser,
  silentSignin
};

userManager.events.addUserLoaded((user) => {
  authStore.set({ user, isAuthenticated: true, origin: 'user-loaded-event' });
});

userManager.events.addUserUnloaded(() => {
  authStore.set({ user: null, isAuthenticated: false, origin: 'user-unloaded-event' });
});

userManager.events.addAccessTokenExpiring(async () => {
  await silentSignin();
});

userManager.events.addAccessTokenExpired(async () => {
  await silentSignin();
});

async function updateStoreWithCurrentUser(origin) {
  const user = await userManager.getUser();
  authStore.set({ user, isAuthenticated: user && !user.expired, origin });
}

async function silentSignin() {
  try {
    await userManager.signinSilent();
  } catch {
    // We'll leave it to the user to explicitly sign in
  }
}
