using FluffyServ.Model.Entities.GameItems;
using FluffyServ.Model.Mechanisms;
using FluffyServ.Model.Mechanisms.Battle;
using System;

namespace FluffyServ.Model.Entities.Characters
{
    /// <summary>
    /// Represent an abstract character. 
    /// It must be extended to create an animal, human, mosnter, machine,...
    /// It must be extended to create to be playable or not.
    /// </summary>
    public abstract class Character : Entity
    {
        public const int DEFAULT_ENERGY = 100;
        public const int DEFAULT_HEALTH = 100;
        public const int DEFAULT_ATTACK = 5;
        public const int DEFAULT_DEFENSE = 0;

        /* Attributs */
        private Displacement movement;

        private int energy;
        public int Energy { get => energy; }
        private int maxEnergy;
        public int MaxEnergy { get => maxEnergy; }

        private int health = DEFAULT_HEALTH;
        public int Health { get => health; }
        private int maxHealth;
        public int MaxHealth { get => maxHealth; }

        private int attack;
        public int Attack { get => attack; }
        private int defense;
        public int Defense { get => defense; }

        private Inventory inventory;
        internal Inventory Inventory { get => inventory; }

        private Battle battle = null;
        public Battle Battle
        {
            get
            {
                return battle;
            }
            set
            {
                if (value == null)
                {
                    this.battle = null;
                    return;
                }
                if (this.battle != null)
                {
                    throw new Exception("Already in battle");
                }
                else
                {
                    this.battle = value;
                }
            }
        }

        private int x;
        /// <summary>
        /// The character position on X.
        /// </summary>
        public int X
        {
            get { return x; }
        }
        private int y;
        /// <summary>
        /// The character position on y.
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        /* Constructeurs */
        internal Character(string name, int x, int y, Grid g, Displacement movement,
            int maxHealth = DEFAULT_HEALTH, int maxEnergy = DEFAULT_ENERGY,
            int attack = DEFAULT_ATTACK, int defense = DEFAULT_DEFENSE) : base(name)
        {
            this.x = x;
            this.y = y;
            g.GetCell(x, y).AddCharacter(this);
            this.movement = movement;
            this.inventory = new Inventory();
            this.energy = maxEnergy;
            this.maxEnergy = maxEnergy;
            this.health = maxHealth;
            this.maxHealth = maxHealth;
            this.attack = attack;
            this.defense = defense;
        }

        /// <summary>
        /// Use the energy of the character. 
        /// Return true if the action is possible or false is not enough energy is available.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal bool UseEnergy(int amount)
        {
            if (energy >= amount)
            {
                energy -= amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Give energy to the character until the max is reached. 
        /// </summary>
        /// <param name="amount"></param>
        internal void Rest(int amount)
        {
            energy += amount;
            if (energy > MaxEnergy)
            {
                energy = MaxEnergy;
            }
        }

        /// <summary>
        /// Heal the character until his life is full.
        /// </summary>
        /// <param name="amount"></param>
        internal void Heal(int amount)
        {
            health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        /// <summary>
        /// Damage the character. If the life goes under 0, return true and the character is killed.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal bool Damage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                health = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Move the character of one cell in the given direction.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="g"></param>
        internal virtual void Move(Direction dir, Grid g)
        {
            if (this.movement == Displacement.NONE)
            {
                return;
            }
            if(this.battle != null)
            {
                return;
            }
            int x = this.X;
            int y = this.Y;

            switch (dir)
            {
                case Direction.NORD:
                    if (y > 0)
                        y -= 1;
                    break;
                case Direction.SUD:
                    if (y < g.Height - 1)
                        y += 1;
                    break;
                case Direction.OUEST:
                    if (x > 0)
                        x -= 1;
                    break;
                case Direction.EST:
                    if (x < g.Width - 1)
                        x += 1;
                    break;
            }
            if (movement.CanMove(g.GetTerrain(x, y)))
            {
                if (energy >= g.GetTerrain(X, Y).Difficulty)
                {
                    energy -= g.GetTerrain(X, Y).Difficulty;

                    PlaceCharacter(x, y, g);
                }
            }
        }

        /// <summary>
        /// Place a character in the grid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="g"></param>
        internal void PlaceCharacter(int x, int y, Grid g)
        {
            g.GetCell(this.x, this.y).RemoveCharacter(this);
            this.x = x;
            this.y = y;
            g.GetCell(this.x, this.y).AddCharacter(this);
        }

        /// <summary>
        /// Destroy the characters from the grid.
        /// </summary>
        /// <param name="g"></param>
        internal void DestroyCharacter(Grid g)
        {
            g.EndBattle(this);
            g.GetCell(this.x, this.y).AddFromInventory(this.inventory);
            g.GetCell(this.x, this.y).RemoveCharacter(this);
        }

        /// <summary>
        /// Use an item an remove it from the inventory.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal bool UseItem(string name)
        {
            GameItem item = inventory.GetItem(name);
            if (item.GetType().Equals(typeof(UsableItem)))
            {
                if (inventory.RemoveItems(item))
                {
                    ((UsableItem)item).UseItem(this);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attack the target character. Return the BattleActionInfos.
        /// EndBattle is true if the target is killed.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal BattleActionInfos AttackCharacter(Character c)
        {
            BattleActionResult actionResult = BattleActionResult.NOTHING;

            int min = (int)(attack * 0.8);
            int max = (int)(attack * 1.2);

            int value = 0;
            Random rand = new Random();
            double chance = rand.NextDouble();
            // Hit
            if (chance < 0.8)
            {
                value = rand.Next(min, max) - c.defense;
                actionResult = BattleActionResult.HIT;
                // Useless
                if (value <= 0)
                {
                    value = 0;
                    actionResult = BattleActionResult.NO_DAMAGE;
                }
                // Critics
                if (chance < 0.2)
                {
                    value *= 2;
                    actionResult = BattleActionResult.CRITIC;
                }
            }
            // Miss
            else
            {
                value = 0;
                actionResult = BattleActionResult.MISS;
            }
            Console.WriteLine("Damage " + value);
            bool endBattle = c.Damage(value);

            return new BattleActionInfos(endBattle, this.Name, c.Name, value, BattleAction.ATTACK, actionResult);
        }

        /// <summary>
        /// Return true if the character is already in a battle.
        /// </summary>
        /// <returns></returns>
        public bool IsInBattle()
        {
            if (this.battle != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set the next action battle for this character.
        /// </summary>
        /// <param name="actionString"></param>
        public void SetNextBattleAction(string actionString)
        {
            if (this.battle != null)
            {
                this.battle.SetAction(this, actionString);
            }
        }

        public override string ToString()
        {
            string result = "{\"energy\":" + Energy + "," +
                "\"name\":\"" + Name + "\"," +
                "\"maxEnergy\":" + MaxEnergy + "," +
                "\"health\":" + Health + "," +
                "\"maxHealth\":" + DEFAULT_HEALTH +
                "}";

            return result;
        }
    }

}
