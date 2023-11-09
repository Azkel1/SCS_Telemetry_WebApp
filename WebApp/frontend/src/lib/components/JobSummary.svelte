<!-- ---------------------------------------------------------- JS/TS ---------------------------------------------------------- -->
<script lang="ts">
	import { spring } from 'svelte/motion';
	import TruckIcon from '$lib/icons/truck.svg?component';

	export let travelledDistance: number;
	export let totalDistance: number;
	export let origin: string;
	export let destination: string;

	let totalPercentage = spring(0);
	$: remainingDistance = (totalDistance - travelledDistance).toFixed(1);
	$: totalPercentage.set((travelledDistance * 100) / totalDistance);
</script>

<!-- ----------------------------------------------------------- HTML ---------------------------------------------------------- -->
<section id="job-summary">
	<article id="cargo-info">
		<div id="distances-wrapper">
			<span id="total-distance" class="tag">Total: {totalDistance}km</span>
			<span id="remaining-distance" class="tag">Remaining: {remainingDistance}km</span>
		</div>

		<div id="meter-wrapper">
			<div
				id="meter"
				role="meter"
				aria-valuenow={travelledDistance}
				style:width="{$totalPercentage}%"
			>
				<TruckIcon id="truck-icon" />
			</div>
		</div>

		<div id="destinations-wrapper">
			<span id="origin" class="tag">From: {origin}</span>
			<span id="destination" class="tag">To: {destination}</span>
		</div>
	</article>
</section>

<!-- ----------------------------------------------------------- CSS ----------------------------------------------------------- -->
<style lang="scss">
	#job-summary {
		align-items: center;
		text-align: center;

		display: flex;
		flex-direction: column;
		gap: 1em;

		.tag {
			background-color: #444;
			border-radius: 0.25em;
			box-shadow: var(--shadow-2);
			color: #eee;
			padding: 0 0.3em;
		}

		> #cargo-info {
			display: grid;
			gap: 2.5rem;

			position: relative;
			width: 100%;

			> #distances-wrapper {
				display: flex;
				justify-content: space-between;

				> #total-distance {
					background-color: #444;
					border-radius: 0.25em;
					box-shadow: var(--shadow-2);
					color: #eee;
					padding: 0 0.3em;
				}

				> #remaining-distance {
					background-color: #444;
					border-radius: 0.25em;
					box-shadow: var(--shadow-2);
					color: #eee;
					padding: 0 0.3em;
				}
			}

			> #meter-wrapper {
				width: 90%;
				position: relative;
				margin-inline: auto;

				&::before {
					background-color: #eee;
					content: '';
					display: block;
					height: 3px;
					width: 100%;

					position: absolute;
					top: 50%;
					transform: translateY(-50%);
				}

				> #meter {
					border-radius: 7px;

					display: flex;
					justify-content: flex-end;

					height: 100%;
					position: relative;
					transition: background-color 300ms;

					> :global(#truck-icon) {
						background-color: #333;

						display: block;

						position: absolute;
						right: 0;
						inline-size: 3rem;
						max-inline-size: unset;
						padding: 0.25rem;
						transform: translate(50%, -50%);
					}
				}
			}

			> #destinations-wrapper {
				display: flex;
				justify-content: space-between;
				gap: 1em;
			}
		}
	}
</style>
