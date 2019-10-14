namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    /// <summary>
    /// Represent an objec that can be equiped.
    /// </summary>
    public abstract class Equipable : GameItem
    {
        private int defense = 0;

        public Equipable(string name, double space, double mass, int defense) : base(name, space, mass)
        {
            this.defense = defense;
        }

        public int Defense { get => defense; }

        public override string ToString()
        {
            string result = "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
            "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name +
            "\",\"Attack\":\"" + 0 +
            "\",\"Defense\":\"" + Defense + "\"}";
            return result;
        }
    }
}
