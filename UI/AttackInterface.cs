using System;
using System.Collections.Generic;

namespace game {
    public class AttackInterface : UIStep {
        public AttackInterface(Battle _battle, BattleUnitsStack _stack) {
            battle = _battle;
            stack = _stack;
        }

        public override int Actions() {
            BattleArmy opponentArmy;

            Console.WriteLine("Possible targets:");
            if (stack.parentArmy == battle.firstPlayerArmy) {
                opponentArmy = battle.secondPlayerArmy;
            }
            else {
                opponentArmy = battle.firstPlayerArmy;
            }

            int index = 0;
            var targets = new List<BattleUnitsStack>();
            foreach (var target in opponentArmy.unitsStackList) {
                if (target.unitsCount > 0) {
                    Console.WriteLine($"{++index} - {target}");
                    targets.Add(target);
                }
            }

            Console.WriteLine("Choose the target:");
            int chosenTarget = ConsoleUI.GetNumericOption(1, index);

            Action.Attack(stack, targets[chosenTarget-1]);
            ConsoleUI.NewScreen(1800);
            return 0;
        }

        public override UIStep NextStep(int option) {
            return new TurnInterface(battle);
        }

        private Battle battle;
        private BattleUnitsStack stack;
    }
}