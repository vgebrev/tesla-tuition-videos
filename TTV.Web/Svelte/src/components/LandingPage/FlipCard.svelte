<div class="card-container col-12 col-sm-12 col-md-6 col-lg-4 py-3 px-4">
	<div class="card-flip">
		<div class="card front border-primary rounded-3">
			<div class="card-body bg-secondary rounded-3 h-100 p-3">
				<div class="h-100 d-flex flex-column text-center">
					<div class="flex-grow-1 d-flex flex-column justify-content-center">
						<slot name="front" />
					</div>
					<div class="mx-auto w-100 text-end text-primary">
						<i class="bi bi-arrow-return-right"></i>
					</div>
				</div>
			</div>
		</div>
		<div class="card back border-primary rounded-3">
			<div class="h-100 card-block bg-secondary rounded-3 p-3">
				<slot name="back" />
			</div>
		</div>
	</div>
</div>

<style>
	.card {
		margin: 10px 0;
	}

	.card-container {
		display: grid;
		perspective: 700px;
	}

	.card-flip {
		display: grid;
		grid-template: 1fr / 1fr;
		grid-template-areas: 'frontAndBack';
		transform-style: preserve-3d;
		transition: all 0.4s ease;
		cursor: pointer;
	}

	.card-flip div {
		backface-visibility: hidden;
		transform-style: preserve-3d;
	}

	.front {
		grid-area: frontAndBack;
	}

	.back {
		grid-area: frontAndBack;
		transform: rotateY(-180deg);
	}

	.card-container:hover .card-flip {
		transform: rotateY(180deg);
	}
</style>
