export function payfastModal(paymentId, returnUrl, cancelUrl) {
  window.payfast_do_onsite_payment({
    "uuid": paymentId,
    "return_url": returnUrl,
    "cancel_url": cancelUrl,
  });
}