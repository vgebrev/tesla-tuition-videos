<script>
  import { goto } from '$app/navigation';

  export let href = '/';
  export let cssClass = 'nav-link';
  export let toggle = 'collapse';
  export let target = '#navbar';

  let elem;

  function navigate() {
    goto(href);
  }
</script>

<a
  bind:this={elem}
  class={cssClass}
  {href}
  data-bs-toggle={toggle}
  data-bs-target={target}
  {...$$restProps}
  on:click|preventDefault={navigate}><slot /></a>
