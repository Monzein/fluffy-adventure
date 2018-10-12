
namespace FluffyServ.Model
{
    abstract class Character
    {
        public const int MAX_ENERGY = 100;
        private static int idCpt = 0;

        /* Attributs */
        private int id;
        public int Id
        {
            get { return id; }
        }
        private string name;
        public string Name
        {
            get { return name; }
        }
        private Displacement movement;
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

        private int energy = MAX_ENERGY;
        public int Energy { get => energy; }

        /* Constructeurs */
        public Character(string name, Displacement movement, int Energy = MAX_ENERGY)
        {
            this.id = NextId();
            this.name = name;
            this.movement = movement;
            this.x = 0;
            this.y = 0;
        }

        public Character(string name, Displacement movement, int x, int y, int Energy = MAX_ENERGY)
        {
            this.id = NextId();
            this.name = name;
            this.movement = movement;
            this.x = x;
            this.y = y;
        }

        /* Création */
        private static int NextId()
        {
            idCpt++;
            return idCpt;
        }

        /* Opérations sur un personnage */
        public virtual void Move(Direction dir, Grid g)
        {
            int x = this.x;
            int y = this.y;

            switch (dir)
            {
                case Direction.NORD:
                    if (y > 0)
                        y -= 1;
                    break;
                case Direction.SUD:
                    if (y < g.Height - 1)
                        y += 1;
                    break;
                case Direction.OUEST:
                    if (x > 0)
                        x -= 1;
                    break;
                case Direction.EST:
                    if (x < g.Width - 1)
                        x += 1;
                    break;
            }
            if (movement.CanMone(g.GetTerrain(x, y)))
            {
                if (energy >= g.GetTerrain(X, Y).Difficulty)
                {
                    energy -= g.GetTerrain(X, Y).Difficulty;
                    this.x = x;
                    this.y = y;
                }
            }
        }
    }

}
