﻿using Fleck;
using FluffyServ.Model;
using FluffyServ.Model.Entities.GameItems;
using FluffyServ.Model.Mechanisms.Craft;

namespace FluffyServ.Server
{
    public abstract class MessageSender
    {
        internal static void InfoMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = session.SessionPlayer.ToString();
            ServerMessage messageResult = new ServerMessage(session.Id, "info", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void BoardMessage(ClientSession session, IWebSocketConnection socket, Grid grid)
        {
            string datas = grid.GetViewPlayerString(session.SessionPlayer);
            ServerMessage messageResult = new ServerMessage(session.Id, "board", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void InventoryMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = session.SessionPlayer.Inventory.ToString();
            ServerMessage messageResult = new ServerMessage(session.Id, "inventory", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void EquipementMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = session.SessionPlayer.Gear.ToString();
            ServerMessage messageResult = new ServerMessage(session.Id, "equipement", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void MapMessage(ClientSession session, IWebSocketConnection socket, Grid grid)
        {
            //Cheat version
            //string map = grid.GetViewMapString();
            //Regular version
            string map = grid.GetViewPlayerMapString(session.SessionPlayer);
            string datas = "{\"width\":" + grid.Width + ",\"height\":" + grid.Height + ",\"map\":" + map + "}";
            ServerMessage messageResult = new ServerMessage(session.Id, "map", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void ReceipeMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = ItemCrafting.Instance.ToString();
            ServerMessage messageResult = new ServerMessage(session.Id, "receipes", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void RefreshMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = null;
            ServerMessage messageResult = new ServerMessage(session.Id, "refresh", datas);
            socket.Send(messageResult.ToString());
        }

        internal static void BattleMessage(ClientSession session, IWebSocketConnection socket)
        {
            if (session.DoMessageBattle())
            {
                ServerMessage messageResult;
                if (session.SessionPlayer.Battle != null)
                {
                    messageResult = new ServerMessage(session.Id, "battle", session.SessionPlayer.Battle.ToString());
                    //socket.Send(messageResult.ToString());
                }
                else
                {
                    messageResult = new ServerMessage(session.Id, "end_battle", null);
                }
                socket.Send(messageResult.ToString());
            }
        }

        internal static void LooseMessage(ClientSession session, IWebSocketConnection socket)
        {
            string datas = null;
            ServerMessage messageResult = new ServerMessage(session.Id, "loose", datas);
            socket.Send(messageResult.ToString());
        }
    }
}
