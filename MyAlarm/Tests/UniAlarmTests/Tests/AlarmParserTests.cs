using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FluentAssertions;
using System.IO;

namespace UniAlarm.Tests
{
    [TestClass]
    public class AlarmParserTests
    {
        [TestMethod]
        public void ParseTimeSpan()
        {
            var result = TimeSpan.Parse("10:30");
            Assert.AreEqual(10, result.Hours, "Hours");
            Assert.AreEqual(30, result.Minutes, "Minutes");
        }

        [TestMethod]
        public void TryParseTimeSpan()
        {
            TimeSpan result;
            var parseSucceeded = TimeSpan.TryParse("12:45", out result);
            Assert.IsTrue(parseSucceeded);
            Assert.AreEqual(12, result.Hours, "Hours");
            Assert.AreEqual(45, result.Minutes, "Minutes");
        }

        [TestMethod]
        public void CreateAlarmParser()
        {
            var configpath = "some_path_to_config_file";
            var parser = new AlarmParser(configpath);
            Assert.AreEqual(configpath, parser.ConfigFileFullPath, "Config Path");
        }

        [TestMethod]
        public void ParseAlarm()
        {
            var alarmsConfig = @"
10:45 file1

11:30 url1

12:00 file2
22:00 url2
22:30 file3
23:00 url3
";
            var lines = alarmsConfig.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var parser = new AlarmParser("invalid_config_file");
            var result = parser.Parse(lines);
            result.Should().Equal(
                new AlarmConfig[] {
                    new AlarmConfig { Time = TimeSpan.FromHours(10.75), SoundPath = "file1" },
                    new AlarmConfig { Time = TimeSpan.FromHours(11.5), SoundPath = "url1" },
                    new AlarmConfig { Time = TimeSpan.FromHours(12), SoundPath = "file2" },
                    new AlarmConfig { Time = TimeSpan.FromHours(22), SoundPath = "url2" },
                    new AlarmConfig { Time = TimeSpan.FromHours(22.5), SoundPath = "file3" },
                    new AlarmConfig { Time = TimeSpan.FromHours(23), SoundPath = "url3" }
                }
            );
        }

        [TestMethod]
        public void GetNextAlarm()
        {
            var alarmsConfig = @"
10:45 file1
12:00 file2
22:00 file3
22:30 file4
";
            var lines = alarmsConfig.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var parser = new AlarmParser("invalid_config_file");
            var result = parser.GetNextAlarm(TimeSpan.FromHours(11.00), parser.Parse(lines));
            result.ShouldBeEquivalentTo(new AlarmConfig
            {
                Time = TimeSpan.FromHours(12.00),
                SoundPath = "file2",
            });
        }

        [TestMethod]
        public void GetNextAlarmForNextDay()
        {
            var alarmsConfig = @"
10:45 file1
12:00 file2
22:00 file3
22:30 file4
";
            var lines = alarmsConfig.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var parser = new AlarmParser("invalid_config_file");
            var result = parser.GetNextAlarm(TimeSpan.FromHours(23.00), parser.Parse(lines));
            result.ShouldBeEquivalentTo(new AlarmConfig
            {
                Time = TimeSpan.FromHours(11.75), // 23.00 - 24.00 + 10:45
                SoundPath = "file1",
            });
        }
    }
}
