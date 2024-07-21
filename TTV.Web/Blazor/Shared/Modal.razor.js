export function openModal(element) {
  const modal = bootstrap.Modal.getOrCreateInstance(element);
  modal.show();
}

export function closeModal(element) {
  const modal = bootstrap.Modal.getOrCreateInstance(element);
  modal.hide();
}