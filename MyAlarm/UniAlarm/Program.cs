using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace UniAlarm
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(fac =>
            {
                fac.Service<AlarmService>(cfg =>
                {
                    cfg.ConstructUsing(() => new AlarmService())
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
