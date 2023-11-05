namespace VQTFarm
{
    public class ThreadInfoClass
    {
        public ThreadInfoClass() { }
        public ThreadInfoClass(string scriptPath, string ip, string team_name)
        {
            this.scriptPath = scriptPath;
            this.ip = ip;
            this.team_name = team_name;
        }
        public string scriptPath;
        public string ip;
        public string team_name;
        public string[] flags;
    }
}
