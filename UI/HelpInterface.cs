using System;

namespace game {
    public class HelpInterface : UIStep {
        public HelpInterface(UIStep _prevStep) {
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("HELP\n");

            ConsoleUI.PrintNumericList(1, "Common mechanics", "Units", "Initiative", "Casts", "Modifiers", "Effects", "Back");

            return ConsoleUI.GetNumericOption(1, 7);
        }

        public override UIStep NextStep(int option) {
            UIStep nextStep = null;
            switch (option) {
                case 1:
                    nextStep = new CommonMechanicsInterface(this);
                    break;
                case 2:
                    nextStep = new UnitsHelpInterface(this);
                    break;
                case 3:
                    nextStep = new InitiativeHelpInterface(this);
                    break;
                case 4:
                    nextStep = new CastHelpInterface(this);
                    break;
                case 5:
                    nextStep = new ModifiersHelpInterface(this);
                    break;
                case 6:
                    nextStep = new EffectHelpInterface(this);
                    break;
                case 7:
                    nextStep = prevStep;
                    break;
                default:
                    nextStep = this;
                    break;
            }
            return nextStep;
        }

        private UIStep prevStep;
    }
}