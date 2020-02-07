namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    /// <summary>
    /// A weapon equipable.
    /// </summary>
    public abstract class Weapon : Equipable
    {
        private int attack = 0;
        public int Attack { get => attack; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        public Weapon(string name, string picture, double space, double mass, int attack, int defense)
            : base(name, picture, space, mass, defense)
        {
            this.attack = attack;
        }

        public override string ToString()
        {
            string result = "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
            "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name +
            "\",\"Picture\":\"" + Picture +
            "\",\"Type\":\"" + GetType().ToString() +
            "\",\"Attack\":\"" + Attack +
            "\",\"Defense\":\"" + Defense + "\"}";
            return result;
        }
    }
}
