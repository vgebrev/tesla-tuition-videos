export function handleModalHidden(element, dotnet) {
  element.addEventListener('hidden.bs.modal', function () {
    dotnet.invokeMethodAsync('OnModalHidden');
  });
}

export function openModal(element) {
  const modal = bootstrap.Modal.getOrCreateInstance(element);
  modal.show();
}

export function closeModal(element) {
  const modal = bootstrap.Modal.getOrCreateInstance(element);
  modal.hide();
}