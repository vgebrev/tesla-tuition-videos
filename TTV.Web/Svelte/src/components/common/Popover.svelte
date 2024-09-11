<script>
  import { onDestroy } from 'svelte';
  import { createEventDispatcher } from 'svelte';

  /** @type {HTMLElement} */
  export let triggerElem;

  /** @type {bootstrap.Tooltip.PopoverPlacement} */
  export let placement = 'right';

  /** @type {string} */
  export let title = '';

  /** @type {'click' | 'hover' | 'focus' | 'manual' | 'click hover' | 'click focus' | 'hover focus' | 'click hover focus' | undefined} */
  export let trigger = 'click';

  /** @type {string} */

  export let customClass = 'popover-dark'; // lives in static/css/app.css

  /** @type {bootstrap.Popover} */
  let popoverInstance;

  /** @type HTMLDivElement */
  let content;

  let isOpen = false;

  const dispatch = createEventDispatcher();
  onDestroy(() => {
    if (popoverInstance) {
      popoverInstance.dispose();
    }
    document.removeEventListener('click', handleOutsideClick);
  });

  /** Handle outside click to close the popover
   * @param {MouseEvent} event
   * */
  function handleOutsideClick(event) {
    if (popoverInstance && isOpen && !popoverInstance.tip.contains(event.target)) {
      popoverInstance.hide();
    }
  }

  $: if (triggerElem) {
    if (popoverInstance) {
      popoverInstance.dispose();
    }
    popoverInstance = new bootstrap.Popover(triggerElem, {
      content: content.innerHTML,
      html: true,
      title,
      placement,
      trigger,
      customClass
    });

    triggerElem.addEventListener('shown.bs.popover', () => {
      dispatch('shown');
      isOpen = true;
    });

    triggerElem.addEventListener('hidden.bs.popover', () => {
      dispatch('hidden');
      isOpen = false;
    });

    document.addEventListener('click', handleOutsideClick);
  }
</script>

<div class="d-none">
  <div bind:this={content}>
    <slot></slot>
  </div>
</div>
