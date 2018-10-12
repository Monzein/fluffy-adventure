using System;

namespace FluffyServ.Model
{
    class GridCreator
    {

        public static Grid Creator1(int height, int width, int seed)
        {
            //vérifications
            if (height < 20)
                height = 20;
            if (width < 20)
                width = 20;

            // Variable
            int min = Math.Min(height, width);
            int radiusOcean = 3,
                radiusEarth = min/4,
                radiusForest = (int)(radiusEarth * 0.5),
                radiusMountain = (int)(radiusEarth * 0.2);
            int ilots = 25;
            int x_j = 0, y_j = 0;

            // Initialisation du random
            Random r;
            if (seed == 0)
                r = new Random();
            else
                r = new Random(seed);

            // Tableau pour travailler sur les terrains
            int[,] tab = new int[height, width];
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    tab[i, j] = 0;
                }
            }

            for (int il = 0; il < ilots; il++) {
                // Point de départ de la terre
                int x = (int)(r.NextDouble() * (width - 2 * radiusEarth) + radiusEarth);
                int y = (int)(r.NextDouble() * (height - 2 * radiusEarth) + radiusEarth);

                // Création de la terre
                for (int i = y - radiusEarth; i < y + radiusEarth; i++)
                {
                    for (int j = x - radiusEarth; j < x + radiusEarth; j++)
                    {
                        if ((j - x) * (j - x) + (i - y) * (i - y) < radiusMountain * radiusMountain)
                        {
                            tab[i, j] = 3;
                        }
                        else if (tab[i,j]<=1 && (j - x) * (j - x) + (i - y) * (i - y) < radiusForest * radiusForest)
                        {
                            tab[i, j] = 2;
                        }
                        else if (tab[i,j]==0 && (j - x) * (j - x) + (i - y) * (i - y) < radiusEarth * radiusEarth) {
                            tab[i, j] = 1;
                        }
                    }
                }
            }

            // Création des plages
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (tab[i, j] == 1)
                    {
                        if (tab[i, j - 1] == 0 || tab[i, j + 1] == 0 || tab[i - 1, j] == 0 || tab[i + 1, j] == 0 ||
                            tab[i - 1, j - 1] == 0 || tab[i - 1, j + 1] == 0 || tab[i + 1, j - 1] == 0 || tab[i + 1, j + 1] == 0)
                        {
                            tab[i, j] = 4;
                        }
                    }
                }
            }

            // Construction de la grille selon le tableau
            string lol = "";
            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    lol += tab[i, j] + " ";
                }
                lol += "\n";
            }
            Console.WriteLine(lol);
            Grid grid = new Grid(width, height, seed, tab);
            return grid;
        }

    }
}
