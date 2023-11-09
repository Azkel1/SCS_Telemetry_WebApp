using Fleck;
using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using SCSSdkClient;
using SCSSdkClient.Object;
using System.Net;
using WebSocketProxy;

namespace SCS_Telemetry_WebApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TcpProxyConfiguration configuration = new()
            {
                PublicHost = new Host()
                {
                    IpAddress = IPAddress.Parse("0.0.0.0"),
                    Port = 8080
                },
                HttpHost = new Host()
                {
                    IpAddress = IPAddress.Loopback,
                    Port = 8081
                },
                WebSocketHost = new Host()
                {

                    IpAddress = IPAddress.Loopback,
                    Port = 8082
                }
            };

            using var websocketServer = new WebSocketServer("ws://0.0.0.0:8082");
            using var tcpProxy = new TcpProxyServer(configuration);
            using var nancyHost = new NancyHost(new HostConfiguration()
            {
                UrlReservations = new UrlReservations()
                {
                    CreateAutomatically = true
                }
            }, new Uri("http://localhost:8081"));

            // Start the HTTP server
            nancyHost.Start();

            // Connect to the game SDK and start listening for events
            GameTelemetry.Start();

            // Start the WebSocket server
            websocketServer.Start(connection =>
            {
                TelemetryData dataHandler = (SCSTelemetry data, bool updated) =>
                {
                    if (updated)
                    {
                        connection.Send(GameTelemetry.GetSerializedData());
                    }
                };

                connection.OnOpen = () =>
                {
                    Console.WriteLine($"Client connected: {connection.ConnectionInfo.Id}");

                    // Handle game telemetry events and broadcast them to each websocket client
                    GameTelemetry.RawGameTelemetry.Data += dataHandler;
                };

                connection.OnClose = () =>
                {
                    Console.WriteLine($"Client disconnected: {connection.ConnectionInfo.Id}");

                    // Remove the event listener from the game SDK
                    GameTelemetry.RawGameTelemetry.Data -= dataHandler;
                };
            });

            tcpProxy.Start();

            Console.WriteLine("Press [Enter] to stop");
            Console.ReadLine();
        }
    }

    public class Controller : NancyModule
    {
        public Controller()
        {
            Get("/", _ => View["index.html"]);
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("", @"views")
            );
        }
    }
}