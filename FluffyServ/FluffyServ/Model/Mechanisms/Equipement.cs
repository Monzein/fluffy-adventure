using FluffyServ.Model.GameItems.Equipables;

namespace FluffyServ.Model.Mechanisms
{
    /// <summary>
    /// Represent the equipement of a character.
    /// Need to be extended to suit a character type.
    /// </summary>
    public abstract class Equipement
    {
        private int totalDefense = 0;

        /// <summary>
        /// The total of defense point of the equipement.
        /// </summary>
        public int TotalDefense { get => totalDefense; }

        /// <summary>
        /// Add an equipable to the equipement.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void AddEquipement(Equipable item)
        {
            if (item != null)
            {
                totalDefense += item.Defense;
            }
        }

        /// <summary>
        /// Remove an equipable from the equipement.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void RemoveEquipement(Equipable item)
        {
            if (item != null)
            {
                totalDefense -= item.Defense;
            }
        }

        /// <summary>
        /// Equip the given equipable.
        /// Return true if the item was correctly equiped.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool EquipObject(Equipable item, Inventory inventory);


        public abstract bool UnEquip(string part);
    }
}
