<script>
  /** @type {Testimonial} */
  export let testimonial;
</script>

<div class="row d-flex justify-content-center">
  <div class="col-12 col-sm-11 col-md-8 col-lg-9 col-xl-8">
    <div class="d-flex">
      <div class="flex-grow-1">
        <figure>
          <blockquote class="blockquote mb-4">
            <p>
              <i class="bi bi-quote bi-lg text-warning me-2"></i>
              <span> {testimonial.text}</span>
            </p>
          </blockquote>
          <figcaption class="blockquote-footer">
            {testimonial.testimonialBy}
          </figcaption>
        </figure>
      </div>
    </div>
  </div>
</div>
