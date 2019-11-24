using System;
using System.Linq;
using System.Collections.Generic;

namespace game {

    class Battle {
        public Battle(Player _firstPlayer, Player _secondPlayer) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;

            firstPlayerArmy = new BattleArmy(firstPlayer.army);
            secondPlayerArmy = new BattleArmy(secondPlayer.army);

            intitativeScale = new Initiative(firstPlayerArmy, secondPlayerArmy);
            roundNumber = 0;
            winner = null;
        }

        public void Start() {
            while (winner == null) {
                while (intitativeScale.NextStack() != null) {
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
            Console.WriteLine($"Round {roundNumber}");
            foreach (var stack in firstPlayerArmy.unitsStackList.Concat(secondPlayerArmy.unitsStackList)) {
                stack.NewRound();
            }
            intitativeScale.NewRound();
        }

        private void NextTurn(BattleUnitsStack stack) {

            Console.WriteLine($"Turn of {(stack.parentArmy == firstPlayerArmy ? firstPlayer.nickname : secondPlayer.nickname)}'s {stack.unitsType.type}");
            Console.WriteLine("Possibe actions:");

            if (stack.state == State.NotMadeMove) {
                Console.WriteLine($"1 - Attack\n2 - Cast\n3 - Wait\n4 - Defend\n5 - Surrender");

                int chosenAction = 0;
                int.TryParse(Console.ReadLine(), out chosenAction);
                while (!(chosenAction >= 1 && chosenAction <= 5)) {
                    Console.WriteLine("Incorrect action number, try again");
                    int.TryParse(Console.ReadLine(), out chosenAction);
                }

                BattleArmy opponentArmy;
                int index = 0;
                var targets = new List<BattleUnitsStack>();
                int chosenTarget = 0;
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
                                Console.Write($"{++index} - {target} (");
                                foreach (var mod in target.Modifiers.Keys) {
                                    Console.Write($"{(IModifier)mod}");
                                }
                                Console.WriteLine(")");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = 0;
                        int.TryParse(Console.ReadLine(), out chosenTarget);
                        while (!(chosenTarget >= 1 && chosenTarget <= index)) {
                            Console.WriteLine("Incorrect action number, try again");
                            int.TryParse(Console.ReadLine(), out chosenAction);
                        }

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
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
                                Console.Write($"{++index} - {target} (");
                                foreach (var mod in target.Modifiers.Keys) {
                                    Console.Write($"{(IModifier)mod}");
                                }
                                Console.WriteLine(")");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = 0;
                        int.TryParse(Console.ReadLine(), out chosenTarget);
                        while (!(chosenTarget >= 1 && chosenTarget <= index)) {
                            Console.WriteLine("Incorrect action number, try again");
                            int.TryParse(Console.ReadLine(), out chosenAction);
                        }

                        Action.Cast(stack);
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
                Console.WriteLine($"1 - Attack\n2 - Cast\n3 - Defend\n4 - Surrender");

                int chosenAction = 0;
                int.TryParse(Console.ReadLine(), out chosenAction);
                while (!(chosenAction == 1 || chosenAction == 2 || chosenAction == 3 || chosenAction == 4)) {
                    Console.WriteLine("Incorrect action number, try again");
                    int.TryParse(Console.ReadLine(), out chosenAction);
                }
                                BattleArmy opponentArmy;
                int index = 0;
                var targets = new List<BattleUnitsStack>();
                int chosenTarget = 0;
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
                                Console.Write($"{++index} - {target} (");
                                foreach (var mod in target.Modifiers.Keys) {
                                    Console.Write($"{(IModifier)mod}");
                                }
                                Console.WriteLine(")");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = 0;
                        int.TryParse(Console.ReadLine(), out chosenTarget);
                        while (!(chosenTarget >= 1 && chosenTarget <= index)) {
                            Console.WriteLine("Incorrect action number, try again");
                            int.TryParse(Console.ReadLine(), out chosenAction);
                        }

                        Action.Attack(stack, targets[chosenTarget-1]);
                        break;
                    case 2:
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
                                Console.Write($"{++index} - {target} (");
                                foreach (var mod in target.Modifiers.Keys) {
                                    Console.Write($"{(IModifier)mod}");
                                }
                                Console.WriteLine(")");
                                targets.Add(target);
                            }
                        }

                        Console.WriteLine("Choose the target:");
                        chosenTarget = 0;
                        int.TryParse(Console.ReadLine(), out chosenTarget);
                        while (!(chosenTarget >= 1 && chosenTarget <= index)) {
                            Console.WriteLine("Incorrect action number, try again");
                            int.TryParse(Console.ReadLine(), out chosenAction);
                        }

                        Action.Cast(stack);
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
            Console.WriteLine($"Armies:\n" +
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