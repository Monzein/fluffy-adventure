using System.Collections.Generic;

namespace FluffyServ.Model.Mechanisms
{
    public class Battles
    {
        private List<Battle> battlesList;
        private Grid grid;

        public Battles(Grid grid)
        {
            battlesList = new List<Battle>();
            this.grid = grid;
        }

        public void AddBattle(Character attacker, Character defender)
        {
            battlesList.Add(new Battle(attacker, defender));
        }

        public void EndBattle(Battle battle)
        {
            if (battle != null)
            {
                battle.EndBattle(grid);
                battlesList.Remove(battle);
            }
        }

        public void RoundBattles()
        {
            battlesList.RemoveAll(item => item.DoRound(grid));
        }
    }
}
