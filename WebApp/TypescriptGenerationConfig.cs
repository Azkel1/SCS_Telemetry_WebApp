using Reinforced.Typings.Fluent;

namespace SCS_Telemetry_WebApp
{
    public static class ReinforcedTypingsConfiguration
    {
        public static void Configure(ConfigurationBuilder builder)
        {
            builder.Global(conf => conf.ExportPureTypings());

            builder
                .ExportAsClasses(new Type[]
                {
                    typeof(GameTelemetry),
                    typeof(Speed),
                    typeof(Coordinates)

                }, conf => conf
                    .WithPublicProperties()
                    .OverrideNamespace("GameSDK")
                );
        }
    }
}
