using FluffyServ.Model.Mechanisms;

namespace FluffyServ.Model
{
    /// <summary>
    /// Template to create characters.
    /// TDO: Fill it from a config file.
    /// </summary>
    public class CharacterTemplate
    {
        public static CharacterTemplate CHICKEN = new CharacterTemplate("Poulet", Displacement.TERRESTRIAL, 10, 5, 1, 1);
        public static CharacterTemplate YOUNG_BOAR = new CharacterTemplate("Marcassin", Displacement.TERRESTRIAL, 60, 50, 5, 2);

        private string name;
        private Displacement movement;
        private int maxHealth;
        private int maxEnergy;
        private int attack;
        private int defense;

        public string Name { get => name; }
        public Displacement Movement { get => movement; }
        public int MaxHealth { get => maxHealth; }
        public int MaxEnergy { get => maxEnergy; }
        public int Attack { get => attack; }
        public int Defense { get => defense; }

        public CharacterTemplate(string name, Displacement movement, int maxHealth, int maxEnergy, int attack, int defense)
        {
            this.name = name;
            this.movement = movement;
            this.maxHealth = maxHealth;
            this.maxEnergy = maxEnergy;
            this.attack = attack;
            this.defense = defense;
        }

    }
}
