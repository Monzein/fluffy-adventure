using System.Collections.Generic;

namespace FluffyServ.Model.Entities.GameItems
{
    /// <summary>
    /// The resources available and possible on each terrain.
    /// </summary>
    public class Resources
    {
        /* OCEAN */
        public static Dictionary<Resource, int> OceanAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();

            return result;
        }
        public static Dictionary<Resource, int> OceanPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();

            return result;
        }

        /* PLAIN */
        public static Dictionary<Resource, int> PlainAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.FOLIAGE, 50);
            return result;
        }
        public static Dictionary<Resource, int> PlainPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.FRUIT, 50);
            result.Add(Resource.WOOD, 20);
            result.Add(Resource.STONE, 20);
            return result;
        }

        /* FOREST */
        public static Dictionary<Resource, int> ForestAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.WOOD, 250);
            result.Add(Resource.FOLIAGE, 400);
            return result;
        }
        public static Dictionary<Resource, int> ForestPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.FRUIT, 100);
            return result;
        }

        /* MOUNTAIN */
        public static Dictionary<Resource, int> MountainAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.STONE, 500);
            return result;
        }
        public static Dictionary<Resource, int> MountainPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.IRON, 50);
            result.Add(Resource.SAND, 50);
            return result;
        }

        /* BEACH */
        public static Dictionary<Resource, int> BeachAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.SAND, 400);
            return result;
        }
        public static Dictionary<Resource, int> BeachPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();
            result.Add(Resource.STONE, 50);
            result.Add(Resource.WOOD, 5);
            return result;
        }

        /* RIVER */
        public static Dictionary<Resource, int> DesolationAvailable()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();

            return result;
        }
        public static Dictionary<Resource, int> DesolationPossible()
        {
            Dictionary<Resource, int> result = new Dictionary<Resource, int>();

            return result;
        }
    }
}
