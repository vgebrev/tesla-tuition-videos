<script>
  export let user;

  /**
   * Get the icon class for the given provider.
   * @param {string} provider
   * @returns {string}
   */
  function getProviderIconClass(provider) {
    /** @type {Record<string, string>} */
    const icons = {
      'TTV Account': 'bi bi-person',
      Google: 'bi bi-google'
    };
    return icons[provider] || '';
  }
</script>

<i
  class={getProviderIconClass(user.provider)}
  title={user.provider}></i>
{user.email}
