using FluffyServ.Model.GameItems.Equipables;
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
        public Weapon RightHand
        {
            get { return rightHand; }
            set
            {
                if (value.GetType().Equals(typeof(TwoHandedWeapon)))
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
                if (inventory.AddItem(leftHand) && inventory.SwitchItems(rightHand, item))
                {
                    rightHand = (TwoHandedWeapon)item;
                    leftHand = null;
                }
            }
            else if (itemType.Equals(typeof(OneHandedWeapon)))
            {
                if (rightHand == null || rightHand.GetType().Equals(typeof(TwoHandedWeapon)))
                {
                    if (inventory.SwitchItems(rightHand, item))
                    {
                        rightHand = (OneHandedWeapon)item;
                    }
                }
                else
                {
                    if (inventory.SwitchItems(leftHand, item))
                    {
                        leftHand = (OneHandedWeapon)item;
                    }
                }
            }

            return false;
        }

        public override string ToString()
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

        public override bool UnEquip(string part)
        {
            throw new NotImplementedException();
        }
    }
}
