using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UniAlarm
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new SoundPlayer("http://www.wavsource.com/snds_2016-10-30_1570758759693582/movies/aladdin/aladdin_fast.wav");
            player.Play();

            Console.ReadLine();

            var play2 = new SoundPlayer(@"D:\src\git\my-alarm\assets\aladdin_who_disturbs.wav");
            play2.Play();

            Console.ReadLine();
        }
    }
}
