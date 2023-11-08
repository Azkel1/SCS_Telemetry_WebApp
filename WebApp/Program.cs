using Fleck;
using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using SCSSdkClient;
using SCSSdkClient.Object;
using System.Net;
using System.Text.Json;
using WebSocketProxy;

namespace SCS_Telemetry_WebApp
{
    internal class Program
    {
        private static SCSSdkTelemetry? _telemetry = null;

        public static SCSSdkTelemetry Telemetry
        {
            get
            {
                _telemetry ??= new SCSSdkTelemetry(200);

                return _telemetry;
            }
        }

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

            // Start the WebSocket server
            websocketServer.Start(connection =>
            {
                connection.OnOpen = () => Console.WriteLine($"Client connected: {connection.ConnectionInfo.Id}");
                connection.OnClose = () => Console.WriteLine($"Client disconnected: {connection.ConnectionInfo.Id}");

                // Handle game telemetry events and broadcast them to each websocket client
                Telemetry.Data += (SCSTelemetry data, bool updated) =>
                {
                    if (updated)
                    {
                        GameTelemetry gameTelemetry = new GameTelemetry(data);

                        connection.Send(JsonSerializer.Serialize(gameTelemetry));
                    }
                };

                // Handle button presses on the client UI and forward them to the game
                //connection.OnMessage = (string message) =>
                //{
                //    Message parsedMessage = JsonConvert.DeserializeObject<Message>(message);

                //    Console.WriteLine($"Message: {parsedMessage}");
                //};
            });

            tcpProxy.Start();

            Console.WriteLine("Press [Enter] to stop");
            Console.ReadLine();
        }
    }

    class Message
    {
        public required string Command { get; set; }
        public required object Data { get; set; }
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