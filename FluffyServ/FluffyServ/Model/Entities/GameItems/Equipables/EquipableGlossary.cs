using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    public class EquipableGlossary
    {
        public static TwoHandedWeapon WOOD_SPEAR = new TwoHandedWeapon("Lance en bois", "spear", 5, 1, 5, 1);
        public static TwoHandedWeapon STUNNING_STONE = new TwoHandedWeapon("Masse de pierre", "stone", 1, 5, 3, 0);
        public static OneHandedWeapon WOOD_CUDGLE = new OneHandedWeapon("Gourdin en bois", "cudgle", 3, 2, 2, 0);
        public static OneHandedWeapon WOOD_RUDIMENTARY_SHIELD = new OneHandedWeapon("Bouclier en bois rudimentaire", "shield", 3, 3, 0, 5);
        public static BodyArmor CASUAL_CLOTHES = new BodyArmor("Tenue simple", "casual_clothes", 1, 1, 1);
        public static BodyArmor FUR_CLOTHES = new BodyArmor("Ensemble de fourrure", "fur_clothes", 6, 4, 5);

        /// <summary>
        /// Get the Equipable by his name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Equipable Parse(string name)
        {
            if (name == WOOD_SPEAR.Name)
            {
                return WOOD_SPEAR;
            }
            else if (name == STUNNING_STONE.Name)
            {
                return STUNNING_STONE;
            }
            else if (name == WOOD_CUDGLE.Name)
            {
                return WOOD_CUDGLE;
            }
            else if (name == WOOD_RUDIMENTARY_SHIELD.Name)
            {
                return WOOD_RUDIMENTARY_SHIELD;
            }
            else if (name == CASUAL_CLOTHES.Name)
            {
                return CASUAL_CLOTHES;
            }
            else if (name == FUR_CLOTHES.Name)
            {
                return FUR_CLOTHES;
            }

            return null;
        }
    }
}
