using Newtonsoft.Json;

namespace FluffyServ.Server
{
    public class ServerMessage
    {
        private int id;
        private string command;
        private object datas;

        public int Id { get => id; set => id = value; }
        public object Datas { get => datas; set => datas = value; }
        public string Command { get => command; set => command = value; }

        public ServerMessage(int id, string command, object datas)
        {
            this.id = id;
            this.command = command;
            this.datas = datas;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
