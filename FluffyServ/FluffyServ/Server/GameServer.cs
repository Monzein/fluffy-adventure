using Fleck;
using FluffyServ.Model;
using FluffyServ.Model.Mechanisms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Timers;

namespace FluffyServ.Server
{
    public class GameServer : WebSocketServer
    {
        private const string LOCATION = "ws://0.0.0.0:7000";
        private const int TICK_TIME = 200;

        private static Grid grid;
        private static Dictionary<IWebSocketConnection, ClientSession> clients = new Dictionary<IWebSocketConnection, ClientSession>();
        private static List<GameAction> gameActions;

        private Timer aTimer;

        public GameServer(string location = LOCATION) : base(location)
        {
            grid = GridCreator.Creator1(50, 50, 0);
            Console.WriteLine("Map Generated!");
            gameActions = new List<GameAction>();
        }

        private void SetTimer()
        {
            // Create a timer with a five second interval.
            aTimer = new System.Timers.Timer(TICK_TIME);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DoServerTick();
        }

        public void StopServ()
        {
            aTimer.Stop();
            aTimer.Dispose();
        }

        public void StartServ()
        {
            this.Start(socket =>
            {
                socket.OnOpen = () => OnOpen(socket);
                socket.OnClose = () => OnClose(socket);
                socket.OnMessage = message => OnMessage(socket, message);
            });

            SetTimer();

        }

        private static void OnOpen(IWebSocketConnection socket)
        {
            ClientSession client = new ClientSession(grid);
            clients.Add(socket, client);
            Console.WriteLine("Open client " + client.Id);
            ServerMessage result = new ServerMessage(client.Id, "connected", null);
            socket.Send(result.ToString());

            MessageSender.BoardMessage(client, socket, grid);
            MessageSender.InfoMessage(client, socket);
            MessageSender.ReceipeMessage(client, socket);
        }

        private static void OnClose(IWebSocketConnection socket)
        {
            Console.WriteLine("Close!");
            if (clients.TryGetValue(socket, out ClientSession session))
            {
                grid.RemoveCharacter(session.SessionPlayer);
            }
            clients.Remove(socket);
        }

        private static void OnMessage(IWebSocketConnection socket, string messageStr)
        {
            Console.WriteLine("Message received: " + messageStr);
            ServerMessage messageIn = JsonConvert.DeserializeObject<ServerMessage>(messageStr);
            ClientSession session = null;

            if (!clients.TryGetValue(socket, out session))
            {
                Console.WriteLine("Error! No session for the message: " + messageStr);
                return;
            }
            if (session.IsLoose)
            {
                return;
            }
            switch (messageIn.Command)
            {
                case "move":
                    if (session.DoAction())
                    {
                        Enum.TryParse(messageIn.Datas.ToString(), out Direction d);
                        gameActions.Add(new GameAction(GameActionType.MOVE, session, d));
                    }
                    break;
                case "extract":
                    if (session.DoAction())
                    {
                        gameActions.Add(new GameAction(GameActionType.EXTRACT, session, messageIn.Datas.ToString()));
                    }
                    break;
                case "inventory":
                    MessageSender.InventoryMessage(session, socket);
                    break;
                case "map":
                    MessageSender.MapMessage(session, socket, grid);
                    break;
                case "use":
                    session.SessionPlayer.UseItem(messageIn.Datas.ToString());
                    MessageSender.InventoryMessage(session, socket);
                    MessageSender.InfoMessage(session, socket);
                    break;
                case "craft":
                    session.SessionPlayer.Craft(messageIn.Datas.ToString(), grid);
                    MessageSender.InventoryMessage(session, socket);
                    break;
                case "receipes":
                    MessageSender.ReceipeMessage(session, socket);
                    break;
                case "start_battle":
                    if (session.DoAction())
                    {
                        if (int.TryParse(messageIn.Datas.ToString(), out int idDefender))
                        {
                            gameActions.Add(new GameAction(GameActionType.START_FIGHT, session, idDefender));
                        }
                    }
                    break;
                case "action_battle":
                    if (session.DoActionBattle())
                    {
                        gameActions.Add(new GameAction(GameActionType.ACTION_FIGHT, session, messageIn.Datas.ToString()));
                    }
                    break;
                default:
                    break;
            }
        }

        private static void DoServerTick()
        {
            foreach(GameAction action in gameActions)
            {
                action.DoAction(grid);
            }
            gameActions.Clear();
            grid.DoRounds();
            List<IWebSocketConnection> toClose = new List<IWebSocketConnection>();
            foreach (KeyValuePair<IWebSocketConnection, ClientSession> client in clients)
            {
                RefreshClient(client.Key, client.Value, toClose);
            }
            foreach(IWebSocketConnection sock in toClose)
            {
                sock.Close();
            }
        }

        private static void RefreshClient(IWebSocketConnection socket, ClientSession session,
            List<IWebSocketConnection> toClose)
        {
            if (!session.IsLoose)
            {
                if (session.ResetAction())
                {
                    MessageSender.RefreshMessage(session, socket);
                    MessageSender.BoardMessage(session, socket, grid);
                    MessageSender.InfoMessage(session, socket);
                    MessageSender.BattleMessage(session, socket);
                }
                else
                {
                    MessageSender.BattleMessage(session, socket);
                    MessageSender.LooseMessage(session, socket);
                    toClose.Add(socket);
                }
            }
        }
    }
}
