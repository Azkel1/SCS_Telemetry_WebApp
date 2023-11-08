<script lang="ts">
	import '../app.scss';

	import FullScreenEnterIcon from '$lib/icons/fullscreen-enter.svg?component';
	import FullScreenExitIcon from '$lib/icons/fullscreen-exit.svg?component';

	let isFullscreen = document.fullscreenElement !== null;

	document.addEventListener('fullscreenchange', (event) => {
		isFullscreen = document.fullscreenElement !== null;
	});
</script>

{#if !isFullscreen}
	<button
		id="fullscreen-toggle"
		on:click={() => {
			document.body.requestFullscreen();
		}}
	>
		<FullScreenEnterIcon />
	</button>
{:else}
	<button
		id="fullscreen-toggle"
		on:click={() => {
			document.exitFullscreen();
		}}
	>
		<FullScreenExitIcon />
	</button>
{/if}

<slot />

<style lang="scss">
	#fullscreen-toggle {
		@include glassmorphism();

		cursor: pointer;

		padding: 0.2rem;
		position: fixed;
		top: var(--size-3);
		left: var(--size-3);
		z-index: var(--layer-5);
		width: 2.5rem;

		:global(svg) {
			block-size: unset;
			inline-size: unset;
		}
	}
</style>
