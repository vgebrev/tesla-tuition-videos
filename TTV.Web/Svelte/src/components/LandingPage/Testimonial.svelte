<script>
  /** @type {import('$lib/types').Testimonial} */
  export let testimonial;
</script>

<div class="row d-flex justify-content-center">
  <div class="col-12 col-sm-11 col-md-8 col-lg-9 col-xl-8">
    <div class="d-flex">
      <div class="flex-grow-1 align-items-center d-flex justify-content-center">
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

<style>
  figure {
    transform: scale(0.75);
  }

  @media (min-width: 576px) {
    figure {
      transform: scale(0.82);
    }
  }

  @media (min-width: 768px) {
    figure {
      transform: scale(0.9);
    }
  }

  @media (min-width: 992px) {
    figure {
      transform: scale(1);
    }
  }

  @media (min-width: 1200px) {
    figure {
      transform: scale(1);
    }
  }

  @media (min-width: 1400px) {
    figure {
      transform: scale(1);
    }
  }
</style>
