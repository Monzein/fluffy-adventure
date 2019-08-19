using FluffyServ.Model;
using FluffyServ.Model.Mechanisms;

namespace FluffyServ.Server
{
    internal class GameAction
    {
        private GameActionType type;
        private ClientSession session;
        private object param;

        public GameAction(GameActionType type, ClientSession session, object param)
        {
            this.type = type;
            this.session = session;
            this.param = param;
        }

        public void DoAction(Grid grid)
        {
            switch (type)
            {
                case GameActionType.MOVE:
                    grid.MoveCharacter(session.SessionPlayer.Id, (Direction)param);
                    break;
                case GameActionType.EXTRACT:
                    grid.Extract(session.SessionPlayer, (string)param);
                    break;
                case GameActionType.START_FIGHT:
                    grid.StartBattle(session.SessionPlayer, (int)param);
                    break;
                case GameActionType.ACTION_FIGHT:
                    session.SessionPlayer.SetNextBattleAction((string)param);
                    break;
                case GameActionType.PICK:
                    grid.PickUpItem(session.SessionPlayer, (string)param);
                    break;
            }
        }
    }

    internal enum GameActionType
    {
        MOVE,
        EXTRACT,
        START_FIGHT,
        ACTION_FIGHT,
        PICK
    }
}
