namespace FluffyServ.Model.Entities.GameItems
{
    /// <summary>
    /// An item that can be created by the extraction on a cell.
    /// TODO: Fill it from a config file.
    /// </summary>
    public class Resource : GameItem
    {
        /* RESSOURCES */
        public static Resource WOOD = new Resource("bois","wood", 1, 3, 1, 2);
        public static Resource STONE = new Resource("pierre", "stone", 1, 6, 1, 4);
        public static Resource IRON = new Resource("fer", "iron", 1, 10, 0.5, 6);
        public static Resource FOLIAGE = new Resource("feuilllage", "folliage", 1, 1, 1, 1);
        public static Resource SAND = new Resource("sable", "sand", 1, 8, 0.8, 1);
        public static Resource FRUIT = new Resource("fruit", "fruit", 1, 1, 0.2, 1);

        private double ratio;
        /// <summary>
        /// The chance to find this resource. Uused by the game generation.
        /// </summary>
        public double Ratio
        {
            get { return ratio; }
        }
        private int difficulty;
        /// <summary>
        /// The amount of energy required to extract it.
        /// </summary>
        public int Difficulty
        {
            get { return difficulty; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="ratio"></param>
        /// <param name="difficulty"></param>
        internal Resource(string name, string picture, double space, double mass, double ratio, int difficulty) :
            base(name,picture,space,mass)
        {
            this.ratio = ratio;
            this.difficulty = difficulty;
        }

        /// <summary>
        /// Return the GameItem resulting from the extraction.
        /// </summary>
        /// <returns></returns>
        public GameItem Extract()
        {
            return this;
        }

        /// <summary>
        /// Get the Resource from his name.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Resource Parse(string s)
        {
            s = s.ToLower();
            if(WOOD.Name == s)
                return WOOD;
            else if (STONE.Name == s)
                return STONE;
            else if (IRON.Name == s)
                return IRON;
            else if (FOLIAGE.Name == s)
                return FOLIAGE;
            else if (SAND.Name == s)
                return SAND;
            else if (FRUIT.Name == s)
                return FRUIT;

            return null;
        }

        public override string ToString()
        {
            return "{\"Space\":\"" + Space + "\",\"Mass\":\"" + Mass +
                 "\",\"Name\":\"" + Name + "\",\"Picture\":\"" + Picture + "\"}";
        }
    }
}
