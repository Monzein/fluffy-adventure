using FluffyServ.Model.Mechanisms;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FluffyServ.Model.GameItems
{
    /// <summary>
    /// Contain all the receipe to craft GameItem.
    /// </summary>
    public class ItemCrafting
    {
        private Dictionary<string, Dictionary<string,int>> receipes;

        private static ItemCrafting instance = null;
        /// <summary>
        /// The receipes in a string format.
        /// </summary>
        private static string receipesString="";

        /// <summary>
        /// Constructor, private for singleton pattern.
        /// </summary>
        private ItemCrafting()
        {
            InitReceipes();
        }

        /// <summary>
        /// Initialize the receipes.
        /// </summary>
        private void InitReceipes()
        {
            Dictionary<string, int> receipe;
            receipes = new Dictionary<string, Dictionary<string, int>>();

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.FRUIT.Name,5);
            receipes.Add(GameItemGlossary.FRUIT_SALAD.Name,receipe);

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.FOLIAGE.Name,5);
            receipes.Add(GameItemGlossary.BANDAGE.Name, receipe);

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.WOOD.Name, 1);
            receipe.Add(Resource.STONE.Name, 1);
            receipe.Add(Resource.FOLIAGE.Name, 2);
            receipes.Add(GameItemGlossary.MASO.Name, receipe);

            InitToString();
        }

        /// <summary>
        /// Get the single instance of the class.
        /// </summary>
        public static ItemCrafting Instance {
            get
            {
                if (instance == null)
                {
                    instance = new ItemCrafting();
                }
                return instance;
            }
        }

        private void InitToString()
        {
            receipesString = "[";
            foreach(KeyValuePair<string, Dictionary<string, int>> pair in receipes)
            {
                receipesString += ",\"" + pair.Key + "\"";
            }
            receipesString = receipesString.Remove(1, 1);
            receipesString += "]";
        }

        /// <summary>
        /// Craft the item from his name. 
        /// The crafted item is placed in the inventory.
        /// If it can't be placed in the character inventory, it will be placed in the cell.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inventory"></param>
        /// <param name="c"></param>
        public void CraftItem(string name, Inventory inventory, Cell c)
        {
            GameItem item = GameItemGlossary.Parse(name);
            if (item == null)
            {
                return;
            }
            if(receipes.TryGetValue(name, out Dictionary<string, int> receipe))
            {
                IList<Tuple<GameItem, int>> list = new List<Tuple<GameItem, int>>();
                Tuple<GameItem, int> tuple;
                foreach (KeyValuePair<string,int> pair in receipe)
                {
                    tuple = inventory.GetItemCount(pair.Key);
                    if (tuple==null || tuple.Item2 < pair.Value)
                    {
                        return;
                    }
                    list.Add(new Tuple<GameItem, int>(tuple.Item1,pair.Value));
                }
                foreach(Tuple<GameItem, int> t in list)
                {
                    inventory.RemoveItems(t.Item1, t.Item2);
                }
                inventory.AddItem(item, c);
            }
        }

        public override string ToString()
        {
            return receipesString;
        }
    }
}
