using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniAlarm
{
    internal class AlarmService
    {
        private readonly string appDir;
        private SoundPlayer player = new SoundPlayer();
        private Timer timer;
        private TimeSpan timeToAlarm;
        private AlarmParser parser;

        public AlarmService(string appDir)
        {
            this.appDir = appDir;
            this.timer = new Timer(state => this.TimeToAlarm());

            // default alarms config file
            var configPath = Path.Combine(appDir, "alarms.txt");
            this.parser = new UniAlarm.AlarmParser(configPath);
        }

        public void Start()
        {
            var timer = this.timer;

            SetNextAlarm();
        }

        public void SetNextAlarm()
        {
            var now = DateTime.Now;
            var currentTime = now - now.Date;
            var alarm = this.parser.GetNextAlarm(currentTime);
            this.timeToAlarm = alarm.Time;
            this.timer.Change(alarm.Time - currentTime, TimeSpan.FromMilliseconds(-1));
        }

        public void TimeToAlarm()
        {
            var player = this.player;
            if (player == null)
            {
                player = new SoundPlayer();
                this.player = player;
            }
            var timeToPlay = this.timeToAlarm.Add(TimeSpan.FromMilliseconds(-1));
            var alarm = this.parser.GetNextAlarm(timeToPlay);
            player.SoundLocation = alarm.SoundPath;
            player.Load();
            player.Play();

            SetNextAlarm();
        }

        public void Stop()
        {
            var palyer = this.player;
            if (player != null)
            {
                player.Dispose();
            }
            this.player = null;

            var timer = this.timer;
            if (timer != null)
            {
                timer.Dispose();
            }
            this.timer = null;
        }
    }
}
