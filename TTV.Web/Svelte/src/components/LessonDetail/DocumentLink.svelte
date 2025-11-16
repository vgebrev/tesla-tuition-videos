<script>
  import { config } from '$lib/config.js';
  import { authStore } from '$lib/auth.js';

  /** @type {import('$lib/types').Document} */
  export let document;

  // External API URL - doesn't need resolve()
  $: documentUri = `${config.api.baseUrl}/documents/${document.id}${$authStore.isAuthenticated ? `?access_token=${$authStore.user.access_token}` : ''}`;
</script>

<!-- eslint-disable svelte/no-navigation-without-resolve -->
<a
  href={documentUri}
  title={document.title}
  target="_blank"
  rel="noopener noreferrer"><i class="bi bi-file-earmark-pdf"></i> {document.title}</a>
<!-- eslint-enable svelte/no-navigation-without-resolve -->
