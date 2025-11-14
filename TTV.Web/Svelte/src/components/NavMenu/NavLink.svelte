<script>
  import { goto } from '$app/navigation';
  import { resolve } from '$app/paths';

  export let href = '/';
  export let cssClass = 'nav-link';
  export let toggle = 'collapse';
  export let target = '.navbar-collapse.show';

  function navigate() {
    goto(resolve(href));
  }
</script>

<a
  class={cssClass}
  href={resolve(href)}
  data-bs-toggle={toggle}
  data-bs-target={target}
  {...$$restProps}
  on:click|preventDefault={navigate}><slot /></a>
