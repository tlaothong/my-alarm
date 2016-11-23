using Microsoft.ApplicationInsights;
using System;
using Topshelf;

namespace UniAlarm
{
    class Program
    {
        private static readonly TelemetryClient telemetry = new TelemetryClient();

        static void Main(string[] args)
        {
            var appDir = AppContext.BaseDirectory;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HostFactory.Run(fac =>
            {
                fac.Service<AlarmService>(cfg =>
                {
                    cfg.ConstructUsing(() => new AlarmService(appDir))
                        .WhenStarted(svc => svc.Start())
                        .WhenStopped(svc => svc.Stop());
                });
                fac.SetDescription("Universal Alarm App/Service");
                fac.SetDisplayName("Universal Alarm Service");
                fac.SetServiceName("UniAlarm");

                // Fix permission for some machines!
                fac.RunAsLocalSystem();
            });
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            telemetry.TrackException(e.ExceptionObject as Exception);
        }
    }
}
