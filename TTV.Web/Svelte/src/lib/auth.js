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
  isAuthenticated: false
});

/** Auth store actions */
export const authActions = {
  updateStoreWithCurrentUser
};

// NOTE: The userLoaded event fires before signinRedirectCallback in /authentication/login-callback which does a 2nd
// redirect to the original rout. To avoid interrupting authStore subscribers, we update authStore after the 2nd redirect,
// in +layout.svelte's afterNavigate.
// auth.events.addUserLoaded((user) => {
//   authStore.set({ user, isAuthenticated: true });
// });

userManager.events.addUserUnloaded(() => {
  authStore.set({ user: null, isAuthenticated: false });
});

async function updateStoreWithCurrentUser() {
  const user = await userManager.getUser();
  authStore.set({ user, isAuthenticated: user && !user.expired });
}
