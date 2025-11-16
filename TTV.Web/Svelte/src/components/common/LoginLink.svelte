<script>
  import { authActions } from '$lib/auth.js';
  import { resolve } from '$app/paths';

  async function login() {
    await authActions.fullSignin();
  }
</script>

<a
  href={resolve('/')}
  {...$$restProps}
  on:click|preventDefault={login}><slot>Log in</slot></a>
