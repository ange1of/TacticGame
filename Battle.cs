using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace game {

    class Battle {
        public Battle(Player _firstPlayer, Player _secondPlayer) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;

            firstPlayerArmy = new BattleArmy(firstPlayer.army);
            secondPlayerArmy = new BattleArmy(secondPlayer.army);
            intitativeScale = new Initiative(firstPlayerArmy, secondPlayerArmy);
        }

        public void Start() {
            roundNumber = 0;
            winner = null;
            while (winner == null) {
                ConsoleUI.PrintLine($"ROUND {++roundNumber}");
                while (intitativeScale.NextStack() != null) {
                    ConsoleUI.PrintLine($"{firstPlayer.nickname}'s army:");
                    ConsoleUI.PrintList("\t", firstPlayerArmy.unitsStackList);
                    
                    ConsoleUI.Print("\n");

                    ConsoleUI.PrintLine($"{secondPlayer.nickname}'s army:");
                    ConsoleUI.PrintList("\t", secondPlayerArmy.unitsStackList);

                    ConsoleUI.Print("\n");
                    ConsoleUI.PrintBlock(PrintScale());

                    NextTurn(intitativeScale.NextStack());
                    if (winner != null) {
                        End();
                        return;
                    }
                }
                NewRound();
            }
        }

        private void NewRound() {
            roundNumber++;
            foreach (var stack in firstPlayerArmy.unitsStackList.Concat(secondPlayerArmy.unitsStackList)) {
                stack.NewRound();
            }
            intitativeScale.NewRound();
        }

        private void NextTurn(BattleUnitsStack stack) {
            List<ICast> possibleCasts = stack.Casts.Where(c => !c.hasBeenCasted).ToList();

            ConsoleUI.PrintLine($"Turn of {(intitativeScale.NextStack().parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}'s {intitativeScale.NextStack().unitsType.type}");

            if (possibleCasts.Count > 0) {
                ConsoleUI.PrintLine($"({string.Join(", ", possibleCasts.Select(x => x.type))})");
            }

            ConsoleUI.PrintLine("Possibe actions:");

            if (stack.state == State.NotMadeMove) {

                ConsoleUI.PrintNumericList(1, "Attack", "Cast", "Wait", "Defend", "Surrender");

                int chosenAction = ConsoleUI.GetNumericOption(1, 5);

                if (possibleCasts.Count == 0) {
                    while (chosenAction == 2) {
                        ConsoleUI.PrintLine("No possible casts to use");
                        chosenAction = ConsoleUI.GetNumericOption(1, 5);
                    }
                }

                BattleArmy opponentArmy;
                int index = 0;
                var targets = new List<BattleUnitsStack>();
                int chosenTarget = 0;
                int chosenCast = 0;
                switch (chosenAction) {
                    case 1:
                        ConsoleUI.PrintLine("Possible targets:");
                        if (stack.parentArmy == firstPlayerArmy) {
                            opponentArmy = secondPlayerArmy;
                        }
                        else {
                            opponentArmy = firstPlayerArmy;
                        }
                        index = 0;
                        targets = new List<BattleUnitsStack>();
                        foreach (var target in opponentArmy.unitsStackList) {
                            if (target.unitsCount > 0) {
                                ConsoleUI.Print($"{++index} - {target} (");
                                foreach (var mod in target.GetModifiers()) {
                                    ConsoleUI.Print($"{mod}");
                                }
                                ConsoleUI.PrintLine(")");
                                targets.Add(target);
                            }
                        }

                        ConsoleUI.PrintLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
                        index = 0;
                        foreach (var cast in possibleCasts) {
                            ConsoleUI.PrintLine($"{++index} - {cast.type}");
                        }
                        chosenCast = ConsoleUI.GetNumericOption(1, index);
                        
                        var concreteCast = possibleCasts[chosenCast-1];

                        if (stack.parentArmy == firstPlayerArmy) {
                            opponentArmy = secondPlayerArmy;
                        }
                        else {
                            opponentArmy = firstPlayerArmy;
                        }

                        BattleArmy targetArmy = opponentArmy;
                        if (concreteCast is IFriendCast) {
                            targetArmy = stack.parentArmy;
                        }

                        index = 0;
                        targets = new List<BattleUnitsStack>();
                        ConsoleUI.PrintLine("Possible targets:");
                        foreach (var target in targetArmy.unitsStackList) {
                            if ((target.unitsCount > 0 && !(concreteCast is CResurrection)) || (concreteCast is CResurrection && target.ressurectable)) {
                                ConsoleUI.Print($"{++index} - {target} (");
                                foreach (var mod in target.GetModifiers()) {
                                    ConsoleUI.Print($"{mod}");
                                }
                                ConsoleUI.PrintLine(")");
                                targets.Add(target);
                            }
                        }

                        ConsoleUI.PrintLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Cast(concreteCast, stack, targets[chosenTarget-1]);
                        break;
                    case 3:
                        Action.Wait(stack);
                        break;
                    case 4:
                        Action.Defend(stack);
                        break;
                    case 5:
                        Surrender(stack);
                        break;
                    default:
                        break;
                }
            }
            else if (stack.state == State.Awaiting) {
                ConsoleUI.PrintNumericList(1, "Attack", "Cast", "Defend", "Surrender");

                int chosenAction = ConsoleUI.GetNumericOption(1, 4);
                
                if (possibleCasts.Count == 0) {
                    while (chosenAction == 2) {
                        ConsoleUI.PrintLine("No possible casts to use");
                        chosenAction = ConsoleUI.GetNumericOption(1, 4);
                    }
                }

                BattleArmy opponentArmy;
                int index = 0;
                var targets = new List<BattleUnitsStack>();
                int chosenTarget = 0;
                int chosenCast = 0;
                switch (chosenAction) {
                    case 1:
                        ConsoleUI.PrintLine("Possible targets:");
                        if (stack.parentArmy == firstPlayerArmy) {
                            opponentArmy = secondPlayerArmy;
                        }
                        else {
                            opponentArmy = firstPlayerArmy;
                        }
                        index = 0;
                        targets = new List<BattleUnitsStack>();
                        foreach (var target in opponentArmy.unitsStackList) {
                            if (target.unitsCount > 0) {
                                ConsoleUI.Print($"{++index} - {target} (");
                                foreach (var mod in target.GetModifiers()) {
                                    ConsoleUI.Print($"{mod}");
                                }
                                ConsoleUI.PrintLine(")");
                                targets.Add(target);
                            }
                        }

                        ConsoleUI.PrintLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
                        ConsoleUI.PrintLine("Possible targets:");
                        if (stack.parentArmy == firstPlayerArmy) {
                            opponentArmy = secondPlayerArmy;
                        }
                        else {
                            opponentArmy = firstPlayerArmy;
                        }

                        index = 0;
                        targets = new List<BattleUnitsStack>();
                        foreach (var target in opponentArmy.unitsStackList) {
                            if (target.unitsCount > 0) {
                                ConsoleUI.Print($"{++index} - {target} (");
                                foreach (var mod in target.GetModifiers()) {
                                    ConsoleUI.Print($"{mod}");
                                }
                                ConsoleUI.PrintLine(")");
                                targets.Add(target);
                            }
                        }

                        ConsoleUI.PrintLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        // Action.Cast(stack);
                        break;
                    case 3:
                        Action.Defend(stack);
                        break;
                    case 4:
                        Surrender(stack);
                        break;
                    default: 
                        break;
                }
            }
            intitativeScale.UpdateScale();
            CheckWinner();
        }

        private void Surrender(BattleUnitsStack stack) {
            if (stack.parentArmy == firstPlayerArmy) {
                winner = secondPlayerArmy;
            }
            else if (stack.parentArmy == secondPlayerArmy) {
                winner = firstPlayerArmy;
            }
        }

        private void CheckWinner() {
            if (!firstPlayerArmy.isAlive) {
                winner = secondPlayerArmy;
            }
            else if (!secondPlayerArmy.isAlive) {
                winner = firstPlayerArmy;
            }
        }

        private void End() {
            if (winner == firstPlayerArmy) {
                ConsoleUI.PrintLine($"Player {firstPlayer.nickname} WINS!");
            }
            else {
                ConsoleUI.PrintLine($"Player {secondPlayer.nickname} WINS!");
            }
            ConsoleUI.PrintLine($"Armies:\n" +
            $"{firstPlayer.nickname}: {string.Join(", ", firstPlayerArmy.unitsStackList.Where(x => x.unitsCount != 0))}" + "\n" + 
            $"{secondPlayer.nickname}: {string.Join(", ", secondPlayerArmy.unitsStackList.Where(x => x.unitsCount != 0))}"
            );

            firstPlayer.UpdateArmy(firstPlayerArmy);
            secondPlayer.UpdateArmy(secondPlayerArmy);
        }

        private string PrintScale() {
            string result = "Scale: ";
            intitativeScale.GetMainScale().ForEach(x => result += $"{x.Item1}({x.Item2}) ");
            result += "| ";
            intitativeScale.GetAwaitingScale().ForEach(x => result += $"{x.Item1}({x.Item2}) ");
            result += " |  ";
            intitativeScale.GetNextScale().ForEach(x => result += $"{x.Item1}({x.Item2}) ");
            return result;
        }

        public BattleArmy winner { get; private set; }
        public int roundNumber { get; private set; }
        private Initiative intitativeScale { get; }
        private Player firstPlayer;
        private Player secondPlayer;
        private BattleArmy firstPlayerArmy;
        private BattleArmy secondPlayerArmy;
    }

}