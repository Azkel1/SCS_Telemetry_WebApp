using Reinforced.Typings.Attributes;
using SCSSdkClient;
using SCSSdkClient.Object;
using System.Text.Json;

namespace SCS_Telemetry_WebApp
{
    public struct Speed
    {
        public int Kph { get; set; }
        public int Mph { get; set; }
    }

    public struct Coordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class GameTelemetry
    {
        private static SCSSdkTelemetry? _rawTelemetry = null;
        private static bool storeTruckOdometer = false;
        private static float? truckOdometer = null;

        [TsIgnore]
        public static SCSSdkTelemetry RawGameTelemetry
        {
            get
            {
                _rawTelemetry ??= new SCSSdkTelemetry(200);

                return _rawTelemetry;
            }
        }

        public static bool IsGamePaused { get; private set; }
        public static string? GameTime { get; private set; }
        public static string? TimeToSleep { get; private set; }

        public static Speed CurrentSpeed { get; private set; }
        public static Speed SpeedLimit { get; private set; }
        public static Coordinates Coordinates { get; private set; }

        public static float FuelAmount { get; private set; }
        public static float TotalFuelCapacity { get; private set; }
        public static float EstimatedFuelRange { get; private set; }

        public static float JobDistance { get; private set; }
        public static float CurrentJobTravelledDistance { get; private set; }
        public static string? OriginCity { get; private set; }
        public static string? DestinationCity { get; private set; }

        [TsIgnore]
        public static void Start()
        {
            RawGameTelemetry.Data += (SCSTelemetry data, bool updated) =>
            {
                if (updated)
                {
                    if (storeTruckOdometer && !data.Paused)
                    {
                        truckOdometer = data.TruckValues.CurrentValues.DashboardValues.Odometer;
                        storeTruckOdometer = false;
                    }

                    DateTime _gameTime = data.CommonValues.GameTime.Date;
                    DateTime _timeToSleep = data.CommonValues.NextRestStop.Date;

                    IsGamePaused = data.Paused;
                    GameTime = $"{_gameTime.Hour}:{_gameTime.Minute}";
                    TimeToSleep = $"{_timeToSleep.Hour}:{_timeToSleep.Minute}";

                    CurrentSpeed = new Speed
                    {
                        Kph = (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph),
                        Mph = (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.Speed.Mph)
                    };
                    SpeedLimit = new Speed
                    {
                        Kph = (int)Math.Round(data.NavigationValues.SpeedLimit.Kph),
                        Mph = (int)Math.Round(data.NavigationValues.SpeedLimit.Mph),
                    };
                    Coordinates = new Coordinates
                    {
                        Latitude = string.Format("{0:0.###}", data.TruckValues.Positioning.CabinPositionInWorlSpace.Y),
                        Longitude = string.Format("{0:0.###}", data.TruckValues.Positioning.CabinPositionInWorlSpace.X),
                    };

                    FuelAmount = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                    TotalFuelCapacity = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                    EstimatedFuelRange = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range;

                    JobDistance = data.JobValues.PlannedDistanceKm;
                    OriginCity = data.JobValues.CitySource;
                    DestinationCity = data.JobValues.CityDestination;

                    if (data.SpecialEventsValues.OnJob && truckOdometer is not null)
                    {
                        CurrentJobTravelledDistance = data.TruckValues.CurrentValues.DashboardValues.Odometer - (float)truckOdometer;
                    }
                    else
                    {
                        CurrentJobTravelledDistance = 0;
                    }
                }
            };

            RawGameTelemetry.JobStarted += (object? sender, EventArgs e) =>
            {
                storeTruckOdometer = true;
                truckOdometer = null;
            };
        }

        [TsIgnore]
        public static string GetSerializedData()
        {
            return JsonSerializer.Serialize(new
            {
                IsGamePaused,
                GameTime,
                TimeToSleep,
                CurrentSpeed,
                SpeedLimit,
                Coordinates,
                FuelAmount,
                TotalFuelCapacity,
                EstimatedFuelRange,
                JobDistance,
                CurrentJobTravelledDistance,
                OriginCity,
                DestinationCity
            }, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
