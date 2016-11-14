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
            var configPath = this.ConfigFileFullPath;

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Alarm Configuration file's not exists", configPath);
            }
            var lines = File.ReadLines(configPath);
            return Parse(lines);
        }

        internal IEnumerable<AlarmConfig> Parse(IEnumerable<string> alarmsConfigLines)
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

        public AlarmConfig GetNextAlarm(TimeSpan fromTime)
        {
            return GetNextAlarm(fromTime, Parse());
        }

        internal AlarmConfig GetNextAlarm(TimeSpan fromTime, IEnumerable<AlarmConfig> alarms)
        {
            var nextAlarms = from alarm in alarms
                             orderby alarm.Time
                             where alarm.Time > fromTime
                             select alarm;

            var nextAlarmToday = nextAlarms.FirstOrDefault();
            if (nextAlarmToday != null)
            {
                return nextAlarmToday;
            }

            var firstAlarmOfTheDay = (from alarm in alarms
                                      orderby alarm.Time
                                      select alarm).FirstOrDefault();
            firstAlarmOfTheDay.Time = firstAlarmOfTheDay.Time.Add(TimeSpan.FromHours(24) - fromTime);
            return firstAlarmOfTheDay;
        }
    }
}
