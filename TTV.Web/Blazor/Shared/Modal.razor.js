export function openModal(id) {
  const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById(id));
  modal.show();
}

export function closeModal(id) {
  const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById(id));
  modal.hide();
}