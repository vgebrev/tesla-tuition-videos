<script>
  import { authStore } from '$lib/auth.js';
  import { shoppingCartActions } from './shopping-cart.js';
  import { myLessonsActions, myLessonsStore } from '$components/My/Lessons/my-lessons.js';
  import { onDestroy } from 'svelte';

  const unsubscribeAuth = authStore.subscribe(
    /** @param {import('$lib/types').AuthStoreState} authState */
    async (authState) => {
      if (!authState.isAuthenticated || authState.origin !== 'login-callback') return;
      try {
        shoppingCartActions.loadFromLocalStorage();
        await myLessonsActions.getOwnedLessons();
      } catch (e) {
        console.error(e);
      }
    }
  );

  const unsubscribeMyLessons = myLessonsStore.subscribe((myLessonsState) => {
    const lessons = myLessonsState.lessons || [];
    for (let lesson of lessons) {
      shoppingCartActions.removeLesson(lesson);
    }
  });

  onDestroy(() => {
    unsubscribeAuth();
    unsubscribeMyLessons();
  });
</script>
