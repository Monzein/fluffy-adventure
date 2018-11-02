using System;

namespace FluffyServ.Model.Mechanisms
{
    /// <summary>
    /// Represent a battle between two chracters.
    /// </summary>
    public class Battle
    {
        private Character attacker;
        /// <summary>
        /// The character that start the battle.
        /// </summary>
        internal Character Attacker { get => attacker;  }

        private Character defender;
        /// <summary>
        /// The character that was targeted by the battle.
        /// </summary>
        internal Character Defender { get => defender; }

        private BattleAction actionAttacker;
        /// <summary>
        /// The next action of the attacker.
        /// </summary>
        internal BattleAction ActionAttacker { get => actionAttacker; }

        private BattleAction actionDefender;
        /// <summary>
        /// The next action of the defender.
        /// </summary>
        internal BattleAction ActionDefender { get => actionDefender; }

        /// <summary>
        /// Wich character should start this round.
        /// </summary>
        private bool turn;

        /// <summary>
        /// Constructor. Set the battle for the two character.
        /// If one of them is already in a battle, throw an exception.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        internal Battle(Character attacker, Character defender)
        {
            this.attacker = attacker;
            this.defender = defender;
            actionAttacker = BattleAction.NOTHING;
            actionDefender = BattleAction.NOTHING;
            this.attacker.Battle = this;
            this.defender.Battle = this;
        }

        /// <summary>
        /// End the battle for the two characters.
        /// </summary>
        internal void EndBattle(Grid g)
        {
            this.attacker.Battle = null;
            this.defender.Battle = null;
            if (attacker.Health==0)
            {
                g.RemoveCharacter(attacker);
            }
            if (defender.Health == 0)
            {
                g.RemoveCharacter(defender);
            }
        }

        /// <summary>
        /// The two characters fight a round. Return true if the fight is finished (death or flee).
        /// </summary>
        /// <returns></returns>
        internal bool DoRound(Grid g)
        {
            if (turn)
            {
                if (DoActionAttacker())
                {
                    EndBattle(g);
                    return true;
                }
                if (DoActionDefender())
                {
                    EndBattle(g);
                    return true;
                }
            } else
            {
                if (DoActionDefender())
                {
                    EndBattle(g);
                    return true;
                }
                if (DoActionAttacker())
                {
                    EndBattle(g);
                    return true;
                }
            }
            actionAttacker = BattleAction.NOTHING;
            actionDefender = BattleAction.NOTHING;
            turn = !turn;
            return false;
        }

        /// <summary>
        /// Do the action of the attacker. Return true if the battle is ended.
        /// </summary>
        /// <returns></returns>
        private bool DoActionAttacker()
        {
            switch (actionAttacker)
            {
                case BattleAction.ATTACK:
                    return attacker.AttackCharacter(defender);
                case BattleAction.DEFEND:
                    
                    break;
                case BattleAction.FLEE:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Do the action of the defender. Return true if the battle is ended.
        /// </summary>
        /// <returns></returns>
        private bool DoActionDefender()
        {
            switch (actionDefender)
            {
                case BattleAction.ATTACK:
                    return defender.AttackCharacter(attacker);
                case BattleAction.DEFEND:

                    break;
                case BattleAction.FLEE:
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Set the action for the given character if it's one of the fighter.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="actionString"></param>
        public void SetAction(Character c, string actionString)
        {
            BattleAction action = (BattleAction)Enum.Parse(typeof(BattleAction), actionString);
            if (c == attacker)
            {
                actionAttacker = action;
            }
            else if(c == defender)
            {
                actionDefender = action;
            }
        }

        
        public override string ToString()
        {
            string result = "{\"attacker\":" + attacker.ToString() + "," +
                "\"defender\":" + defender.ToString() +
                "}";
            return result;
        }
    }
}
