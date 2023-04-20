using System.Text.RegularExpressions;

namespace VQTFarm
{
    [Serializable]
    public class FarmSettings
    {
        public FarmSettings() { }
        public FarmSettings(string flagFormat, string teamToken, string teamOwnerIP, int roundTime, int flagLifeTime, string scoreBoardURL, string flagSubmitterURL, string pythonGetScriptPath, string pythonFlagSendScriptPath)
        {
            this.flagFormat = flagFormat;
            this.teamToken = teamToken;
            this.teamOwnerIP = teamOwnerIP;
            this.roundTime = roundTime;
            this.flagLifeTime = flagLifeTime;
            this.scoreBoardURL = scoreBoardURL;
            this.flagSubmitterURL = flagSubmitterURL;
            this.pythonGetScriptPath = pythonGetScriptPath;
            this.pythonFlagSendScriptPath = pythonFlagSendScriptPath;
        }
        public string flagFormat { get; set; }
        public string teamToken { get; set; }
        public string teamOwnerIP { get; set; }
        public int roundTime { get; set; }
        public int flagLifeTime { get; set; }
        public string scoreBoardURL { get; set; }
        public string flagSubmitterURL { get; set; }
        public string pythonGetScriptPath {  get; set; }
        public string pythonFlagSendScriptPath { get; set; }
    }
}
