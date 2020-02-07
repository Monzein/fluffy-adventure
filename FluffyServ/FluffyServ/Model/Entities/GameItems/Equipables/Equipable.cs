namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    /// <summary>
    /// Represent an objec that can be equiped.
    /// </summary>
    public abstract class Equipable : GameItem
    {
        private int defense = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="defense"></param>
        public Equipable(string name, string picture, double space, double mass, int defense) : base(name, picture, space, mass)
        {
            this.defense = defense;
        }

        public int Defense { get => defense; }

        public override string ToString()
        {
            string result = "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
            "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name +
            "\",\"Picture\":\"" + Picture +
            "\",\"Attack\":\"" + 0 +
            "\",\"Defense\":\"" + Defense + "\"}";
            return result;
        }
    }
}
