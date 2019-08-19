using System;

namespace FluffyServ.Model
{
    /// <summary>
    /// This class use different algorithm to create and populate Grid.
    /// </summary>
    public class GridCreator
    {
        /// <summary>
        /// Create and populate a Grid with the given dimensions and a seed for the random.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static Grid Creator1(int height, int width, int seed)
        {
            //vérifications
            if (height < 20)
                height = 20;
            if (width < 20)
                width = 20;

            // Variable
            int min = Math.Min(height, width);
            int minIlot = 12, maxIlot = 25;
            int radiusDesolation = min / 10,
                radiusEarth = min / 6,
                radiusForest = (int)(radiusEarth * 0.60),
                radiusMountain = (int)(radiusEarth * 0.30);

            // Initialisation du random
            Random r;
            if (seed == 0)
                r = new Random();
            else
                r = new Random(seed);


            int normalIlot = r.Next(minIlot,maxIlot), desolationIlot = normalIlot/2+1;

            // Tableau pour travailler sur les terrains
            int[,] tab = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tab[i, j] = 0;
                }
            }
            int x = 0, y = 0,
                pX = r.Next(radiusDesolation + 1, width - radiusDesolation - 2),
                pY = r.Next(radiusDesolation + 1, height - radiusDesolation - 2);
            int mult = 3 * radiusDesolation / 2;
            for (int il = 0; il < desolationIlot; il++)
            {
                // Point de départ de la désolation
                x = r.Next(pX - mult, pX + mult);
                y = r.Next(pY - mult, pY + mult);

                while (x - radiusEarth < 1 || x + radiusEarth > width - 2 ||
                    y - radiusEarth < 1 || y + radiusEarth > height - 2)
                {
                    x = r.Next(pX - mult, pX + mult);
                    y = r.Next(pY - mult, pY + mult);
                }
                pX = x;
                pY = y;

                // Création de la désolation
                for (int i = y - radiusDesolation; i < y + radiusDesolation; i++)
                {
                    for (int j = x - radiusDesolation; j < x + radiusDesolation; j++)
                    {
                        if (tab[i, j] == 0 && (j - x) * (j - x) + (i - y) * (i - y) < radiusDesolation * radiusDesolation)
                        {
                            tab[i, j] = 1;
                        }
                    }
                }
            }
            mult = 3 * radiusEarth / 2;
            for (int il = 0; il < normalIlot; il++)
            {
                // Point de départ de la terre
                x = r.Next(pX - mult, pX + mult);
                y = r.Next(pY - mult, pY + mult);

                while (x - radiusEarth < 1 || x + radiusEarth > width - 2 ||
                    y - radiusEarth < 1 || y + radiusEarth > height - 2 || tab[y, x]==4)
                {
                    x = r.Next(pX - mult, pX + mult);
                    y = r.Next(pY - mult, pY + mult);
                }
                pX = x;
                pY = y;

                // Création de la terre
                for (int i = y - radiusEarth; i < y + radiusEarth; i++)
                {
                    for (int j = x - radiusEarth; j < x + radiusEarth; j++)
                    {
                        if ((j - x) * (j - x) + (i - y) * (i - y) < radiusMountain * radiusMountain)
                        {
                            tab[i, j] = 4;
                        }
                        else if (tab[i, j] <= 2 && (j - x) * (j - x) + (i - y) * (i - y) < radiusForest * radiusForest)
                        {
                            tab[i, j] = 3;
                        }
                        else if (tab[i, j] <= 1 && (j - x) * (j - x) + (i - y) * (i - y) < radiusEarth * radiusEarth)
                        {
                            tab[i, j] = 2;
                        }
                    }
                }
            }

            // Création des plages
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (tab[i, j] == 2 || tab[i, j] == 1)
                    {
                        if (tab[i, j - 1] == 0 || tab[i, j + 1] == 0 || tab[i - 1, j] == 0 || tab[i + 1, j] == 0 ||
                            tab[i - 1, j - 1] == 0 || tab[i - 1, j + 1] == 0 || tab[i + 1, j - 1] == 0 || tab[i + 1, j + 1] == 0)
                        {
                            tab[i, j] = 5;
                        }
                    }
                }
            }

            string lol = "Map:\n";
            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    lol += tab[i, j] + " ";
                }
                lol += "\n";
            }
            Console.WriteLine(lol);

            // Construction de la grille selon le tableau
            Grid grid = new Grid(width, height, seed, tab);
            
            Populate1(grid,r);
            return grid;
        }

        /// <summary>
        /// Populate the given Grid. Populate means add the non playable character
        /// and random object.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rand"></param>
        private static void Populate1(Grid grid, Random rand)
        {
            for (int i = 0; i < grid.Height; i++)
            {
                for (int j = 0; j < grid.Width; j++)
                {
                    if (grid.GetTerrain(j, i) != Terrain.OCEAN)
                    {
                        if (grid.GetTerrain(j, i) == Terrain.PLAIN)
                        {
                            if (rand.NextDouble() > 0.3)
                            {
                                grid.AddCharacter(new NonPlayableCharacter(CharacterTemplate.CHICKEN, j, i, grid));
                            }
                        }
                        if (grid.GetTerrain(j, i) == Terrain.FOREST)
                        {
                            if (rand.NextDouble() > 0.2)
                            {
                                grid.AddCharacter(new NonPlayableCharacter(CharacterTemplate.YOUNG_BOAR, j, i, grid));
                            }
                        }
                    }
                }
            }
        }
    }
}
