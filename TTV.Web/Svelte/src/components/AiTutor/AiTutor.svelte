<script>
  import { aiTutorAnswer, askAiTutor } from './ai-tutor.js';
  import { marked } from 'marked';
  let userInput = '';

  /**
   * @param {KeyboardEvent} event
   */
  async function keyPressed(event) {
    if (event.key === 'Enter') {
      await ask();
    }
  }

  async function ask() {
    await askAiTutor(userInput);
  }

  $: markdown = marked.parse($aiTutorAnswer);
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">AI Tutor</h1>
  </div>
</div>

<div class="form-group">
  <div class="row g-3">
    <div class="col-12 col-md-8 col-lg-9">
      <div class="form-floating">
        <input
          type="text"
          class="form-control"
          id="ai-tutor-question"
          bind:value={userInput}
          on:keypress={keyPressed}
          placeholder="Ask a question..." />
        <label for="ai-tutor-question">Question</label>
      </div>
    </div>
    <div class="col-12 col-md-4 col-lg-3">
      <div class="d-flex gap-3 h-100">
        <button
          class="btn btn-outline-primary flex-fill h-100"
          type="button"
          on:click={ask}>
          <i class="bi bi-robot me-1"></i> <span>Ask</span>
        </button>
      </div>
    </div>
  </div>
</div>

<div class="row my-2">
  <div class="col">
    <div class="response pre-wrap">
      {@html markdown}
    </div>
  </div>
</div>
