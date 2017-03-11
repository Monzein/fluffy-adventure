using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jeu.Modele
{
    abstract class Personnage
    {
        private static int id_cpt = 0;

        /* Attributs */
        private int id;
        private string nom;
        public string Nom
        {
            get { return nom; }
        }
        private Deplacement deplacement;
        private int x;
        public int X
        {
            get { return x; }
        }
        private int y;
        public int Y
        {
            get { return y; }
        }

        /* Constructeurs */
        public Personnage(String nom,Deplacement deplacement)
        {
            this.id = next_id();
            this.nom = nom;
            this.deplacement = deplacement;
            this.x = 0;
            this.y = 0;
        }

        public Personnage(String nom, Deplacement deplacement, int x, int y)
        {
            this.id = next_id();
            this.nom = nom;
            this.deplacement = deplacement;
            this.x = x;
            this.y = y;
        }

        /* Création */
        private int next_id()
        {
            id_cpt++;
            return id_cpt;
        }

        /* Opérations sur un personnage */
        public void deplacer(Direction dir, Grille g)
        {
            switch (dir)
            {
                case Direction.NORD:
                    if(this.y>0)
                        this.y =- 1;
                    break;
                case Direction.SUD:
                    if (this.y < g.Hauteur-1)
                        this.y =+ 1;
                    break;
                case Direction.OUEST:
                    if (this.x > 0)
                        this.x =- 1;
                    break;
                case Direction.EST:
                    if (this.x < g.Largeur - 1)
                        this.x =+ 1;
                    break;
            }
        }
    }

}
