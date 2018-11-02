using FluffyServ.Model.GameItems;
using FluffyServ.Model.Mechanisms;

namespace FluffyServ.Model
{
    public class Player : Character
    {
        public const int MAX_ENERGY = 100;
        private int idPlayer;
        private bool[,] mapView;

        internal Player(Grid g, int x, int y, int idPlayer) : base("joueur_" + idPlayer, x, y, g, Displacement.TERRESTRIAL)
        {
            this.idPlayer = idPlayer;
            this.mapView = new bool[g.Height, g.Width];
            View(g);
        }

        public int IdPlayer { get => idPlayer; }
        public bool[,] MapView { get => mapView; }

        private void View(Grid g)
        {
            for (int i = this.Y - 1; i <= this.Y + 1; i++)
            {
                for (int j = this.X - 1; j <= this.X + 1; j++)
                {
                    if (!(i < 0 || i >= g.Height || j < 0 || j >= g.Width))
                    {
                        mapView[i, j] = true;
                    }
                }
            }
        }

        internal override void Move(Direction dir, Grid g)
        {
            base.Move(dir, g);
            View(g);
        }

        internal void Extract(Grid g, Resource r)
        {
            UseEnergy(r.Difficulty);
            GameItem item = g.GetCell(X, Y).Extract(r);
            if (!this.Inventory.AddItem(item))
            {
                g.GetCell(X, Y).AddItem(item);
            }
        }

        internal void Craft(string name, Grid g)
        {
            ItemCrafting.Instance.CraftItem(name, Inventory, g.GetCell(X,Y));
        }

        public override string ToString()
        {
            string result = "{\"id\":\"" + idPlayer + "\"," +
                "\"name\":\"" + Name + "\"," +
                "\"energy\":" + Energy + "," +
                "\"maxEnergy\":" + MaxEnergy + "," +
                "\"health\":" + Health + "," +
                "\"maxHealth\":" + DEFAULT_HEALTH +
                "}";

            return result;
        }
    }
}
