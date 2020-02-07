namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    /// <summary>
    /// A weapon that need two to be equipped.
    /// </summary>
    public class TwoHandedWeapon : Weapon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="picture"></param>
        /// <param name="space"></param>
        /// <param name="mass"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        public TwoHandedWeapon(string name, string picture, double space, double mass, int attack, int defense) 
            : base(name, picture, space, mass, attack, defense)
        {

        }
    }
}
