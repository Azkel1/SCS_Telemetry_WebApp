<!-- ---------------------------------------------------------- JS/TS ---------------------------------------------------------- -->
<script lang="ts">
	import Clock from '$lib/components/Clock.svelte';
	import Speedo from '$lib/components/Speedo.svelte';
	import Fuel from '$lib/components/Fuel.svelte';
	import StatusIcons from '$lib/components/StatusIcons.svelte';
	import JobSummary from '$lib/components/JobSummary.svelte';
	import { telemetry } from '$lib/stores';
</script>

<!-- ----------------------------------------------------------- HTML ---------------------------------------------------------- -->
{#if $telemetry}
	<main id="main">
		<Clock currentDateTime={$telemetry.gameTime} timeToSleep={$telemetry.timeToSleep} />
		<Speedo
			currentSpeed={$telemetry.currentSpeed.kph}
			currentLimit={$telemetry.speedLimit.kph}
			coords={{
				long: $telemetry.coordinates.latitude,
				lat: $telemetry.coordinates.longitude
			}}
		/>
		<Fuel
			currentLiters={$telemetry.fuelAmount}
			totalCapacity={$telemetry.totalFuelCapacity}
			remainingDistance={$telemetry.estimatedFuelRange}
		/>
		<StatusIcons />
		<JobSummary
			totalDistance={$telemetry.jobDistance}
			travelledDistance={$telemetry.currentJobTravelledDistance}
			origin={$telemetry?.originCity}
			destination={$telemetry?.destinationCity}
		/>
	</main>
{/if}

<!-- ----------------------------------------------------------- CSS ----------------------------------------------------------- -->
<style lang="scss">
	#main {
		display: grid;

		grid-template-areas:
			'clock'
			'speedo'
			'fuel'
			'status-icons'
			'job-summary';

		grid-template-rows: 1.25fr auto 0.75fr 1fr 4fr;
		min-height: 100vh;

		:global {
			> * {
				box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
				padding: 1rem; /* FIXME: This padding increases page height */
			}

			> #clock {
				grid-area: clock;
			}

			> #speedo {
				grid-area: speedo;
			}

			> #fuel {
				grid-area: fuel;
			}

			> #status-icons {
				grid-area: status-icons;
			}

			> #job-summary {
				grid-area: job-summary;
			}
		}
	}
</style>
