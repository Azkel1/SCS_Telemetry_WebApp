<!-- ---------------------------------------------------------- JS/TS ---------------------------------------------------------- -->
<script lang="ts">
    import SleepIcon from '$lib/icons/sleep.svg?component';

    export let currentDateTime: string;
    export let timeToSleep: string;
</script>

<!-- ----------------------------------------------------------- HTML ---------------------------------------------------------- -->
<section id="clock">
    <strong id="ingame-time">{currentDateTime}</strong>
    <!-- TODO: Find a better element -->
    <span id="time-to-rest">
        <SleepIcon />
        {timeToSleep}
    </span>
</section>

<!-- ----------------------------------------------------------- CSS ----------------------------------------------------------- -->
<style lang="scss">
	#clock {
        display: grid;
        /* grid-auto-flow: column;
        grid-template-columns: 1fr auto; */
        justify-items: center;
        align-content: center;

        > #ingame-time {
            font-size: 2.5rem;
            line-height: 1;

            place-self: center;
        }

        > #time-to-rest {
            font-size: 1.25rem;
            display: flex;
            gap: 0.15em;
        }
	}
</style>
