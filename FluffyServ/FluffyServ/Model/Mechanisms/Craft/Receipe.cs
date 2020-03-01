using FluffyServ.Model.Entities.GameItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Model.Mechanisms.Craft
{
    /// <summary>
    /// The list to create a given object. Responsible for the creation of item by character.
    /// </summary>
    public class Receipe
    {

        private GameItem item;
        private Dictionary<string, int> ingredients;
        
        public GameItem Item { get => item; }
        internal Dictionary<string, int> Ingredients { get => ingredients; }

        private string toString = null;

        public Receipe(GameItem item, Dictionary<string, int> ingredients)
        {
            this.item = item;
            this.ingredients = ingredients;
        }

        public String getItemName()
        {
            if (item != null && item.Name != null)
            {
                return item.Name;
            }
            return "";
        }

        /// <summary>
        /// Craft the item of the receipe. Use the item from the given inventory.
        /// If it's possible to create the item, create it then put it in the inventory.
        /// If the inventory is full, the item is dropped on the cell.
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="c"></param>
        public void CraftItem(Inventory inventory, Cell c)
        {
            IList<Tuple<GameItem, int>> list = new List<Tuple<GameItem, int>>();
            Tuple<GameItem, int> tuple;
            foreach (KeyValuePair<string, int> pair in ingredients)
            {
                tuple = inventory.GetTupleItemCount(pair.Key);
                if (tuple == null || tuple.Item2 < pair.Value)
                {
                    return;
                }
                list.Add(new Tuple<GameItem, int>(tuple.Item1, pair.Value));
            }
            foreach (Tuple<GameItem, int> t in list)
            {
                inventory.RemoveItems(t.Item1, t.Item2);
            }
            inventory.AddItem(item, c);
        }

        /// <summary>
        /// Create the string of all the ingredients.
        /// </summary>
        /// <returns></returns>
        private string ingredientsString()
        {
            string result = "[";
            foreach(KeyValuePair<string, int> pair in ingredients){
                GameItem item = GameItemGlossary.Parse(pair.Key);
                string itemPicture = "null";
                if (item != null)
                {
                    itemPicture = item.Picture;
                }
                result += ",{\"Name\":\"" + pair.Key + "\", \"Number\":" + pair.Value + ", \"Picture\":\"" + itemPicture + "\"}";
            }
            result = result.Remove(1, 1);
            result += "]";
            return result;
        }

        public override string ToString()
        {
            if (toString == null)
            {
                toString = "{\"Item\":" + item + ",\"Ingredients\":" + ingredientsString() + "}";
            }
            return toString;
        }
    }
}
