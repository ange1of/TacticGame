using System;

namespace game {

    public class InitialInterface : UIStep {
        public InitialInterface(Player _firstPlayer, Player _secondPlayer) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;
        }
        public override int Actions() {
            ConsoleUI.NewScreen(0);
            ConsoleUI.PrintNumericList(1, "Start creating players", "Help");
            return ConsoleUI.GetNumericOption(1, 2);
        }

        public override UIStep NextStep(int option) {
            if (option == 1) {
                return new FirstPlayerInterface(firstPlayer, secondPlayer, this);
            }
            else {
                return null;
            }
        }

        private Player firstPlayer;
        private Player secondPlayer;
    }

}