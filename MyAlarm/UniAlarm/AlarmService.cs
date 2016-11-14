using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UniAlarm
{
    internal class AlarmService
    {
        private readonly string appDir;
        private SoundPlayer player = new SoundPlayer();

        public AlarmService(string appDir)
        {
            this.appDir = appDir;
        }

        public void TryPlaySound()
        {
            var player = this.player;
            if (player == null)
            {
                player = new SoundPlayer();
            }

            var file = Path.Combine(this.appDir, "alarms.txt");
            player.SoundLocation = File.ReadAllText(file);
            player.Load();
            player.Play();
        }

        public void Stop()
        {
            var palyer = this.player;

            if (player != null)
            {
                player.Dispose();
            }

            this.player = null;
        }
    }
}
