using System;
using System.Text.RegularExpressions;

namespace game {
    class FirstPlayerInterface : UIStep {
        public FirstPlayerInterface(Player _firstPlayer, Player _secondPlayer, UIStep _prevStep) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("First Player");
            ConsoleUI.PrintNumericList(1, "Enter nickname", "Back");
            return ConsoleUI.GetNumericOption(1, 2);
        }

        public override UIStep NextStep(int option) {
            if (option == 1) {
                Console.Write("Nickname: ");
                string nick = Console.ReadLine();
                while (nick == "" || nick.Contains(' ')) {
                    Console.Write("Invalid characters, enter new nickname: ");
                    nick = Console.ReadLine();
                }

                firstPlayer.SetNickname(nick);
                return new FirstPlayerArmyInterface(firstPlayer, secondPlayer, this);
            }
            else {
                return prevStep;
            }
        }

        private Player firstPlayer;
        private Player secondPlayer;
        private UIStep prevStep;
    }
}