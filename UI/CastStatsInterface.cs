using System;

namespace game {
    public class CastStatsInterface : UIStep {
        public CastStatsInterface(ICast _cast, UIStep _prevStep) {
            prevStep = _prevStep;
            cast = _cast;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine($"Type: {cast.type}");
            Console.WriteLine($"\n{cast.description}\n");
            ConsoleUI.PrintNumericList(1, "Back");

            return ConsoleUI.GetNumericOption(1, 1);
        }

        public override UIStep NextStep(int option) {
            return prevStep;
        }

        private UIStep prevStep;
        private ICast cast;
    }
}