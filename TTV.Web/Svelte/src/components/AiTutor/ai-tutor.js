import { writable } from 'svelte/store';
import { api } from '$lib/api.js';

export const aiTutorAnswer = writable('');

export async function askAiTutor(question) {
  aiTutorAnswer.set('Thinking...');

  const response = await api.fetch('/ai-tutor/ask', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ userInput: question })
  });

  const reader = response.body.getReader();
  const decoder = new TextDecoder();
  aiTutorAnswer.set('');
  while (true) {
    const { done, value } = await reader.read();
    if (done) break;
    aiTutorAnswer.update((text) => text + decoder.decode(value));
  }
}
