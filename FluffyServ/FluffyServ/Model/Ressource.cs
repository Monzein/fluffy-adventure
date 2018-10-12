
namespace FluffyServ.Model
{
    public class Ressource : GameObject 
    {
        /* RESSOURCES */
        public static Ressource WOOD = new Ressource("bois", 1, 3, 1, 2);
        public static Ressource STONE = new Ressource("pierre", 1, 6, 1, 4);
        public static Ressource IRON = new Ressource("fer", 1, 10, 0.5, 6);
        public static Ressource FOLIAGE = new Ressource("feuilllage", 1, 1, 1, 1);
        public static Ressource SAND = new Ressource("sable", 1, 8, 0.8, 1);
        public static Ressource FRUIT = new Ressource("fruit", 1, 1, 0.2, 1);

        /* Attributs */
        private double ratio;        // Chances to find it
        public double Ratio
        {
            get { return ratio; }
        }
        private double difficulty;   // Chances to harvest it
        public double Difficulty
        {
            get { return difficulty; }
        }

        /* Constructeurs */
        private Ressource(string name, double space, double mass, double ratio, double difficulty) :
            base(name,space, mass)
        {
            this.ratio = ratio;
            this.difficulty = difficulty;
        }
    }
}
