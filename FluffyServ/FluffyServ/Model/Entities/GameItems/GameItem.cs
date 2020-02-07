namespace FluffyServ.Model.Entities.GameItems
{
    /// <summary>
    /// Represent an item that can be stored in an inventory.
    /// </summary>
    public class GameItem : Entity
    {
        private double space;
        private double mass;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        public GameItem(string name, string picture, double space, double mass) : base(name, picture)
        {
            this.space = space;
            this.mass = mass;
        }

        /// <summary>
        /// The space of the object.
        /// </summary>
        public double Space { get => space; }
        /// <summary>
        /// The mass of the object.
        /// </summary>
        public double Mass { get => mass; }

        public override string ToString()
        {
            return "{\"Space\":\"" + space + "\",\"Mass\":\"" + mass + 
                "\",\"Id\":\"" + Id + "\",\"Name\":\"" + Name + "\",\"Picture\":\"" + Picture + "\"}";
        }
    }
}
