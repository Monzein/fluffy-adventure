using System;
using System.Collections.Generic;

namespace FluffyServ.Model
{
    class Grid
    {
        /* Constantes */
        private const int DEFAULT_WIDTH = 25, DEFAULT_HEIGHT = 25, MAX = 200;

        /* Attributs */
        private Cell[,] cells;
        public Cell[,] Cells
        {
            get { return cells; }
        }
        private int seed;
        public int Seed
        {
            get { return seed; }
        }

        private int width, height;
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }
        private List<Character> characters;
        public List<Character> Characters
        {
            get { return characters; }
        }

        /* Constructeurs */
        public Grid(int width, int height, int seed, int[,] tab)
        {
            if (width > 0 && width < MAX)
                this.width = width;
            else
                this.width = DEFAULT_WIDTH;
            if (height > 0 && height < MAX)
                this.height = height;
            else
                this.height = DEFAULT_HEIGHT;
            this.seed = seed;
            this.cells = new Cell[height, width];
            this.characters = new List<Character>();
            Init(tab);
        }

        /* Initialisation */
        private void Init(int[,] tab)
        {
            Random r;
            if (seed == 0)
                r = new Random();
            else
                r = new Random(seed);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cells[i, j] = new Cell(Terrain.GetTerrain(tab[i, j]),r);
                }
            }
        }

        /* Accesseurs */
        public Terrain GetTerrain(int x, int y)
        {
            return cells[y, x].Type;
        }

        /* Ajout de personnage */
        public Player AddJoueur(int idPlayer)
        {
            Random r = new Random(Seed);
            int xJ = 0, yJ = 0;
            while (cells[xJ, yJ].Type == Terrain.OCEAN)
            {
                xJ = (int)(5 + r.NextDouble() * (width - 5));
                yJ = (int)(5 + r.NextDouble() * (height - 5));
            }
            Player j = new Player(xJ, yJ, idPlayer);
            this.characters.Add(j);
            return j;
        }

        public Character GetCharacter(int id)
        {
            foreach(Character p in characters)
            {
                if(p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        public void MoveCharacter(int id, Direction direction)
        {
            Character p = GetCharacter(id);
            if (p != null)
            {
                p.Move(direction, this);
            }
        }

        public void RemoveCharacter(Character p)
        {
            characters.Remove(p);
        }

        public Cell[,] GetViewPlayer(Player player)
        {
            Cell[,] result = new Cell[3, 3];
            int y = 0;
            for (int i= player.Y-1; i<= player.Y+1; i++)
            {
                int x = 0;
                for (int j = player.X - 1; j <= player.X + 1; j++)
                {
                    if(i<0 || i>=height || j<0 || j >= width)
                    {
                        result[y, x] = Cell.voidCell;
                    }
                    else
                    {
                        result[y, x] = cells[i, j];
                    }
                    x++;
                }
                y++;
            }
            return result;
        }

        public string GetViewPlayerString(Player player)
        {
            Cell[,] tab = GetViewPlayer(player);
            return GetViewString(tab);
        }

        public string GetViewMapString()
        {
            return GetViewString(cells);
        }

        private string GetViewString(Cell[,] tab)
        {
            string result = "[";
            foreach (Cell cel in tab)
            {
                result += cel.ToString() + ",";
            }
            result = result.Remove(result.Length - 1);
            result += "]";
            return result;
        }
    }
}
