using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jeu.Modele
{
    class Deplacement
    {
        public static Deplacement ALL = new Deplacement("all");
        public static Deplacement TERRESTRE = new Deplacement("terreste");
        public static Deplacement MARITIME = new Deplacement("maritime");

        /* Constructeur */
        private Deplacement(String nom)
        {
            this.nom = nom;
        }

        /* Attributs */
        private String nom;
        public String Nom
        {
            get { return nom; }
        }

        /* Méthodes */
        public Boolean can_Move(Terrain t)
        {
            return true;
        }
    }
}
