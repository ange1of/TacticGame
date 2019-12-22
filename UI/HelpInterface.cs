using System;

namespace game {
    public class HelpInterface : UIStep {
        public HelpInterface(UIStep _prevStep) {
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("\nHelp");

            return 0;
        }

        public override UIStep NextStep(int option) {
            return null;
        }

        private UIStep prevStep;
    }
}