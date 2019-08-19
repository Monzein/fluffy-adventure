namespace FluffyServ.Model.GameItems.Equipables
{
    /// <summary>
    /// A weapon that can be equiped on one hand.
    /// </summary>
    public class OneHandedWeapon : Weapon
    {
        public OneHandedWeapon(string name, double space, double mass, int attack, int defense) 
            : base(name, space, mass, attack, defense)
        {

        }
    }
}
