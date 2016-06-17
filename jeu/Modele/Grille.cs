using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jeu.Modele
{
    class Grille
    {
        /* Constantes */
        private static int LARGEUR = 25, HAUTEUR = 25, MAX = 200;

        /* Attributs */
        private Cellule[,] cellules;
        public Cellule[,] Cellules
        {
            get { return cellules; }
        }
        private int seed;
        public int Seed
        {
            get { return seed; }
        }

        private int largeur, hauteur;
        public int Hauteur
        {
            get { return hauteur; }
        }
        public int Largeur
        {
            get { return largeur; }
        }

        /* Constructeurs */
        public Grille(int largeur, int hauteur, int seed)
        {
            if (largeur > 0 && largeur < MAX)
                this.largeur = largeur;
            else
                this.largeur = LARGEUR;
            if (hauteur > 0 && hauteur < MAX)
                this.hauteur = hauteur;
            else
                this.hauteur = HAUTEUR;
            this.seed = seed;
            this.cellules=new Cellule[largeur,hauteur];
            Init();
        }

        /* Initialisation */
        private void Init()
        {
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    cellules[i, j] = new Cellule(Terrain.OCEAN);
                }
            }
        }
    
        /* Accesseurs */
        public String GetTerrainName(int x, int y)
        {
            return cellules[x, y].Type.Nom;
        }
    }
}
