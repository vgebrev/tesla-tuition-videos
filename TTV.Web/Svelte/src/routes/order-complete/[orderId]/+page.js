import { error } from '@sveltejs/kit';

/**
 * @param {{params: { orderId: string}}} params
 * @returns
 */
export function load({ params }) {
  const orderId = parseInt(params.orderId);
  if (isNaN(orderId)) {
    error(404, { message: 'Not Found' });
  }

  return {
    orderId
  };
}
