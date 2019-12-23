using System;
using System.Collections.Generic;
using System.Linq;

namespace game {
    public class CastInterface : UIStep {
        public CastInterface(Battle _battle, BattleUnitsStack _stack) {
            battle = _battle;
            stack = _stack;
        }

        public override int Actions() {
            List<ICast> possibleCasts = battle.intitativeScale.NextStack()?.Casts.Where(c => !c.hasBeenCasted).ToList();

            int index = 0;
            foreach (var c in possibleCasts) {
                Console.WriteLine($"{++index} - {c.type}");
            }
            int chosenCast = ConsoleUI.GetNumericOption(1, index);
            
            var cast = possibleCasts[chosenCast-1];

            BattleArmy targetArmy;
            if (stack.parentArmy == battle.firstPlayerArmy) {
                targetArmy = battle.secondPlayerArmy;
            }
            else {
                targetArmy = battle.firstPlayerArmy;
            }

            if (cast is IFriendCast) {
                targetArmy = stack.parentArmy;
            }

            index = 0;
            
            var targets = new List<BattleUnitsStack>();
            Console.WriteLine("Possible targets:");
            foreach (var target in targetArmy.unitsStackList) {
                if (cast.Applicable(target)) {
                    Console.WriteLine($"{++index} - {target}");
                    targets.Add(target);
                }
            }

            Console.WriteLine("Choose the target:");
            int chosenTarget = ConsoleUI.GetNumericOption(1, index);

            Action.Cast(cast, stack, targets[chosenTarget-1]);
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