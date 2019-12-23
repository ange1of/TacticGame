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
            roundNumber = 1;
            winner = null;
        }

        public void NewRound() {
            roundNumber++;
            foreach (var stack in firstPlayerArmy.unitsStackList.Concat(secondPlayerArmy.unitsStackList)) {
                stack.NewRound();
            }
            intitativeScale.NewRound();
        }

        public BattleUnitsStack NextStack() {
            return intitativeScale.NextStack();
        }

        public void NextTurn() {
            intitativeScale.UpdateScale();
            CheckWinner();
        }

        public void Surrender(BattleUnitsStack stack) {
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

        public void UpdateArmies() {
            firstPlayer.UpdateArmy(firstPlayerArmy);
            secondPlayer.UpdateArmy(secondPlayerArmy);
        }

        public string PrintScale() {
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
        public Initiative intitativeScale { get; }
        public Player firstPlayer { get; }
        public Player secondPlayer { get; }
        public BattleArmy firstPlayerArmy { get; }
        public BattleArmy secondPlayerArmy { get; }
    }

}