import { dev } from '$app/environment';
import { type Readable, readable } from 'svelte/store';

const websocketURL = `ws://${window.location.hostname}:8080`;

export const telemetry: Readable<null | GameSDK.GameTelemetry> = (() => {
	const websocket = new WebSocket(websocketURL);

	const { subscribe } = readable<null | GameSDK.GameTelemetry>(null, (set) => {
		// Wait for the WebSocket connection to open before listening for messages
		websocket.addEventListener('open', () => {
			// TODO: Additionally we should also handle errors / websocket connection closing

			websocket.addEventListener('message', (event) => {
				const gameData = JSON.parse(event.data);

				set(gameData);
				//set(gameData.SdkActive ? gameData : null);
			});
		});
	});

	return {
		subscribe,
		websocket
	};
})();
