using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyServ.Model.Mechanisms.Battle
{
    /// <summary>
    /// Contains the information of one action during a battle.
    /// </summary>
    public class BattleActionInfos
    {

        private static string[] NOTHING_MESSAGES = { " regarde les papillons devant son adversaire.",
            " se demande si l'herbe est verte quand personne ne la voit.",
            " préfère philosopher sur le sens du mot carotte.",
            " fait une pause.", " à oublier d'agir.", " use de son droit de ne rien faire.",
            " respire.", " ne fait rien.", " n'agit pas.", " attends.", " passe son tour.", " se laisse faire."
        };

        private bool endBattle;
        private string message;

        public bool EndBattle { get => endBattle; }
        public string Message { get => message; }

        public BattleActionInfos(bool endBattle, string attackerName, string defenderName, int points, BattleAction action, BattleActionResult result)
        {
            this.endBattle = endBattle;
            constructMeessage(endBattle, attackerName, defenderName, points, action, result);
        }

        public BattleActionInfos(bool endBattle, string attackerName, BattleAction action)
        {
            this.endBattle = endBattle;
            constructMeessage(endBattle, attackerName, "", 0, action, BattleActionResult.NOTHING);
        }

        private void constructMeessage(bool endBattle, string attackerName, string defenderName, int points, BattleAction action, BattleActionResult result)
        {
            switch (action)
            {
                case BattleAction.ATTACK:

                    string s = points > 1 ? "s" : "";
                    switch (result)
                    {
                        case BattleActionResult.HIT:
                            message = attackerName + " attaque et inflige " + points + " point" + s + " de vie à " + defenderName + ".";
                            break;
                        case BattleActionResult.CRITIC:
                            message = attackerName + " attaque et inflige un coup critique à " + points + " point" + s + " de vie à " + defenderName + ".";
                            break;
                        case BattleActionResult.MISS:
                            message = attackerName + " rate son coup et loupe " + defenderName + ".";
                            break;
                        case BattleActionResult.NO_DAMAGE:
                            message = attackerName + " attaque mais n'arrive pas à égratiner " + defenderName + ".";
                            break;
                        default:
                            message = "Pas de résultat d'action. Celà ne devrait jamais arriver :(";
                            break;
                    }
                    break;
                case BattleAction.DEFEND:
                    message = attackerName + " veut se défendre mais celà ne sert à rien"; ;
                    break;
                case BattleAction.FLEE:
                    message = attackerName + " s'enfuit.";
                    break;
                case BattleAction.NOTHING:
                    Random rand = new Random();
                    double chance = rand.NextDouble();
                    int position = (int)(chance * NOTHING_MESSAGES.Length);
                    message = attackerName + NOTHING_MESSAGES[position];
                    break;
                default:
                    message = "Pas d'action de combat. Celà ne devrait pas arriver :(";
                    break;
            }
            if (endBattle)
            {
                message += " Le combat est terminé.";
            }
        }

        public override string ToString()
        {
            return message;
        }
    }
}
