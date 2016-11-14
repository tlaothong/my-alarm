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

        internal IEnumerable<AlarmConfig> Parse(string alarmsConfigFileContent)
        {
            throw new NotImplementedException();
        }
    }
}
