<script>
  import { onMount } from 'svelte';

  /* type {string} */
  export let id = 'drawer';
  /* type {boolean} */
  export let isOpen = false;

  let _isOpen = isOpen;
  let elem;

  onMount(() => {
    elem.addEventListener('hidden.bs.offcanvas', () => {
      _isOpen = false;
      isOpen = false;
    });
  });

  function open() {
    if (_isOpen) return;
    bootstrap.Offcanvas.getOrCreateInstance(elem).show();
    _isOpen = true;
  }

  function close() {
    if (!_isOpen) return;
    bootstrap.Offcanvas.getOrCreateInstance(elem).hide();
    _isOpen = false;
  }

  $: if (isOpen) {
    open();
  } else {
    close();
  }
</script>

<div
  class="offcanvas offcanvas-end text-bg-dark"
  data-bs-scroll="true"
  tabindex="-1"
  bind:this={elem}
  {id}
  aria-labelledby="{id}Label">
  <div class="offcanvas-header">
    <h5
      class="offcanvas-title"
      id="{id}Label">
      <slot name="title" />
    </h5>
    <button
      type="button"
      class="btn-close btn-close-white"
      data-bs-dismiss="offcanvas"
      aria-label="Close"></button>
  </div>
  <div class="offcanvas-body p-0">
    <slot name="body" />
  </div>
</div>
