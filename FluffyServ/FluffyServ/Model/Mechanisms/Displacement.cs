namespace FluffyServ.Model.Mechanisms
{
    public class Displacement
    {
        public static Displacement NONE = new Displacement("none");
        public static Displacement ALL = new Displacement("all");
        public static Displacement TERRESTRIAL = new Displacement("terrestre");
        public static Displacement MARITIME = new Displacement("maritime");

        /* Constructeur */
        private Displacement(string name)
        {
            this.name = name;
        }

        /* Attributs */
        private string name;
        public string Name
        {
            get { return name; }
        }

        /* Méthodes */
        public bool CanMove(Terrain t)
        {
            if(t == Terrain.VOID)
            {
                return false;
            }
            if (this == ALL) { 
                return true;
            }
            else if(this == NONE)
            {
                return false;
            }
            else if(this == MARITIME)
            {
                if (t == Terrain.OCEAN || t == Terrain.BEACH)
                {
                    return true;
                }
            }else if(this == TERRESTRIAL)
            {
                if(t != Terrain.OCEAN)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
