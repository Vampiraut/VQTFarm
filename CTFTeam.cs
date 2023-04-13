using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VQTFarm
{
    public class CTFTeam
    {
        public CTFTeam() { }
        public CTFTeam(string teamPlace, string teamName, string teamId, string teamScore)
        {
            this.teamPlace = teamPlace;
            this.teamName = teamName;
            this.teamIP = teamId;
            this.teamScore = teamScore;
        }

        public int id;
        public string teamPlace;
        public string teamName;
        public string teamIP;
        public string teamScore;
    }
}
