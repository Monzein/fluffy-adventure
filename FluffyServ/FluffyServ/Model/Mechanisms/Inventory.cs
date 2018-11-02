using Newtonsoft.Json;
using System.Collections.Generic;
using FluffyServ.Model.GameItems;
using System;

namespace FluffyServ.Model.Mechanisms
{
    public class Inventory
    {
        public const double DEFAULT_MAX_SPACE = 100;
        public const double DEFAULT_MAX_MASS = 100;

        private Dictionary<GameItem, int> items = new Dictionary<GameItem, int>();
        private double maxSpace;
        private double maxMass;
        private double currentSpace = 0;
        private double currentMass = 0;

        internal Inventory(double maxSpace = DEFAULT_MAX_SPACE, double maxMass = DEFAULT_MAX_MASS)
        {
            this.maxSpace = maxSpace;
            this.maxMass = maxMass;
        }

        public double MaxSpace { get => maxSpace; }
        public double MaxMass { get => maxMass; }
        public double CurrentSpace { get => currentSpace; }
        public double CurrentMass { get => currentMass; }
        public Dictionary<GameItem, int> Items { get => items; }

        /// <summary>
        /// Add an item in the inventory. 
        /// If a cell is given, remove the item from the cell from the cell before adding to the inventory.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        internal bool AddItem(GameItem obj, Cell c = null)
        {
            if (obj == null)
            {
                return false;
            }
            if(maxMass==-1 && maxSpace == -1)
            {
                if (items.ContainsKey(obj))
                {
                    items[obj] = items[obj] + 1;
                }
                else
                {
                    items.Add(obj, 1);
                }
                return true;
            }

            if (CurrentMass + obj.Mass <= MaxMass)
            {
                if (CurrentSpace + obj.Space <= MaxSpace)
                {
                    currentMass = CurrentMass + obj.Mass;
                    currentSpace = CurrentSpace + obj.Space;
                    if (items.ContainsKey(obj))
                    {
                        items[obj] = items[obj] + 1;
                    }
                    else
                    {
                        items.Add(obj, 1);
                    }
                    return true;
                }
            }
            if (c != null)
            {
                c.AddItem(obj);
            }
            return false;
        }

        /// <summary>
        /// Remove from the inventory a number of the item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        internal bool RemoveItems(GameItem item, int number=1)
        {
            if (number < 1)
            {
                return false;
            }
            if (items != null)
            {
                if (items.ContainsKey(item))
                {
                    int temp = items[item];
                    if (temp < number)
                    {
                        return false;
                    }
                    temp = temp - number;
                    if (temp == 0)
                    {
                        items.Remove(item);
                    }
                    else
                    {
                        items[item] = temp;
                    }
                    currentSpace = CurrentSpace - (item.Space * number);
                    currentMass = currentMass - (item.Mass * number);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get a GameItem present in the inventory given his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameItem GetItem(string name)
        {
            if (items != null)
            {
                GameItem item = GameItemGlossary.Parse(name);
                if (item != null)
                {
                    if (items.ContainsKey(item))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Return the GameItem and the number of in the inventory gien the name of the item.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tuple<GameItem, int> GetItemCount(string name)
        {
            if (items != null)
            {
                GameItem item = GameItemGlossary.Parse(name);
                if (item != null)
                {
                    if (items.ContainsKey(item))
                    {
                        return new Tuple<GameItem, int>(item, items[item]);
                    }
                }
            }
            return null;
        }

        public override string ToString()
        {
            string result = "[";
            if (items != null && items.Count > 0)
            {
                foreach (KeyValuePair<GameItem, int> pair in items)
                {
                    if (pair.Key.GetType().Equals(typeof (UsableItem)))
                    {
                        result += ",{\"Name\":\"" + pair.Key.Name + "\",\"Value\":\"" +
                            pair.Value + "\",\"Usable\":\"true\"}";
                    }
                    else
                    {
                        result += ",{\"Name\":\"" + pair.Key.Name + "\",\"Value\":\"" + pair.Value + "\"}";
                    }
                }
                result = result.Remove(1, 1);
            }
            result += "]";
            return result;
        }
    }
}
