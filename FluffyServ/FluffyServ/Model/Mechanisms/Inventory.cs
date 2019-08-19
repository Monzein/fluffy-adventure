using Newtonsoft.Json;
using System.Collections.Generic;
using FluffyServ.Model.GameItems;
using System;
using FluffyServ.Model.GameItems.Equipables;

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
        /// If a cell is given, add the item to the cell if the inventory is full.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        internal bool AddItem(GameItem obj, Cell c = null)
        {
            if (obj == null)
            {
                return true;
            }
            if (maxMass == -1 && maxSpace == -1)
            {
                AddItems(obj, 1);
                return true;
            }

            if (CurrentMass + obj.Mass <= MaxMass)
            {
                if (CurrentSpace + obj.Space <= MaxSpace)
                {
                    AddItems(obj, 1);
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
        /// Add the items to the inventory. The check must be done before calling this method.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="number"></param>
        private void AddItems(GameItem obj, int number)
        {
            currentMass = CurrentMass + (obj.Mass * number);
            currentSpace = CurrentSpace + (obj.Space * number);
            if (items.ContainsKey(obj))
            {
                items[obj] = items[obj] + number;
            }
            else
            {
                items.Add(obj, number);
            }
        }

        /// <summary>
        /// Add all the items from the given inventory to this inventory.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        internal bool AddFromInventory(Inventory inv, bool emptyAtTheEnd)
        {
            if (maxMass != -1 || maxSpace != -1)
            {
                if (this.currentMass + inv.currentMass > this.maxMass ||
                this.currentSpace + inv.currentSpace > this.MaxSpace)
                {
                    return false;
                }
            }
            foreach (KeyValuePair<GameItem, int> pair in inv.Items)
            {
                AddItems(pair.Key, pair.Value);
            }
            if (emptyAtTheEnd)
            {
                inv.Empty();
            }
            return true;
        }

        /// <summary>
        /// Remove from the inventory a number of the item.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        internal bool RemoveItems(GameItem item, int number = 1)
        {
            if (number < 1)
            {
                return false;
            }
            if(item == null)
            {
                return true;
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
        /// Try to replace an item by another one in the inventory. 
        /// in or out item could be null.
        /// Return false and cancel the switch if there is not enough 
        /// space or too much weight.
        /// </summary>
        /// <param name="outItem"></param>
        /// <param name="inItem"></param>
        /// <returns></returns>
        internal bool SwitchItems(GameItem inItem, GameItem outItem)
        {
            if (RemoveItems(outItem))
            {
                if (AddItem(inItem))
                {
                    return true;
                }
                else
                {
                    if (!AddItem(outItem))
                    {
                        throw new Exception("Switching item is impossible and the outItem: "
                            + outItem.ToString() + " is lost.");
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Empty all the inventory.
        /// </summary>
        internal void Empty()
        {
            this.currentMass = 0;
            this.currentSpace = 0;
            this.items.Clear();
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
        /// Return null if the GameItem can't be found in the cell inventory.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tuple<GameItem, int> GetTupleItemCount(string name)
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

        /// <summary>
        /// Get a GameItem present in the inventory given his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetItemCount(GameItem item)
        {
            if (items != null && item != null)
            {
                if (items.TryGetValue(item, out int result))
                {
                    return result;
                }
            }
            return 0;
        }

        private string ToStringItems()
        {
            string result = "[";
            if (items != null && items.Count > 0)
            {
                foreach (KeyValuePair<GameItem, int> pair in items)
                {
                    if (pair.Key.GetType().Equals(typeof(UsableItem)))
                    {
                        result += ",{\"Name\":\"" + pair.Key.Name + "\",\"Value\":\"" +
                            pair.Value + "\",\"Usable\":\"true\"}";
                    }
                    else if (typeof(Equipable).IsInstanceOfType(pair.Key))
                    {
                        result += ",{\"Name\":\"" + pair.Key.Name + "\",\"Value\":\"" +
                            pair.Value + "\",\"Equipable\":\"true\"}";
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

        public override string ToString()
        {
            string result = "{\"mass\":\"" + currentMass + "/" + MaxSpace +
                "\",\"space\":\"" + CurrentSpace + "/" + MaxSpace +
                "\",\"items\":" + ToStringItems() + "}";
            return result;
        }
    }
}
