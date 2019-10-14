namespace FluffyServ.Model.Entities.Characters
{
    /// <summary>
    /// A character non playable with no comportement.
    /// </summary>
    public class NonPlayableCharacter : Character
    {
        public NonPlayableCharacter(CharacterTemplate template, int x, int y, Grid g)
            : base(template.Name, x, y, g, template.Movement, template.MaxHealth,
                  template.MaxEnergy, template.Attack, template.Defense)
        {
            Inventory.AddFromInventory(template.Inventory,false);
        }
    }
}
