export function handleOffcanvasHidden(element, dotnet) {
  element.addEventListener('hidden.bs.offcanvas', function () {
    dotnet.invokeMethodAsync('OnOffcanvasHidden');
  });
}
export function openOffcanvas(element) {
  const offcanvas = bootstrap.Offcanvas.getOrCreateInstance(element);
  offcanvas.show();
}

export function closeOffcanvas(element) {
  const offcanvas = bootstrap.Offcanvas.getOrCreateInstance(element);
  offcanvas.hide();
}