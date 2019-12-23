using System;

namespace game {
    public class EffectStatsInterface : UIStep {
        public EffectStatsInterface(BaseEffect _effect, UIStep _prevStep) {
            prevStep = _prevStep;
            effect = _effect;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine($"Type: {effect.type}");
            Console.WriteLine($"\n{effect.description}\n");
            ConsoleUI.PrintNumericList(1, "Back");

            return ConsoleUI.GetNumericOption(1, 1);
        }

        public override UIStep NextStep(int option) {
            return prevStep;
        }

        private UIStep prevStep;
        private BaseEffect effect;
    }
}