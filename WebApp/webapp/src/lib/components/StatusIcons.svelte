<!-- ---------------------------------------------------------- JS/TS ---------------------------------------------------------- -->
<script lang="ts">
	import WarningLightsIcon from '$lib/icons/warning.svg?component';
	import LowBeamsIcon from '$lib/icons/low-beams.svg?component';
	import LowerAxleIcon from '$lib/icons/lower-axle.svg?component';
	import RadioIcon from '$lib/icons/radio.svg?component';
	import PowerOnIcon from '$lib/icons/power-on.svg?component';

	// TODO: Make the icons and their function load from the app settings
</script>

<!-- ----------------------------------------------------------- HTML ---------------------------------------------------------- -->
<section id="status-icons">
	<button>
		<LowerAxleIcon />
	</button>
	<button>
		<LowBeamsIcon />
	</button>
	<button>
		<PowerOnIcon />
	</button>
	<button>
		<WarningLightsIcon />
	</button>
	<button>
		<RadioIcon />
	</button>
</section>

<!-- ----------------------------------------------------------- CSS ----------------------------------------------------------- -->
<style lang="scss">
	#status-icons {
		display: grid;
		grid-template-columns: repeat(5, 1fr);

		> button {
			background-color: #444;
			border-radius: 0;
			border: 0;
			box-shadow: 0px 0 5px rgba(0, 0, 0, 0.25);
			font-size: 1.4em;
		}

		> button:first-of-type {
			border-radius: 50vw 0 0 50vw;
		}

		> button:last-of-type {
			border-radius: 0 50vw 50vw 0;
		}
	}
</style>
