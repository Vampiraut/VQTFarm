using System.Text.RegularExpressions;

namespace VQTFarm
{
    [Serializable]
    public class FarmSettings
    {
        public FarmSettings() { }
        public FarmSettings(Regex flagFormat, string teamToken, string teamOwnerIP, int roundTime, int flagLifeTime, string scoreboardURL, string flaSubmitterURL)
        {
            this.flagFormat = flagFormat;
            this.teamToken = teamToken;
            this.teamOwnerIP = teamOwnerIP;
            this.roundTime = roundTime;
            this.flagLifeTime = flagLifeTime;
            this.scoreboardURL = scoreboardURL;
            this.flaSubmitterURL = flaSubmitterURL;
        }
        public Regex flagFormat { get; set; }
        public string teamToken { get; set; }
        public string teamOwnerIP { get; set; }
        public int roundTime { get; set; }
        public int flagLifeTime { get; set; }
        public string scoreboardURL { get; set; }
        public string flaSubmitterURL { get; set; }
    }
}
