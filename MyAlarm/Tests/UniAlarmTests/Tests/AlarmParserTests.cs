using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
