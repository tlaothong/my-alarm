using System;
using Topshelf;

namespace UniAlarm
{
    class Program
    {
        static void Main(string[] args)
        {
            var appDir = AppContext.BaseDirectory;

            HostFactory.Run(fac =>
            {
                fac.Service<AlarmService>(cfg =>
                {
                    cfg.ConstructUsing(() => new AlarmService(appDir))
                        .WhenStarted(svc => svc.TryPlaySound())
                        .WhenStopped(svc => svc.Stop());
                });
                fac.SetDescription("Universal Alarm App/Service");
                fac.SetDisplayName("Universal Alarm Service");
                fac.SetServiceName("UniAlarm");

                fac.RunAsLocalService();
            });
        }
    }
}
