<script>
  import { onMount } from 'svelte';
  import {
    myDiscountVouchersStore,
    myDiscountVouchersActions
  } from '$components/My/DiscountVouchers/my-discount-vouchers.js';
  import { formatDate } from '$lib/util.js';

  onMount(async () => {
    await myDiscountVouchersActions.getDiscountVouchers();
  });

  $: state = $myDiscountVouchersStore;
</script>

<div class="row">
  <div class="card-text">
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
            </tr>
          </thead>
          <tbody>
            {#each state.discountVouchers as voucher, i (voucher.id)}
              <tr class:table-secondary={i % 2 === 0}>
                <td>{voucher.code}</td>
                <td>R{voucher.amount.toFixed(0)}</td>
                <td>R{voucher.balance.toFixed(0)}</td>
                <td
                  nowrap
                  class:text-warning={voucher.expirationDate < new Date().setHours(0, 0, 0, 0)}>
                  {#if voucher.expirationDate}
                    {formatDate(voucher.expirationDate)}
                  {:else}
                    <span class="text-muted">Never</span>
                  {/if}
                </td>
                <td nowrap>{formatDate(voucher.issuedAt)}</td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
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
