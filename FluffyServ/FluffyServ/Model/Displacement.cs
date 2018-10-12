using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluffyServ.Model
{
    class Displacement
    {
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
        public bool CanMone(Terrain t)
        {
            if(t == Terrain.VOID)
            {
                return false;
            }
            if (this == ALL) { 
                return true;
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
