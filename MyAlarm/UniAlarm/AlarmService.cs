using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UniAlarm
{
    internal class AlarmService
    {
        private SoundPlayer player = new SoundPlayer();

        public void TryPlaySound()
        {
            var player = this.player;
            if (player == null)
            {
                player = new SoundPlayer();
            }
            player.SoundLocation = "http://www.wavsource.com/snds_2016-10-30_1570758759693582/movies/aladdin/aladdin_fast.wav";
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
