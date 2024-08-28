<script>
  import { authActions } from '$lib/auth.js';

  async function login() {
    await authActions.fullSignin();
  }
</script>

<a
  href="/"
  {...$$restProps}
  on:click|preventDefault={login}><slot>Log in</slot></a>
