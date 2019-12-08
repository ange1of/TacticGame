using System.Reflection;
using System;
using System.IO;

namespace game {

    public class Player {
        public Player(string _nickname) {
            army = new Army();
            nickname = _nickname;
        }

        public Player(Army _army, string _nickname) {
            army = new Army(_army);
            nickname = _nickname;
        }
        public Player(Player otherPlayer) {
            army = new Army(otherPlayer.army);
            nickname = otherPlayer.nickname;
        }

        public void UpdateArmy(BattleArmy battleArmy) {
            army = new Army(battleArmy);
        }

        public void MakeNewArmy() {
            // int stacksCount = 0;
            // ConsoleUI.PrintNumericList(1, "Add stack", "End");
            // int option = ConsoleUI.GetNumericOption(1, 2);
            // while (option != 2 && stacksCount != int.Parse(Config.GetValue("Army:MAXSIZE"))) {
            //     stacksCount++;
            //     Console.WriteLine("Choose type:");
                
                
            // }
        }

        public string nickname { get; }
        public Army army { get; private set; }
    }

}