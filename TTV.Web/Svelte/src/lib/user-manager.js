import { writable } from 'svelte/store';
import { UserManager } from 'oidc-client';
import { config } from '$lib/config';

export const userManager = new UserManager(config.oidc);

export const authStore = writable({
  user: null,
  isAuthenticated: false
});

userManager.events.addUserLoaded((user) => {
  authStore.set({ user, isAuthenticated: true });
});

userManager.events.addUserUnloaded(() => {
  authStore.set({ user: null, isAuthenticated: false });
});
