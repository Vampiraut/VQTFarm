using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VQTFarm
{
    public class FlagHistory
    {
        public FlagHistory() { }
        public FlagHistory(string sploit_name, string team_name, string sended_flag, string time, string status, string cheskSystemRespons)
        {
            this.sploit_name = sploit_name;
            this.team_name = team_name;
            this.sended_flag = sended_flag;
            this.time = time;
            this.status = status;
            this.cheskSystemRespons = cheskSystemRespons;
        }

        public int id;
        public string sploit_name;
        public string team_name;
        public string sended_flag;
        public string time;
        public string status;
        public string cheskSystemRespons;
    }
}
