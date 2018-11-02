using Fleck;
using FluffyServ.Model;

namespace FluffyServ.Server
{
    public class ClientSession
    {
        private static int idCount = 0;

        private int id;
        private Player sessionPlayer;
        private bool action = true;
        private bool isBattle = false;
        private bool isLoose = false;

        private static int IdCount { get => idCount++; }
        public int Id { get => id; }
        internal Player SessionPlayer { get => sessionPlayer; set => sessionPlayer = value; }
        public bool IsLoose { get => isLoose; }

        public ClientSession(Grid grille)
        {
            id = IdCount;
            SessionPlayer = grille.AddJoueur(id);
            action = true;
        }

        internal bool DoAction()
        {
            if (sessionPlayer.IsInBattle())
            {
                return false;
            }
            if (action)
            {
                action = false;
                return true;
            }
            return false;
        }

        internal bool DoActionBattle()
        {
            if (!sessionPlayer.IsInBattle())
            {
                return false;
            }
            if (action)
            {
                action = false;
                return true;
            }
            return false;
        }

        internal bool DoMessageBattle()
        {
            if (!sessionPlayer.IsInBattle())
            {
                if (isBattle)
                {
                    isBattle = false;
                    return true;
                }
                return false;
            }
            else
            {
                isBattle = true;
                return true;
            }
        }

        internal bool ResetAction()
        {
            action = true;
            if (sessionPlayer.Health == 0)
            {
                isLoose = true;
                return false;
            }
            return true;
        }
    }
}
