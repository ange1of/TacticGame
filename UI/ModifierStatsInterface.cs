using System;

namespace game {
    public class ModifierStatsInterface : UIStep {
        public ModifierStatsInterface(IModifier _modifier, UIStep _prevStep) {
            prevStep = _prevStep;
            modifier = _modifier;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine($"Type: {modifier.type}");
            Console.WriteLine($"\n{modifier.description}\n");
            ConsoleUI.PrintNumericList(1, "Back");

            return ConsoleUI.GetNumericOption(1, 1);
        }

        public override UIStep NextStep(int option) {
            return prevStep;
        }

        private UIStep prevStep;
        private IModifier modifier;
    }
}