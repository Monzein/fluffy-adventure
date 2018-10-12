using Fleck;
using FluffyServ.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Server
{
    class GameServer : WebSocketServer
    {
        private const string LOCATION = "ws://0.0.0.0:7000";

        private static Grid grid;
        private static Dictionary<IWebSocketConnection,ClientSession> clients = new Dictionary<IWebSocketConnection, ClientSession>();

        public GameServer(string location = LOCATION) : base(location)
        {
            grid = GridCreator.Creator1(40, 40, 0);
        }

        public void StartServ()
        {
            this.Start(socket =>
            {
                socket.OnOpen = () => OnOpen(socket);
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });
        }

        private static void OnOpen(IWebSocketConnection socket)
        {
            ClientSession client = new ClientSession(grid);
            clients.Add(socket,client);
            Console.WriteLine("Open client " + client.Id);
            ServerMessage result = new ServerMessage(client.Id, "connected", null);
            socket.Send(result.ToString());

            //result = new ServerMessage(client.Id, "map", grille.GetViewMapString());
            //socket.Send(result.ToString());
        }

        private static void OnClose(IWebSocketConnection socket)
        {
            Console.WriteLine("Close!");
            ClientSession session = null;
            if (clients.TryGetValue(socket, out session))
            {
                grid.RemoveCharacter(session.Joueur);
            }
            clients.Remove(socket);
        }

        private static void OnMessage(IWebSocketConnection socket, string messageStr)
        {
            Console.WriteLine("Message received: " + messageStr);
            ServerMessage messageIn = JsonConvert.DeserializeObject<ServerMessage>(messageStr);
            ServerMessage messageResult = null;
            ClientSession session = null;
            string datas = "";

            if(!clients.TryGetValue(socket, out session))
            {
                Console.WriteLine("Error! No session for the message: " + messageStr);
                return;
            }
            switch (messageIn.Command)
            {
                case "start":
                    datas = grid.GetViewPlayerString(session.Joueur);
                    messageResult = new ServerMessage(session.Id, "board", datas);
                    socket.Send(messageResult.ToString());
                    datas = session.Joueur.Energy + "/" + Player.MAX_ENERGY;
                    messageResult = new ServerMessage(session.Id, "energy", datas);
                    socket.Send(messageResult.ToString());
                    break;
                case "move":
                    Enum.TryParse(messageIn.Datas.ToString(), out Direction d);
                    grid.MoveCharacter(session.Joueur.Id, d);
                    datas = grid.GetViewPlayerString(session.Joueur);
                    messageResult = new ServerMessage(session.Id, "board", datas);
                    socket.Send(messageResult.ToString());
                    datas = session.Joueur.Energy + "/" + Player.MAX_ENERGY;
                    messageResult = new ServerMessage(session.Id, "energy", datas);
                    socket.Send(messageResult.ToString());
                    break;
                default:
                    break;
            }
        }
    }
}
