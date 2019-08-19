namespace FluffyServ.Model.GameItems.Equipables
{
    /// <summary>
    /// A weapon equipable.
    /// </summary>
    public abstract class Weapon : Equipable
    {
        private int attack = 0;
        public int Attack { get => attack; }

        public Weapon(string name, double space, double mass, int attack, int defense)
            : base(name, space, mass, defense)
        {
            this.attack = attack;
        }

        public override string ToString()
        {
            string result = "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
            "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name +
            "\",\"Type\":\"" + GetType().ToString() +
            "\",\"Attack\":\"" + Attack +
            "\",\"Defense\":\"" + Defense + "\"}";
            return result;
        }
    }
}
