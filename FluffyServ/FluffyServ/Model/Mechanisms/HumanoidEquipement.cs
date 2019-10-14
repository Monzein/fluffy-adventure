using FluffyServ.Model.Entities.GameItems.Equipables;
using System;

namespace FluffyServ.Model.Mechanisms
{
    /// <summary>
    /// An equipement set for humanoid character.
    /// </summary>
    public class HumanoidEquipement : Equipement
    {
        private BodyArmor body;
        private Weapon rightHand;
        private OneHandedWeapon leftHand;

        public HumanoidEquipement()
        {

        }
        /// <summary>
        /// The body armor
        /// </summary>
        public BodyArmor Body
        {
            get { return body; }
            set
            {
                RemoveEquipement(body);
                body = value;
                AddEquipement(body);
            }
        }
        /// <summary>
        /// A single handed weapon used on the right hand or a two handed weapon.
        /// </summary>
        public Weapon RightHand
        {
            get { return rightHand; }
            set
            {
                if (value!= null && value.GetType().Equals(typeof(TwoHandedWeapon)))
                {
                    if (leftHand != null)
                    {
                        RemoveEquipement(leftHand);
                    }
                }
                RemoveEquipement(rightHand);
                rightHand = value;
                AddEquipement(rightHand);
            }
        }
        /// <summary>
        /// A single handed weapon used on the left hand.
        /// </summary>
        public OneHandedWeapon LeftHand
        {
            get { return leftHand; }
            set
            {
                if (rightHand == null || rightHand.GetType().Equals(typeof(OneHandedWeapon)))
                {
                    RemoveEquipement(leftHand);
                    leftHand = value;
                    AddEquipement(leftHand);
                }
            }
        }

        /// <summary>
        /// Equip the given equipable.
        /// Return true if the item was correctly equiped.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool EquipObject(Equipable item, Inventory inventory)
        {
            if (inventory.GetItemCount(item) <= 0)
            {
                return false;
            }
            Type itemType = item.GetType();
            if (itemType.Equals(typeof(BodyArmor)))
            {
                if (inventory.SwitchItems(Body, item))
                {
                    Body = (BodyArmor)item;
                }

            }else if (itemType.Equals(typeof(TwoHandedWeapon)))
            {
                if (inventory.CanAddItem(leftHand) && inventory.SwitchItems(rightHand, item))
                {
                    inventory.AddItem(leftHand);
                    RightHand = (TwoHandedWeapon)item;
                    LeftHand = null;
                }
            }
            else if (itemType.Equals(typeof(OneHandedWeapon)))
            {
                if (rightHand == null || rightHand.GetType().Equals(typeof(TwoHandedWeapon)))
                {
                    if (inventory.SwitchItems(rightHand, item))
                    {
                        RightHand = (OneHandedWeapon)item;
                    }
                }
                else
                {
                    if (inventory.SwitchItems(leftHand, item))
                    {
                        LeftHand = (OneHandedWeapon)item;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// The equipements on their given slots.
        /// </summary>
        /// <returns></returns>
        public override string EquipementToString()
        {
            string bodyStr = "null";
            string rightStr = "null";
            string leftStr = "null";
            if(body != null)
            {
                bodyStr = body.ToString();
            }
            if (rightHand != null)
            {
                rightStr = rightHand.ToString();
            }
            if (leftHand != null)
            {
                leftStr = leftHand.ToString();
            }

            string result = "{\"Body\":" + bodyStr +
                ",\"RightHand\":" + rightStr +
                ",\"LeftHand\":" + leftStr + "}";
            return result;
        }

        /// <summary>
        /// Unequipe an object if the object is equipped and put in the inventory.
        /// return true if the item was unequipped.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public override bool UnequipObject(Equipable item, Inventory inventory)
        {
            if (item != null)
            {
                if (inventory.CanAddItem(item))
                {
                    if (item.Equals(body))
                    {
                        inventory.AddItem(body);
                        Body = null;
                    }
                    else if(item.Equals(rightHand))
                    {
                        inventory.AddItem(rightHand);
                        RightHand = null;
                    }
                    else if (item.Equals(leftHand))
                    {
                        inventory.AddItem(leftHand);
                        LeftHand = null;
                    }
                }
            }
            return false;
        }
    }
}
