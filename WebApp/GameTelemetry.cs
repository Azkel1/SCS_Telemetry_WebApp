using SCSSdkClient.Object;

namespace SCS_Telemetry_WebApp
{
    public class Speed
    {
        public int kph { get; set; }
        public int mph { get; set; }
    }

    public class Coordinates
    {
        public required string latitude { get; set; }
        public required string longitude { get; set; }
    }

    public struct GameTelemetry
    {
        public string gameTime { get; set; }
        public string timeToSleep { get; set; }

        public Speed currentSpeed { get; set; }
        public Speed speedLimit { get; set; }
        public Coordinates coordinates { get; set; }

        public float fuelAmount { get; set; }
        public float totalFuelCapacity { get; set; }
        public float estimatedFuelRange { get; set; }

        public float jobDistance { get; set; }
        public float currentJobTravelledDistance { get; set; }
        public string originCity { get; set; }
        public string destinationCity { get; set; }

        public GameTelemetry(SCSTelemetry data)
        {
            DateTime _gameTime = data.CommonValues.GameTime.Date;
            DateTime _timeToSleep = data.CommonValues.NextRestStop.Date;

            gameTime = $"{_gameTime.Hour}:{_gameTime.Minute}";
            timeToSleep = $"{_timeToSleep.Hour}:{_timeToSleep.Minute}";

            currentSpeed = new Speed {
                kph = (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.Speed.Kph),
                mph = (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.Speed.Mph)
            };
            speedLimit = new Speed
            {
                kph = (int)Math.Round(data.NavigationValues.SpeedLimit.Kph),
                mph = (int)Math.Round(data.NavigationValues.SpeedLimit.Mph),
            };
            coordinates = new Coordinates
            {
                latitude = string.Format("{0:0.###}", data.TruckValues.Positioning.CabinPositionInWorlSpace.Y),
                longitude = string.Format("{0:0.###}", data.TruckValues.Positioning.CabinPositionInWorlSpace.X),
            };

            fuelAmount = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
            totalFuelCapacity = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
            estimatedFuelRange = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range;

            jobDistance = data.JobValues.PlannedDistanceKm;
            currentJobTravelledDistance = data.TruckValues.CurrentValues.DashboardValues.Odometer;
            originCity = data.JobValues.CitySource;
            destinationCity = data.JobValues.CityDestination;
        }
    }
}
