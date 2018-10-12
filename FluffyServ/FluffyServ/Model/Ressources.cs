using System.Collections.Generic;

namespace FluffyServ.Model
{
    class Ressources
    {
        /* OCEAN */
        public static Dictionary<Ressource, int> OceanAvailable()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> OceanPossible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }

        /* PLAIN */
        public static Dictionary<Ressource, int> PlainAvailable()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FOLIAGE, 50);
            return result;
        }
        public static Dictionary<Ressource, int> PlainPossible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FRUIT, 50);
            return result;
        }

        /* FOREST */
        public static Dictionary<Ressource, int> ForestAvailable()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.WOOD, 250);
            result.Add(Ressource.FOLIAGE, 400);
            return result;
        }
        public static Dictionary<Ressource, int> ForestPossible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();
            result.Add(Ressource.FRUIT, 100);
            return result;
        }

        /* MOUNTAIN */
        public static Dictionary<Ressource, int> MountainAvailable()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> MountainPossible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }

        /* BEACH */
        public static Dictionary<Ressource, int> BeachAvailable()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
        public static Dictionary<Ressource, int> BeachPossible()
        {
            Dictionary<Ressource, int> result = new Dictionary<Ressource, int>();

            return result;
        }
    }
}
