using System;
using System.Linq;

namespace game {
    public class EndGameInterface : UIStep {
        public EndGameInterface(Battle _battle, BattleUnitsStack _surrenderedStack = null) {
            surrenderedStack = _surrenderedStack;
            battle = _battle;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            if (surrenderedStack != null) {
                battle.Surrender(surrenderedStack);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            if (battle.winner == battle.firstPlayerArmy) {
                Console.WriteLine($"Player {battle.firstPlayer.nickname} WINS!");
            }
            else {
                Console.WriteLine($"Player {battle.secondPlayer.nickname} WINS!");
            }
            Console.ResetColor();

            Console.WriteLine($"Armies:\n\t{battle.firstPlayer.nickname}:");
            ConsoleUI.PrintList(battle.firstPlayerArmy.unitsStackList.Select(x => $"{x.unitsType.type}: {x.unitsCount}"), "\t\t");
            Console.WriteLine($"\t{battle.secondPlayer.nickname}: ");
            ConsoleUI.PrintList(battle.secondPlayerArmy.unitsStackList.Select(x => $"{x.unitsType.type}: {x.unitsCount}"), "\t\t");

            battle.UpdateArmies();
            
            return 0;
        }

        public override UIStep NextStep(int option) {
            return null;
        }

        private Battle battle;
        private BattleUnitsStack surrenderedStack;
    }
}