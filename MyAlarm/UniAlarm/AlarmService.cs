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
        public void TryPlaySound()
        {
            var player = new SoundPlayer("http://www.wavsource.com/snds_2016-10-30_1570758759693582/movies/aladdin/aladdin_fast.wav");
            player.Play();
        }
    }
}
