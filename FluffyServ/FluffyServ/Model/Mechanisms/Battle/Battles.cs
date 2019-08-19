using System.Collections.Generic;

namespace FluffyServ.Model.Mechanisms.Battle
{
    /// <summary>
    /// Manage all the battle on a grid.
    /// </summary>
    public class Battles
    {
        private List<Battle> battlesList;
        private Grid grid;

        /// <summary>
        /// Bbattle constructor.
        /// </summary>
        /// <param name="grid"></param>
        public Battles(Grid grid)
        {
            battlesList = new List<Battle>();
            this.grid = grid;
        }

        /// <summary>
        /// Create a battle between two characters.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        public void AddBattle(Character attacker, Character defender)
        {
            battlesList.Add(new Battle(attacker, defender));
        }

        /// <summary>
        /// End a battle.
        /// </summary>
        /// <param name="battle"></param>
        public void EndBattle(Battle battle)
        {
            if (battle != null)
            {
                battle.EndBattle(grid);
                battlesList.Remove(battle);
            }
        }

        /// <summary>
        /// All the battles do a round.
        /// </summary>
        public void RoundBattles()
        {
            battlesList.RemoveAll(item => item.DoRound(grid).EndBattle);
        }
    }
}
