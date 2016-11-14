using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlarm
{
    [Equals]
    internal class AlarmConfig
    {
        public TimeSpan Time { get; set; }
        public string SoundPath { get; set; }
    }
}
