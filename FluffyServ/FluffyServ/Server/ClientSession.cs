using Fleck;
using FluffyServ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Server
{
    class ClientSession
    {
        private static int idCount = 0;

        private int id;
        private Player joueur;

        private static int IdCount { get => idCount++; }
        public int Id { get => id; }
        internal Player Joueur { get => joueur; set => joueur = value; }

        public ClientSession(Grid grille)
        {
            id = IdCount;
            Joueur = grille.AddJoueur(id);
        }
    }
}
