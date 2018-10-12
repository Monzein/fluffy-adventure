using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FluffyServ.Model
{
    class Cell
    {
        public static Cell voidCell = new Cell(Terrain.VOID);

        /* Attributs */
        private Terrain type;
        public Terrain Type
        {
            get { return type; }
            set { type = Type; }
        }
        private IDictionary<Ressource, int> resources;
        public IDictionary<Ressource, int> Resources
        {
            get { return resources; }
        }
        /* Constructeurs */
        public Cell(Terrain type)
        {
            this.type = type;
            this.resources = null;
        }
        public Cell(Terrain type, Random r)
        {
            this.type = type;
            this.resources = type.GenerateDictionary(r);
        }

        private string GetResourcesString()
        {
            string result = "[";
            if (resources != null)
            {
                foreach (KeyValuePair<Ressource, int> pair in resources)
                {
                    result += "{\"" + pair.Key.Name + "\":" + pair.Value + "},";
                }
                result = result.Remove(result.Length - 1);
            }
            result += "]";
            return result;
        }

        public override string ToString()
        {
            /*
            string result = "{\"type\":\"" + Type.Nom + "\"," +
                "\"ressources\":\""+ GetResourcesString() +
                "\"}";
                */

            /*
            string result = "{\"type\":\"" + Type.Nom + "\"," +
                "\"ressources\":\"" + JsonConvert.SerializeObject(ressources) +
                "\"}";
                */
            string result = JsonConvert.SerializeObject(Type.Name);

            return result;
        }
    }
}
