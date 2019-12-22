using System;
using System.Text.RegularExpressions;

namespace game {
    class SecondPlayerInterface : UIStep {
        public SecondPlayerInterface(Player _firstPlayer, Player _secondPlayer, UIStep _prevStep) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("Second Player");
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

                secondPlayer.SetNickname(nick);
                return new SecondPlayerArmyInterface(firstPlayer, secondPlayer, this);
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