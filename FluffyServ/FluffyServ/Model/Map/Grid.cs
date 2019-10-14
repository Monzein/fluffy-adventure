using FluffyServ.Model.Entities.Characters;
using FluffyServ.Model.Entities.GameItems;
using FluffyServ.Model.Entities.GameItems.Equipables;
using FluffyServ.Model.Mechanisms;
using FluffyServ.Model.Mechanisms.Battle;
using System;
using System.Collections.Generic;

namespace FluffyServ.Model
{
    /// <summary>
    /// The grid is the root to all the game mechanics.
    /// </summary>
    public class Grid
    {
        /* Constants */
        private const int DEFAULT_WIDTH = 25, DEFAULT_HEIGHT = 25, MAX = 200;

        /* Attributes */
        private Cell[,] cells;
        /// <summary>
        /// The 2D cells array forming the whole board.
        /// </summary>
        public Cell[,] Cells
        {
            get { return cells; }
        }
        private int seed;
        /// <summary>
        /// The seed used for the random.
        /// </summary>
        public int Seed
        {
            get { return seed; }
        }

        private int width, height;
        /// <summary>
        /// The height of the grid.
        /// </summary>
        public int Height
        {
            get { return height; }
        }
        /// <summary>
        /// The width of the grid.
        /// </summary>
        public int Width
        {
            get { return width; }
        }
        private List<Character> characters;
        /// <summary>
        /// All the living characters in the game.
        /// </summary>
        public List<Character> Characters
        {
            get { return characters; }
        }

        private Battles battles;

        /// <summary>
        /// The random used to generate the grid.
        /// </summary>
        private Random rand = new Random();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        /// <param name="tab"></param>
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
            this.battles = new Battles(this);
            Init(tab);
        }

        /// <summary>
        /// Init the grid using an array containing all the terrain type.
        /// </summary>
        /// <param name="tab"></param>
        private void Init(int[,] tab)
        {
            if (seed == 0)
                rand = new Random();
            else
                rand = new Random(seed);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cells[i, j] = new Cell(Terrain.GetTerrain(tab[i, j]), rand);
                }
            }
        }

        /// <summary>
        /// Get the cell at the (x,y) position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            return cells[y, x];
        }

        /// <summary>
        /// Get the terrain at the given position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Terrain GetTerrain(int x, int y)
        {
            return cells[y, x].Type;
        }

        /// <summary>
        /// Add a player to the game.
        /// </summary>
        /// <param name="idPlayer"></param>
        /// <returns></returns>
        public Player AddJoueur(int idPlayer)
        {
            int xJ = 0, yJ = 0;
            while (cells[yJ, xJ].Type == Terrain.OCEAN)
            {
                xJ = (int)(5 + rand.NextDouble() * (width - 5));
                yJ = (int)(5 + rand.NextDouble() * (height - 5));
            }
            Player j = new Player(this, xJ, yJ, idPlayer);
            this.characters.Add(j);
            return j;
        }

        /// <summary>
        /// Add a character to the grid.
        /// </summary>
        /// <param name="c"></param>
        internal void AddCharacter(Character c)
        {
            characters.Add(c);
        }

        /// <summary>
        /// Get the character with the gevin id.
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public Character GetCharacter(int characterId)
        {
            foreach (Character p in characters)
            {
                if (p.Id == characterId)
                {
                    return p;
                }
            }
            return null;
        }

        /*
         * PLAYERS ACTIONS
         */

        /// <summary>
        /// Move the character with the given id in the given direction.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="direction"></param>
        public void MoveCharacter(int playerId, Direction direction)
        {
            Character p = GetCharacter(playerId);
            if (p != null)
            {
                p.Move(direction, this);
            }
        }

        /// <summary>
        /// Start a battle betwen two characters.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defenderId"></param>
        public void StartBattle(Player attacker, int defenderId)
        {
            Character defender = GetCharacter(defenderId);
            if (attacker.X == defender.X && attacker.Y == defender.Y)
            {
                battles.AddBattle(attacker, defender);
            }
        }

        /// <summary>
        /// End the battle link to a character
        /// </summary>
        /// <param name="c"></param>
        public void EndBattle(Character c)
        {
            battles.EndBattle(c.Battle);
        }

        /// <summary>
        /// Remove a character from the game.
        /// </summary>
        /// <param name="p"></param>
        public void RemoveCharacter(Character p)
        {
            if (p != null)
            {
                characters.Remove(p);
                p.DestroyCharacter(this);
            }
        }

        /// <summary>
        /// A player extract the given resource on his cell.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="resource"></param>
        public void Extract(Player player, string resource)
        {
            if (player != null)
            {
                Resource r = Resource.Parse(resource);
                if (r != null)
                {
                    ((Player)player).Extract(this, r);
                }
            }
        }

        /// <summary>
        /// Try to pick up the given item on the current player cell to the player inventory. 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemName"></param>
        public void PickUpItem(Player player, string itemName)
        {
            if (player != null)
            {
                Cell c = GetCell(player.X, player.Y);
                Tuple<GameItem, int> result = c.CountItem(itemName);
                if (result!=null && result.Item2>0)
                {
                    if (player.Inventory.AddItem(result.Item1))
                    {
                        c.RemoveItems(result.Item1, 1);
                    }
                }
            }
        }

        /// <summary>
        /// The player drop the selected item on the current cell
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemName"></param>
        public void DropItem(Player player, string itemName)
        {
            GameItem item = GameItemGlossary.Parse(itemName);
            if (item != null)
            {
                if(player.Inventory.RemoveItems(item, 1))
                {
                    GetCell(player.X, player.Y).AddItem(item);
                }
            }
        }

        public void EquipItem(Player player, string itemName)
        {
            Equipable item = EquipableGlossary.Parse(itemName);
            if (item != null)
            {
                player.EquipObject(item);
            }
        }

        public void UnequipItem(Player player, string itemName)
        {
            Equipable item = EquipableGlossary.Parse(itemName);
            if (item != null)
            {
                player.UnequipObject(item);
            }
        }

        /*
         * ROUNDS
         */
        /// <summary>
        /// All the fight do one round.
        /// </summary>
        public void DoRounds()
        {
            battles.RoundBattles();
        }

        /*
         * VIEWS
         */

        /// <summary>
        /// Get the view of the player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Cell[,] GetViewPlayer(Player player)
        {
            Cell[,] result = new Cell[3, 3];
            int y = 0;
            for (int i = player.Y - 1; i <= player.Y + 1; i++)
            {
                int x = 0;
                for (int j = player.X - 1; j <= player.X + 1; j++)
                {
                    if (i < 0 || i >= height || j < 0 || j >= width)
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

        /// <summary>
        /// Get the view of the player in a string format.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public string GetViewPlayerString(Player player)
        {
            Cell[,] tab = GetViewPlayer(player);
            String result = "{\"x\":" + player.X + ",\"y\":" + player.Y + ",\"cells\":";
            result += GetViewString(tab) + "}";
            return result;
        }

        /// <summary>
        /// Get the view of the whole map in a string format.
        /// </summary>
        /// <returns></returns>
        public string GetViewMapString()
        {
            return GetViewString(cells);
        }

        /// <summary>
        /// Transform an array of cells in a string format.
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the view of the whole map discovered by a player in a string format.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public string GetViewPlayerMapString(Player p)
        {
            string result = "[";
            bool[,] views = p.MapView;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (views[i, j])
                    {
                        result += cells[i, j].ToString() + ",";
                    }
                    else
                    {
                        result += Cell.voidCell.ToString() + ",";
                    }
                }
            }
            result = result.Remove(result.Length - 1);
            result += "]";
            return result;
        }
    }
}
