<script>
  import AiTutor from '$components/AiTutor/AiTutor.svelte';
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
</script>

<svelte:head>
  <title>Tesla Tuition Videos - AI Tutor</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <AiTutor />
  </div>
</AuthorizeRoute>
