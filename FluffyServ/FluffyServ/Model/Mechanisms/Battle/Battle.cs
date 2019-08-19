using System;

namespace FluffyServ.Model.Mechanisms.Battle
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
        /// The BattleRoundInfos create the last turn.
        /// </summary>
        private BattleRoundInfos lastRoundInfos;

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
            this.lastRoundInfos = new BattleRoundInfos(null, null);
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
        /// The two characters fight a round. Create a BattleRoundInfos, set it to the lastRound and return it.
        /// EndBattle is true if the fight is finished (death or flee).
        /// </summary>
        /// <returns></returns>
        internal BattleRoundInfos DoRound(Grid g)
        {
            BattleActionInfos firstResult = null;
            BattleActionInfos secondResult = null;

            if (turn)
            {
                firstResult = DoActionAttacker();
                if (firstResult.EndBattle)
                {
                    EndBattle(g);
                    return new BattleRoundInfos(firstResult,secondResult);
                }
                secondResult = DoActionDefender();
                if (secondResult.EndBattle)
                {
                    EndBattle(g);
                    return new BattleRoundInfos(firstResult, secondResult);
                }
            } else
            {
                firstResult = DoActionDefender();
                if (firstResult.EndBattle)
                {
                    EndBattle(g);
                    return new BattleRoundInfos(firstResult, secondResult);
                }
                secondResult = DoActionAttacker();
                if (secondResult.EndBattle)
                {
                    EndBattle(g);
                    return new BattleRoundInfos(firstResult, secondResult);
                }
            }
            actionAttacker = BattleAction.NOTHING;
            actionDefender = BattleAction.NOTHING;
            turn = !turn;
            lastRoundInfos = new BattleRoundInfos(firstResult, secondResult);
            return lastRoundInfos;
        }

        /// <summary>
        /// Do the action of the first character on the second character.
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="action"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        private static BattleActionInfos DoActionOfCharacter(Character c1, BattleAction action, Character c2)
        {
            bool endBattle = false;
            switch (action)
            {
                case BattleAction.ATTACK:
                    return c1.AttackCharacter(c2);
                case BattleAction.DEFEND:
                    endBattle = false;
                    break;
                case BattleAction.FLEE:
                    endBattle = true;
                    break;
            }
            return new BattleActionInfos(endBattle, c1.Name, action);
        }

        /// <summary>
        /// Do the action of the attacker. Return true if the battle is ended.
        /// </summary>
        /// <returns></returns>
        private BattleActionInfos DoActionAttacker()
        {
            return DoActionOfCharacter(attacker, actionAttacker, defender);
        }

        /// <summary>
        /// Do the action of the defender. Return true if the battle is ended.
        /// </summary>
        /// <returns></returns>
        private BattleActionInfos DoActionDefender()
        {
            return DoActionOfCharacter(defender, actionDefender, attacker);
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
                "\"defender\":" + defender.ToString() + "," +
                "\"firstMessage\":\"" + lastRoundInfos.FirstAction + "\"," +
                "\"secondMessage\":\"" + lastRoundInfos.SecondAction + "\"" +
                "}";
            return result;
        }
    }
}
