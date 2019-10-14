using FluffyServ.Model.Entities.GameItems.Equipables;

namespace FluffyServ.Model.Mechanisms
{
    /// <summary>
    /// Represent the equipement of a character.
    /// Need to be extended to suit a character type.
    /// </summary>
    public abstract class Equipement
    {
        private int totalDefense = 0;
        private int totalAttack = 0;

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
                if (item.GetType().IsSubclassOf(typeof(Weapon)))
                {
                    totalAttack += ((Weapon)item).Attack;
                }
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
                if (item.GetType().IsSubclassOf(typeof(Weapon)))
                {
                    totalAttack -= ((Weapon)item).Attack;
                }
            }
        }

        /// <summary>
        /// Remove the last item and add the new item.
        /// The two parameteres could be null.
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="oldItem"></param>
        protected virtual void SwitchEquipement(Equipable newItem, Equipable oldItem)
        {
            RemoveEquipement(oldItem);
            AddEquipement(newItem);
        }

        /// <summary>
        /// Equip the given equipable.
        /// Return true if the item was correctly equiped.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract bool EquipObject(Equipable item, Inventory inventory);

        /// <summary>
        /// Unequip the given equipable.
        /// Return true if the item was correctly unequiped.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public abstract bool UnequipObject(Equipable item, Inventory inventory);

        public override sealed string ToString()
        {
            string result = "{\"Defense\":" + totalDefense + ",\"Attack\":" + totalAttack +
                ",\"Equipements\":" + EquipementToString() + "}";

            return result;
        }

        /// <summary>
        /// Must be implemented. The list of items and the position for an equipement set.
        /// </summary>
        /// <returns></returns>
        public abstract string EquipementToString();
    }
}
