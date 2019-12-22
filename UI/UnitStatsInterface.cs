using System;

namespace game {
    public class UnitStatsInterface : UIStep {
        public UnitStatsInterface(Unit _unit, UIStep _prevStep) {
            prevStep = _prevStep;
            unit = _unit;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine(unit);
            Console.WriteLine();
            ConsoleUI.PrintNumericList(1, "Back");

            return ConsoleUI.GetNumericOption(1, 1);
        }

        public override UIStep NextStep(int option) {
            return prevStep;
        }

        private UIStep prevStep;
        private Unit unit;
    }
}