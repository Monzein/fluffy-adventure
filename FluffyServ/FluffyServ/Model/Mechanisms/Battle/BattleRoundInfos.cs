using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Model.Mechanisms.Battle
{
    /// <summary>
    /// Contains informations of a round of battle
    /// </summary>
    public class BattleRoundInfos
    {

        private bool endBattle;
        private BattleActionInfos firstAction;
        private BattleActionInfos secondAction;

        public BattleRoundInfos(BattleActionInfos firstAction, BattleActionInfos secondAction)
        {
            this.endBattle = false;
            if (firstAction != null)
            {
                this.endBattle = firstAction.EndBattle;
            }
            if(secondAction != null)
            {
                this.endBattle = this.endBattle || secondAction.EndBattle;
            }
            this.firstAction = firstAction;
            this.secondAction = secondAction;
        }

        public bool EndBattle { get => endBattle;}
        public BattleActionInfos FirstAction { get => firstAction;}
        public BattleActionInfos SecondAction { get => secondAction;}

        public override string ToString()
        {
            return "BattleInfo:{EndBattle: " + endBattle + ";FirstAction: " +
                firstAction.Message + ";SecondAction: " + secondAction.Message + "}";
        }

        public static string generateAction(string playerName, BattleAction action)
        {

            return "";
        }
    }
}
