using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using FluffyServ.Model.GameItems;
using FluffyServ.Model.Mechanisms;

namespace FluffyServ.Model
{
    /// <summary>
    /// A cell is a small portion of the grid. 
    /// Each cell have different properties and can host entities.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The deault void cell.
        /// </summary>
        public static Cell voidCell = new Cell(Terrain.VOID);

        private Terrain type;
        /// <summary>
        /// The terrain of a cell.
        /// </summary>
        public Terrain Type
        {
            get { return type; }
            set { type = Type; }
        }
        private IDictionary<Resource, int> resources;
        /// <summary>
        /// The resource available on a cell.
        /// </summary>
        public IDictionary<Resource, int> Resources
        {
            get { return resources; }
        }

        private Inventory inventory;

        private List<Character> characters;
        /// <summary>
        /// The characters present on the cell.
        /// </summary>
        public List<Character> Characters
        {
            get { return characters; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="r"></param>
        internal Cell(Terrain type, Random r = null)
        {
            this.type = type;
            this.characters = new List<Character>();
            this.inventory = new Inventory(-1, -1);
            if (r == null)
            {
                this.resources = null;
            }
            else
            {
                this.resources = type.GenerateDictionary(r);
            }
        }

        /// <summary>
        /// Add an character on the cell.
        /// </summary>
        /// <param name="entity"></param>
        internal void AddCharacter(Character character)
        {
            characters.Add(character);
        }

        /// <summary>
        /// Remove an entity on the cell.
        /// </summary>
        /// <param name="entity"></param>
        internal void RemoveCharacter(Character character)
        {
            characters.Remove(character);
        }

        internal void AddItem(GameItem item)
        {
            inventory.AddItem(item);
        }

        internal void RemoveItems(GameItem item, int number = 1)
        {
            inventory.RemoveItems(item, number);
        }

        /// <summary>
        /// Extract a resource from the cell.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        internal GameItem Extract(Resource r)
        {
            if (resources.TryGetValue(r, out int value))
            {
                if (value > 0)
                {
                    value--;
                    resources[r] = value;
                    return r.Extract();
                }
            }
            return null;
        }

        /// <summary>
        /// Return the resource on a string format.
        /// </summary>
        /// <returns></returns>
        private string RessourcesToString()
        {
            string result = "[";
            if (resources!= null && resources.Count > 0)
            {
                foreach (KeyValuePair<Resource, int> pair in resources)
                {
                    result += ",{\"name\":\"" + pair.Key.Name + "\",\"value\":\"" + pair.Value + "\"}";
                }
                result = result.Remove(1, 1);
            }
            result += "]";
            return result;
        }

        public override string ToString()
        {
            string result = "{\"type\":\"" + Type.Name + "\"," +
                "\"characters\":" + JsonConvert.SerializeObject(characters) + "," +
                "\"items\":" + inventory.ToString() + "," +
                "\"ressources\":" + RessourcesToString() +
                "}";

            return result;
        }
    }
}
