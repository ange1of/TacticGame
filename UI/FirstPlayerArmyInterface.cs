using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace game {
    class FirstPlayerArmyInterface : UIStep {
        public FirstPlayerArmyInterface(Player _firstPlayer, Player _secondPlayer, UIStep _prevStep) {
            firstPlayer = _firstPlayer;
            secondPlayer = _secondPlayer;
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine($"{firstPlayer.nickname}'s army:");
            ConsoleUI.PrintList(firstPlayer.army.unitsStackList, "\t");

            if (firstPlayer.army.unitsStackList.Count < int.Parse(Config.GetValue("Army:MAXSIZE"))) {
                ConsoleUI.PrintNumericList(1, "Add stack", "Delete stack", "Complete creation", "Clear army", "Back");
                return ConsoleUI.GetNumericOption(1, 5);
            }
            else {
                ConsoleUI.PrintNumericList(1, "Delete stack", "Complete creation", "Clear army", "Back");
                return ConsoleUI.GetNumericOption(1, 4) + 1;
            }
            
        }

        public override UIStep NextStep(int option) {
            UIStep nextStep = this;

            switch (option) {
                case 1:
                    List<Type> types = new List<Type>(ModHandler.GetAllUnits());
                    int typesCount = types.Count;

                    Console.WriteLine("Choose type:");
                    ConsoleUI.PrintNumericList(1, types.Select(x => x.Name));
                    int typeNumber = ConsoleUI.GetNumericOption(1, typesCount);

                    Console.Write("Choose count: ");
                    int count = ConsoleUI.GetNumericOption(0, int.Parse(Config.GetValue("UnitsStack:MAXSIZE")));

                    Unit instance = (Unit) (types[typeNumber-1]).GetProperty("Instance").GetValue(null, null);
                    firstPlayer.army.AddStack(new UnitsStack(instance, (uint)count));

                    break;

                case 2:
                    ConsoleUI.PrintNumericList(1, firstPlayer.army.unitsStackList.Select(x => x.ToString()));
                    Console.Write("Choose nubmer: ");
                    int stackNumber = ConsoleUI.GetNumericOption(1, firstPlayer.army.unitsStackList.Count);

                    firstPlayer.army.RemoveStack(firstPlayer.army.unitsStackList[stackNumber-1]);
                    break;
                case 3:
                    nextStep = new SecondPlayerInterface(firstPlayer, secondPlayer, this);
                    break;
                case 4:
                    firstPlayer.army.Clear();
                    break;
                case 5:
                    nextStep = prevStep;
                    break;
            }
            return nextStep;
        }

        private Player firstPlayer;
        private Player secondPlayer;
        private UIStep prevStep;
    }
}