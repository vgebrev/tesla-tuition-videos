<script>
  import { onMount } from 'svelte';

  /** @type {string} */
  export let title;
  /** @type {boolean} */
  export let isOpen = false;

  let _id = crypto.randomUUID().replace(/-/g, '');
  let _isOpen = isOpen;
  let elem;

  onMount(() => {
    elem.addEventListener('hidden.bs.modal', () => {
      _isOpen = false;
      isOpen = false;
    });
  });

  export function open() {
    if (_isOpen) return;
    bootstrap.Modal.getOrCreateInstance(elem).show();
    _isOpen = true;
  }

  export function close(element) {
    if (!_isOpen) return;
    bootstrap.Modal.getOrCreateInstance(element).hide();
    _isOpen = false;
  }

  $: if (isOpen) {
    open();
  } else {
    close();
  }
</script>

<div
  class="modal fade"
  bind:this={elem}
  id={_id}
  data-bs-keyboard="true"
  tabindex="-1"
  aria-labelledby="{_id}modalLabel"
  aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content text-bg-dark">
      <div class="modal-header">
        {#if title}
          <h1 class="modal-title fs-5" id="{_id}modalLabel">{title}</h1>
        {/if}
        <button
          type="button"
          class="btn-close btn-close-white"
          data-bs-dismiss="modal"
          aria-label="Close"></button>
      </div>
      <div class="modal-body"><slot /></div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
