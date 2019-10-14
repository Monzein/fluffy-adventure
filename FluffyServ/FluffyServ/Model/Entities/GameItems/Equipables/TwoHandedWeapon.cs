namespace FluffyServ.Model.Entities.GameItems.Equipables
{
    /// <summary>
    /// A weapon that need two to be equipped.
    /// </summary>
    public class TwoHandedWeapon : Weapon
    {
        public TwoHandedWeapon(string name, double space, double mass, int attack, int defense) 
            : base(name, space, mass, attack, defense)
        {

        }
    }
}
