<script>
  export let showStatus = false;
  export let status = 500;
  export let errorMessage = 'Unexpected Error';

  const errorMessages = {
    404: [
      "Experiment Failed! We lost this page in a lab explosion. Let's try a different formula!",
      "Page Not Found! It must've evaporated in the heat of the experiment.",
      'Lost in Space! This page is floating somewhere in the ether.',
      "Out of Bounds! The particles you're looking for have gone rogue.",
      'Data Not Found! This page is as elusive as dark matter.'
    ],
    403: [
      'Access Denied! Only authorized personnel can enter this lab.',
      "Keep Out! You're not cleared for this top-secret experiment.",
      'Forbidden! You need a higher clearance level to access this reactor.',
      'Hands Off! This data is classified beyond your clearance level.',
      'Halt! Unauthorized access is against lab protocol.'
    ],
    500: [
      'Lab Explosion! Something went wrong in the experiment—better call the cleanup crew!',
      "System Overload! The server just couldn't handle our latest experiment.",
      'Critical Failure! The server had a meltdown—time to cool things down.',
      "Whoops! The atoms didn't collide as expected. We're working on it!",
      "Experiment Gone Wrong! Our server's Bunsen burner just went out. We'll relight it soon."
    ]
  };

  /**
   * Get a random error message for the given error code.
   * @param {number} errorCode
   */
  function getRandomMessage(errorCode) {
    const messages = errorMessages[errorCode] || ['An unknown error occurred.'];
    return messages[Math.floor(Math.random() * messages.length)];
  }

  $: message = getRandomMessage(status);
</script>

<main class="container text-center mt-5">
  <h3 class="text-warning">
    {#if showStatus}{status} -
    {/if}{errorMessage}
  </h3>
  <p class="lead">{message}</p>
  <a
    href="/"
    class="mt-4">Back to Home</a>
</main>
