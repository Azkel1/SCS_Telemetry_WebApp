<!-- ---------------------------------------------------------- JS/TS ---------------------------------------------------------- -->
<script lang="ts">
	import { spring } from 'svelte/motion';

	export let currentLiters: number;
	export let totalCapacity: number;
    export let remainingDistance: number;

	let totalPercentage = spring(0);
	$: totalPercentage.set((currentLiters * 100) / totalCapacity);
	$: barColor = $totalPercentage * 1.25;
</script>

<!-- ----------------------------------------------------------- HTML ---------------------------------------------------------- -->
<section id="fuel-wrapper">
	<main id="fuel">
		<div
			id="meter"
			role="meter"
			aria-valuenow={currentLiters}
			style:width="{$totalPercentage}%"
			style:--hue={barColor}
		/>
		<span id="estimated-distance">{remainingDistance.toFixed(1)}km</span>
		<span id="current-liters">{currentLiters.toFixed(1)}L</span>
	</main>
</section>

<!-- ----------------------------------------------------------- CSS ----------------------------------------------------------- -->
<style lang="scss">
	#fuel-wrapper {
		display: inherit;

		> #fuel {
			background-color: #444;
			border-radius: 7px;
			position: relative;

			> #meter {
				background-color: hsl(var(--hue), 100%, 35%);
				border-radius: 7px;

				display: flex;
				justify-content: flex-end;

				height: 100%;
				position: relative;
				transition: background-color 300ms;
			}

			> #estimated-distance {
				color: #eee;
				text-shadow: 1px 1px 1px #444;
				font-weight: bold;
				position: absolute;
				left: 1.25ch;
				top: 50%;
				transform: translateY(-50%);
			}

			> #current-liters {
				color: #eee;
				text-shadow: 1px 1px 1px #444;
				font-weight: bold;
				position: absolute;
				right: 1.25ch;
				top: 50%;
				transform: translateY(-50%);
			}
		}
	}
</style>
