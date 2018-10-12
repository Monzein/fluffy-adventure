
namespace FluffyServ.Model
{
    class Player : Character
    {
        public const int MAX_ENERGY = 100;
        private int idPlayer;

        public Player(int x, int y, int idPlayer) : base("joueur", Displacement.TERRESTRIAL, x, y)
        {
            this.idPlayer = idPlayer;
        }

        public int IdPlayer { get => idPlayer; }

        public override void Move(Direction dir, Grid g)
        {
            base.Move(dir, g);
        }
    }
}
