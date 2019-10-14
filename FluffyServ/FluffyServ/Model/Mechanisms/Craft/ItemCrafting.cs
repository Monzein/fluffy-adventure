using FluffyServ.Model.Entities.GameItems;
using FluffyServ.Model.Entities.GameItems.Equipables;
using FluffyServ.Model.Mechanisms;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FluffyServ.Model.Mechanisms.Craft
{
    /// <summary>
    /// Contain all the receipe to craft GameItem.
    /// </summary>
    public class ItemCrafting
    {
        //Dicitionnary to access easily the receipe.
        private Dictionary<string, Receipe> receipes;

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
            receipes = new Dictionary<string, Receipe>();

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.FRUIT.Name,5);
            receipes.Add(GameItemGlossary.FRUIT_SALAD.Name, new Receipe(GameItemGlossary.FRUIT_SALAD,receipe));

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.FOLIAGE.Name,5);
            receipes.Add(GameItemGlossary.BANDAGE.Name, new Receipe(GameItemGlossary.BANDAGE, receipe));

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.WOOD.Name, 1);
            receipe.Add(Resource.STONE.Name, 1);
            receipe.Add(Resource.FOLIAGE.Name, 2);
            receipes.Add(GameItemGlossary.MASO.Name, new Receipe(GameItemGlossary.MASO, receipe));

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.WOOD.Name, 5);
            receipes.Add(EquipableGlossary.WOOD_SPEAR.Name, new Receipe(EquipableGlossary.WOOD_SPEAR, receipe));

            receipe = new Dictionary<string, int>();
            receipe.Add(Resource.WOOD.Name, 2);
            receipes.Add(EquipableGlossary.WOOD_CUDGLE.Name, new Receipe(EquipableGlossary.WOOD_CUDGLE, receipe));

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
            foreach(KeyValuePair<string, Receipe> pair in receipes)
            {
                receipesString += "," + pair.Value;
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
            if (receipes.TryGetValue(name, out Receipe receipe))
            {
                receipe.CraftItem(inventory, c);
            }
        }

        public override string ToString()
        {
            return receipesString;
        }
    }
}
