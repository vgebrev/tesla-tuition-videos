<script>
  import { authStore } from '$lib/auth.js';
  import { shoppingCartActions } from './shopping-cart.js';
  import { myLessonsActions, myLessonsStore } from '$components/My/Lessons/my-lessons.js';
  import { onDestroy } from 'svelte';

  const unsubscribe = authStore.subscribe(async (authState) => {
    if (!authState.isAuthenticated || authState.origin !== 'login-callback') return;
    let lessons = [];
    try {
      shoppingCartActions.loadFromLocalStorage();
      await myLessonsActions.getOwnedLessons();
      lessons = $myLessonsStore.lessons || [];
    } catch (e) {
      console.error(e);
    } finally {
      for (let lesson of lessons) {
        shoppingCartActions.removeLesson(lesson);
      }
    }
  });

  onDestroy(() => {
    unsubscribe();
  });
</script>
