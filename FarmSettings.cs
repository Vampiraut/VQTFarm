using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VQTFarm
{
    [Serializable]
    public class FarmSettings
    {
        public FarmSettings() { }
        public FarmSettings(Regex flagFormat, string teamToken, string teamOwnerIP, int roundTime, string adminServerIP)
        {
            this.flagFormat = flagFormat;
            this.teamToken = teamToken;
            this.teamOwnerIP = teamOwnerIP;
            this.roundTime = roundTime;
            this.adminServerIP = adminServerIP;
        }
        public Regex flagFormat { get; set; }
        public string teamToken { get; set; }
        public string teamOwnerIP { get; set; }
        public int roundTime { get; set; }
        public string adminServerIP { get; set; }
    }
}
