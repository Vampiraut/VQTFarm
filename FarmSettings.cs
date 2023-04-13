using System.Text.RegularExpressions;

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
