<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import MyLessons from '$components/My/Lessons/MyLessons.svelte';
</script>

<svelte:head>
  <title>Tesla Tuition Videos - My Lessons</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <MyLessons />
  </div>
</AuthorizeRoute>
