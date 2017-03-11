using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jeu.Modele
{
    class Joueur: Personnage
    {
        

        public Joueur(int x, int y) : base("joueur", Deplacement.TERRESTRE, x, y)
        {

        }
    }
}
