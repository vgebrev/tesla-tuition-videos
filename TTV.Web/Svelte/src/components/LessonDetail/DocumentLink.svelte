<script>
  import { config } from '$lib/config.js';
  import { authStore } from '$lib/auth.js';

  /** @type {import('$lib/types').Document} */
  export let document;

  $: documentUri = `${config.api.baseUrl}/documents/${document.id}${$authStore.isAuthenticated ? `?access_token=${$authStore.user.access_token}` : ''}`;
</script>

<a href={documentUri} title={document.title}
  ><i class="bi bi-file-earmark-pdf"></i> {document.title}</a
>
