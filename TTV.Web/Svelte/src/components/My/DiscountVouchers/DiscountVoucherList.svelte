<script>
  import { onMount } from 'svelte';
  import {
    myDiscountVouchersStore,
    myDiscountVouchersActions
  } from '$components/My/DiscountVouchers/my-discount-vouchers.js';
  import { formatDate, isPast } from '$lib/util.js';
  import Pagination from '$components/common/Pagination.svelte';
  import { defaultPageSize } from '$lib/config.js';

  onMount(async () => {
    await myDiscountVouchersActions.getDiscountVouchers(
      state.pageInfo?.skip || 0,
      state.pageInfo?.take || defaultPageSize
    );
  });

  /**
   * Handle page change.
   * @param {number} skip
   * @param {number} take
   */
  async function onPageChange(skip, take) {
    await myDiscountVouchersActions.getDiscountVouchers(skip, take);
  }

  $: state = $myDiscountVouchersStore;
</script>

<div class="row">
  <div class="card-text">
    {#if state.discountVouchers && state.discountVouchers.length > 0}
      <div class="table-responsive small">
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Code</th>
              <th>Amount</th>
              <th>Balance</th>
              <th>Expires</th>
              <th>Issued</th>
            </tr>
          </thead>
          <tbody>
            {#each state.discountVouchers as voucher, i (voucher.id)}
              <tr class:table-secondary={i % 2 === 0}>
                <td>{voucher.code}</td>
                <td>R{voucher.amount.toFixed(0)}</td>
                <td>R{voucher.balance.toFixed(0)}</td>
                <td
                  class="text-nowrap"
                  class:text-warning={isPast(voucher.expirationDate)}>
                  {#if voucher.expirationDate}
                    {formatDate(voucher.expirationDate)}
                  {:else}
                    <span class="text-muted">Never</span>
                  {/if}
                </td>
                <td class="text-nowrap">{formatDate(voucher.issuedAt)}</td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
      {#if state.pageInfo}
        <Pagination
          pageInfo={state.pageInfo}
          {onPageChange} />
      {/if}
    {:else if !state.isLoading}
      <div class="row">
        <div class="col text-center d-flex flex-column gap-3 mt-3">
          We didn't find any vouchers in your name.
          <span
            ><a
              href="/contact-us"
              title="Contact us"
              class="card-link">Contact us</a> to find out more.</span>
        </div>
      </div>
    {/if}
  </div>
</div>
