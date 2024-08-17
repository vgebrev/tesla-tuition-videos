<script>
  import { userManager } from '$lib/auth.js';

  async function login() {
    sessionStorage.setItem('redirect', window.location.pathname);
    try {
      await userManager.signinSilent();
    } catch {
      await userManager.signinRedirect();
    }
  }
</script>

<a href="/" {...$$restProps} on:click|preventDefault={login}><slot>Log in</slot></a>
