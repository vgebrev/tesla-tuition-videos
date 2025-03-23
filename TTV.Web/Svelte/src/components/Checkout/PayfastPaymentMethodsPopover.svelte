<script>
  import Popover from '$components/common/Popover.svelte';

  /** @type {HTMLElement} */
  let popoverElem;
</script>

<button
  aria-label="Payfast Payment Methods Info"
  bind:this={popoverElem}
  type="button"
  class="btn btn-link p-0 ms-1">
  <i class="bi-info-circle"></i>
</button>
<Popover
  title="Payfast Payment Methods"
  triggerElem={popoverElem}>
  <ul class="list-group">
    <li class="list-group-item">
      <a
        href="https://payfast.io/features/payment-methods/credit-cheque-cards/"
        target="_blank"
        class="link-light">Credit/Cheque Card</a>
    </li>
    <li class="list-group-item">
      <a
        href="https://payfast.io/features/payment-methods/debit-cards/"
        target="_blank"
        class="link-light">Debit Card</a>
    </li>
    <li class="list-group-item">
      <a
        href="https://payfast.io/features/payment-methods/zapper/"
        target="_blank"
        class="link-light">Zapper</a>
    </li>
  </ul>
</Popover>
