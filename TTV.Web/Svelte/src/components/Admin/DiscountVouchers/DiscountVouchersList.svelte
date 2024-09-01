<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import {
    adminDiscountVouchersStore,
    adminDiscountVouchersActions
  } from '$components/Admin/DiscountVouchers/discount-vouchers.js';
  import { onMount } from 'svelte';
  import { formatDate, formatDateTime } from '$lib/util.js';

  onMount(async () => {
    await adminDiscountVouchersActions.getDiscountVouchers();
  });

  /**
   * Check if the given date is in the past.
   * @param {Date | string | null} date
   */
  function isPast(date) {
    if (!date) return false;
    let today = new Date();
    today.setHours(0, 0, 0, 0);
    return new Date(date) < today;
  }
  $: state = $adminDiscountVouchersStore;
</script>

<AuthorizeView authorizationPolicy="admin">
  <div slot="authorized">
    {#if state.discountVouchers && state.discountVouchers.length > 0}
      <div class="table-responsive">
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Code</th>
              <th>Amount</th>
              <th>Balance</th>
              <th>Expires</th>
              <th>Issued</th>
              <th>Issued By</th>
              <th>Used By</th>
              <th>Note</th>
            </tr>
          </thead>
          <tbody>
            {#each state.discountVouchers as voucher, i (voucher.id)}
              <tr class:table-secondary={i % 2 === 0}>
                <td>{voucher.code}</td>
                <td>R{voucher.amount.toFixed(0)}</td>
                <td>R{voucher.balance.toFixed(0)}</td>
                <td
                  class="nowrap"
                  class:text-warning={isPast(voucher.expirationDate)}>
                  {#if voucher.expirationDate}
                    {formatDate(voucher.expirationDate)}
                  {:else}
                    <span class="text-muted">Never</span>
                  {/if}
                </td>
                <td class="nowrap">{formatDateTime(voucher.issuedAt)}</td>
                <td>{voucher.issuedBy.email}</td>
                <td>
                  {#if voucher.claimedBy}
                    {voucher.claimedBy.email}
                  {:else}
                    <span class="text-muted">Not used</span>
                  {/if}
                </td>
                <td class="nowrap">{voucher.note || ''}</td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
    {:else if !state.isLoading}
      <p>No discount vouchers issued.</p>
    {/if}
  </div>
</AuthorizeView>

<style>
  td.nowrap {
    white-space: nowrap;
  }
</style>
