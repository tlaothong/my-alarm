using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniAlarm
{
    internal class AlarmParser
    {
        public readonly string ConfigFileFullPath;

        public AlarmParser(string fullPathToConfigFile)
        {
            this.ConfigFileFullPath = fullPathToConfigFile;
        }

        public IEnumerable<AlarmConfig> Parse()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<AlarmConfig> Parse(string[] alarmsConfigLines)
        {
            var configs = from line in alarmsConfigLines
                          where !string.IsNullOrWhiteSpace(line)
                          select ParseConfigItem(line);
            return configs;
        }

        private AlarmConfig ParseConfigItem(string alarmConfig)
        {
            var splitIndex = alarmConfig.IndexOf(' ');

            if (splitIndex > 0)
            {
                var timeConfig = alarmConfig.Substring(0, splitIndex).Trim();
                var soundFiConfig = alarmConfig.Substring(splitIndex).Trim();

                TimeSpan time;
                if (TimeSpan.TryParse(timeConfig, out time))
                {
                    return new AlarmConfig
                    {
                        Time = time,
                        SoundPath = soundFiConfig,
                    };
                }
            }

            throw new FormatException(string.Format(
                "Alarm Configuration is not property format: `{0}`",
                alarmConfig));
        }
    }
}
