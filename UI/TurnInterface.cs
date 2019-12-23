using System;
using System.Collections.Generic;
using System.Linq;

namespace game {
    public class TurnInterface: UIStep {
        public TurnInterface(Battle _battle) {
            battle = _battle;
        } 

        public override int Actions() {
            ConsoleUI.NewScreen();
            battle.intitativeScale.UpdateScale();
            battle.NextTurn();
            stack = battle.NextStack();

            if (battle.winner != null) {
                return -1;
            }

            if (stack == null) {
                battle.NewRound();
                stack = battle.NextStack();
                if (stack == null) {
                    return -1;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"ROUND {battle.roundNumber}");
            Console.ResetColor();

            Console.WriteLine($"{battle.firstPlayer.nickname}'s army:");
            ConsoleUI.PrintList(battle.firstPlayerArmy.unitsStackList, "\t");
            Console.WriteLine();
            Console.WriteLine($"{battle.secondPlayer.nickname}'s army:");
            ConsoleUI.PrintList(battle.secondPlayerArmy.unitsStackList, "\t");
            Console.WriteLine();
            Console.WriteLine(battle.PrintScale());
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Turn of {(battle.intitativeScale.NextStack().parentArmy == battle.firstPlayerArmy ? battle.firstPlayer.nickname : battle.secondPlayer.nickname)}'s {battle.intitativeScale.NextStack().unitsType.type}");
            Console.ResetColor();

            List<ICast> possibleCasts = battle.intitativeScale.NextStack()?.Casts.Where(c => !c.hasBeenCasted).ToList();
            if (possibleCasts.Count > 0) {
                Console.WriteLine($"({string.Join(", ", possibleCasts.Select(x => x.type))})");
            }
            else {
                Console.WriteLine("(No available casts)");
            }
            Console.WriteLine("Possible actions:");
            
            if (stack.state == State.NotMadeMove) {
                if (possibleCasts.Count > 0) {
                    ConsoleUI.PrintNumericList(1, "Attack", "Cast", "Wait", "Defend", "Surrender", "Help", "Show stats");
                    int option = ConsoleUI.GetNumericOption(1, 7);
                    return option;
                }
                else {
                    ConsoleUI.PrintNumericList(1, "Attack", "Wait", "Defend", "Surrender", "Help", "Show stats");
                    int option = ConsoleUI.GetNumericOption(1, 6);
                    return (option > 1) ? option + 1 : option;
                }
            }
            else if (stack.state == State.Awaiting) {
                if (possibleCasts.Count > 0) {
                    ConsoleUI.PrintNumericList(1, "Attack", "Cast", "Defend", "Surrender", "Help", "Show stats");
                    int option = ConsoleUI.GetNumericOption(1, 6);
                    return (option > 2) ? option + 1 : option;
                }
                else {
                    ConsoleUI.PrintNumericList(1, "Attack", "Defend", "Surrender", "Help", "Show stats");
                    int option = ConsoleUI.GetNumericOption(1, 5);
                    return (option > 1) ? option + 2 : option;
                }
            }
            return 0;
        }

        public override UIStep NextStep(int option) {
            UIStep nextStep = null;
            switch (option) {
                // 1 - "Attack", 2 - "Cast", 3 - "Wait", 4 - "Defend", 5 - "Surrender", 6 - "Help", 7 - Show stats
                case 1:
                    nextStep = new AttackInterface(battle, stack);
                    break;
                case 2:
                    nextStep = new CastInterface(battle, stack);
                    break;
                case 3:
                    Action.Wait(stack);
                    nextStep = this;
                    break;
                case 4:
                    Action.Defend(stack);
                    nextStep = this;
                    break;
                case 5:
                    nextStep = new EndGameInterface(battle, stack);
                    break;
                case 6:
                    nextStep = new HelpInterface(this);
                    break;
                case 7:
                    nextStep = new UnitStatsInterface(stack.unitsType, this);
                    break;
                default:
                    nextStep = new EndGameInterface(battle);
                    break;
            }
            return nextStep;
        }

        private Battle battle;
        private BattleUnitsStack stack;
    }
}