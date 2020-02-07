using FluffyServ.Model.Entities.Characters;

namespace FluffyServ.Model.Entities.GameItems
{
    /// <summary>
    /// An item that can be used to gain healh or energy.
    /// </summary>
    public class UsableItem : GameItem
    {
        private int health;
        private int energy;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="health"></param>
        /// <param name="energy"></param>
        internal UsableItem(string name, string picture, double space, double mass, int health, int energy) : base(name, picture, space, mass)
        {
            this.health = health;
            this.energy = energy;
        }

        /// <summary>
        /// The amount of health restored with the item.
        /// </summary>
        public int Health { get => health; }
        /// <summary>
        /// The amount of energy restored with the item.
        /// </summary>
        public int Energy { get => energy; }

        /// <summary>
        /// Use the item on the given character.
        /// </summary>
        /// <param name="c"></param>
        public void UseItem(Character c)
        {
            c.Heal(health);
            c.Rest(energy);
        }

        public override string ToString()
        {
            return "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
                "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name + "\",\"Picture\":\"" + Picture + "\",\"Usable\":\"true\"}";
        }
    }
}
