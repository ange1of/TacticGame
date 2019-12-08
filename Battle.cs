using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using BaseEntities;

namespace game {

    public class Battle {
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
                Console.WriteLine($"ROUND {++roundNumber}");
                while (intitativeScale.NextStack() != null) {
                    Console.WriteLine($"{firstPlayer.nickname}'s army:");
                    ConsoleUI.PrintList(firstPlayerArmy.unitsStackList, "\t");
                    
                    Console.Write("\n");

                    Console.WriteLine($"{secondPlayer.nickname}'s army:");
                    ConsoleUI.PrintList(secondPlayerArmy.unitsStackList, "\t");

                    Console.Write("\n");
                    Console.WriteLine(PrintScale());

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

            Console.WriteLine($"Turn of {(intitativeScale.NextStack().parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}'s {intitativeScale.NextStack().unitsType.type}");

            if (possibleCasts.Count > 0) {
                Console.WriteLine($"({string.Join(", ", possibleCasts.Select(x => x.type))})");
            }

            Console.WriteLine("Possibe actions:");

            if (stack.state == State.NotMadeMove) {

                ConsoleUI.PrintNumericList(1, "Attack", "Cast", "Wait", "Defend", "Surrender");

                int chosenAction = ConsoleUI.GetNumericOption(1, 5);

                if (possibleCasts.Count == 0) {
                    while (chosenAction == 2) {
                        Console.WriteLine("No possible casts to use");
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
                        Console.WriteLine("Possible targets:");
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
                                Console.WriteLine($"{++index} - {target}");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
                        index = 0;
                        foreach (var cast in possibleCasts) {
                            Console.WriteLine($"{++index} - {cast.type}");
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
                        Console.WriteLine("Possible targets:");
                        foreach (var target in targetArmy.unitsStackList) {
                            if ((target.unitsCount > 0 && !(concreteCast is CResurrection)) || (concreteCast is CResurrection && target.ressurectable)) {
                                Console.WriteLine($"{++index} - {target}");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
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
                        Console.WriteLine("No possible casts to use");
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
                        Console.WriteLine("Possible targets:");
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
                                Console.WriteLine($"{++index} - {target}");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
                        index = 0;
                        foreach (var cast in possibleCasts) {
                            Console.WriteLine($"{++index} - {cast.type}");
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
                        Console.WriteLine("Possible targets:");
                        foreach (var target in targetArmy.unitsStackList) {
                            if ((target.unitsCount > 0 && !(concreteCast is CResurrection)) || (concreteCast is CResurrection && target.ressurectable)) {
                                Console.WriteLine($"{++index} - {target}");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = ConsoleUI.GetNumericOption(1, index);

                        Action.Cast(concreteCast, stack, targets[chosenTarget-1]);
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
                Console.WriteLine($"Player {firstPlayer.nickname} WINS!");
            }
            else {
                Console.WriteLine($"Player {secondPlayer.nickname} WINS!");
            }
            Console.WriteLine($"Armies:\n\t{firstPlayer.nickname}:");
            ConsoleUI.PrintList(firstPlayerArmy.unitsStackList.Select(x => $"{x.unitsType.type}: {x.unitsCount}"), "\t\t");
            Console.WriteLine($"\t{secondPlayer.nickname}: ");
            ConsoleUI.PrintList(secondPlayerArmy.unitsStackList.Select(x => $"{x.unitsType.type}: {x.unitsCount}"), "\t\t");

            firstPlayer.UpdateArmy(firstPlayerArmy);
            secondPlayer.UpdateArmy(secondPlayerArmy);
        }

        private string PrintScale() {
            string result = "Scale: ";
            intitativeScale.GetMainScale().ForEach(x => result += $"({(x.Item1.parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}){x.Item1.unitsType.type}({x.Item2}) ");
            result += "| ";
            intitativeScale.GetAwaitingScale().ForEach(x => result += $"({(x.Item1.parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}){x.Item1.unitsType.type}({x.Item2}) ");
            result += " |  ";
            intitativeScale.GetNextScale().ForEach(x => result += $"({(x.Item1.parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}){x.Item1.unitsType.type}({x.Item2}) ");
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