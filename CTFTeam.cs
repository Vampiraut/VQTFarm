namespace VQTFarm
{
    public class CTFTeam
    {
        public CTFTeam() { }
        public CTFTeam(string teamPlace, string teamName, string teamIP, string teamScore)
        {
            this.teamPlace = teamPlace;
            this.teamName = teamName;
            this.teamIP = teamIP;
            this.teamScore = teamScore;
        }

        public int id;
        public string teamPlace;
        public string teamName;
        public string teamIP;
        public string teamScore;
    }
}
