using System;
using System.Collections.Generic;
using System.Linq;

namespace game {
    public class CastHelpInterface : UIStep {
        public CastHelpInterface(UIStep _prevStep) {
            prevStep = _prevStep;
            types = new List<Type>(ModHandler.GetAllCasts());
            typesCount = types.Count;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("Choose type:");
            ConsoleUI.PrintNumericList(1, types.Select(x => x.Name));
            Console.WriteLine($"\n{typesCount + 1}: Back");

            return ConsoleUI.GetNumericOption(1, typesCount+1);
        }

        public override UIStep NextStep(int option) {
            if (option == typesCount + 1) {
                return prevStep;
            }
            else {
                return new CastStatsInterface((ICast) Activator.CreateInstance(types[option-1]), this);
            }
        }

        private List<Type> types;
        private UIStep prevStep;
        private int typesCount = 0;
    }
}