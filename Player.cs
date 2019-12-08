using System.Reflection;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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
            int stacksCount = 0;
            ConsoleUI.PrintNumericList(1, "Add stack", "End");
            int option = ConsoleUI.GetNumericOption(1, 2);

            var unitTypes = new List<Type>(ModHandler.GetAllUnits());
            int typesCount = unitTypes.Count;

            int maxSize = (int)Army.MAXSIZE;
            while (option != 2 && stacksCount < maxSize) {
                stacksCount++;
                Console.WriteLine("Choose type:");
                ConsoleUI.PrintNumericList(1, unitTypes.Select(x => x.Name));
                int typeNumber = ConsoleUI.GetNumericOption(1, typesCount);

                Console.Write("Choose count: ");
                int count = ConsoleUI.GetNumericOption(0, int.Parse(Config.GetValue("UnitsStack:MAXSIZE")));

                
                Unit instance = (Unit) (unitTypes[typeNumber-1]).GetProperty("Instance").GetValue(null, null);
                army.AddStack(new UnitsStack(instance, (uint)count));

                if (stacksCount < maxSize) {
                    ConsoleUI.PrintNumericList(1, "Add stack", "End");
                    option = ConsoleUI.GetNumericOption(1, 2);
                }
            }

            Console.WriteLine($"{nickname}'s army:");
            ConsoleUI.PrintList(army.unitsStackList, "\t");
        }

        public string nickname { get; }
        public Army army { get; private set; }
    }

}