//     This code was generated by a Reinforced.Typings tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

declare module GameSDK {
	export interface GameTelemetry
	{
		isGamePaused: boolean;
		gameTime: string;
		timeToSleep: string;
		currentSpeed: GameSDK.Speed;
		speedLimit: GameSDK.Speed;
		coordinates: GameSDK.Coordinates;
		fuelAmount: number;
		totalFuelCapacity: number;
		estimatedFuelRange: number;
		jobDistance: number;
		currentJobTravelledDistance: number;
		originCity: string;
		destinationCity: string;
	}
	export interface Speed
	{
		kph: number;
		mph: number;
	}
	export interface Coordinates
	{
		latitude: string;
		longitude: string;
	}
}