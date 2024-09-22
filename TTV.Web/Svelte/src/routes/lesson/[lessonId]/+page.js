import { error } from '@sveltejs/kit';

/**
 * @param {{params: { lessonId: string}}} params
 * @returns
 */
export function load({ params }) {
  const lessonId = parseInt(params.lessonId);
  if (isNaN(lessonId)) {
    error(404, { message: 'Not Found' });
  }
  return {
    lessonId
  };
}
