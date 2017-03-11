using System;

namespace jeu.Modele
{
    class Createur_grille
    {

        public static Grille createur_1(int hauteur, int largeur, int seed)
        {
            //vérifications
            if (hauteur < 20)
                hauteur = 20;
            if (largeur < 20)
                largeur = 20;

            // Variable
            int min = Math.Min(hauteur, largeur);
            int rayon_ocean = 3,
                rayon_terre = min/4,
                rayon_foret = (int)(rayon_terre*0.5),
                rayon_montagne = (int)(rayon_terre * 0.2);
            int ilots = 25;

            // Initialisation du random
            Random r;
            if (seed == 0)
                r = new Random();
            else
                r = new Random(seed);

            // Tableau pour travailler sur les terrains
            int[,] tab = new int[hauteur, largeur];
            for(int i = 0; i < hauteur; i++)
            {
                for(int j = 0; j < largeur; j++)
                {
                    tab[i, j] = 0;
                }
            }

            for (int il = 0; il < ilots; il++) {
                // Point de départ de la terre
                int x = (int)(r.NextDouble() * (largeur - 2 * rayon_terre) + rayon_terre);
                int y = (int)(r.NextDouble() * (hauteur - 2 * rayon_terre) + rayon_terre);
                Console.WriteLine("x " + x + " y " + y);

                // Création de la terre
                for (int i = y - rayon_terre; i < y + rayon_terre; i++)
                {
                    for (int j = x - rayon_terre; j < x + rayon_terre; j++)
                    {
                        if ((j - x) * (j - x) + (i - y) * (i - y) < rayon_montagne * rayon_montagne)
                        {
                            tab[i, j] = 3;
                        }
                        else if (tab[i,j]<=1 && (j - x) * (j - x) + (i - y) * (i - y) < rayon_foret * rayon_foret)
                        {
                            tab[i, j] = 2;
                        }
                        else if (tab[i,j]==0 && (j - x) * (j - x) + (i - y) * (i - y) < rayon_terre * rayon_terre) {
                            tab[i, j] = 1;
                        }
                    }
                }
            }

            // Création des plages
            // Création de la terre
            for (int i = 1; i < hauteur-1; i++)
            {
                for (int j = 1; j < largeur-1; j++)
                {
                    if (tab[i, j] == 1) {
                        if (tab[i, j - 1] == 0 || tab[i, j + 1] == 0 || tab[i - 1, j] == 0 || tab[i + 1, j ] == 0 ||
                            tab[i - 1, j - 1] == 0 || tab[i - 1, j + 1] == 0 || tab[i + 1, j - 1] == 0 || tab[i + 1, j + 1] == 0)
                        {
                            tab[i, j] = 4;
                        }
                    }
                }
            }

                    // Construction de la grille selon le tableau
                    Grille grille = new Modele.Grille(largeur, hauteur, seed, tab);
            return grille;
        }

    }
}
