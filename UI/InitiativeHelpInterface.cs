using System;

namespace game {
    public class InitiativeHelpInterface : UIStep {
        public InitiativeHelpInterface(UIStep _prevStep) {
            prevStep = _prevStep;
        }

        public override int Actions() {
            ConsoleUI.NewScreen();
            Console.WriteLine("Initiative scale\n");

            Console.WriteLine("Шкала инициативы - очередность ходов юнитов (стеков). Очередность определяется параметром “Инициатива”. Стоит учесть, что этот параметр может меняться в ходе сражения, следовательно и шкала инициативы должна обновляться в соответствии с этими изменениями. Также необходимо корректно отработать действие “Ожидать”: стек должен переместиться в конец шкалы в текущем раунде. Если несколько стеков ожидают, то их очередность в конце шкалы обратна изначальной очередности\n");

            ConsoleUI.PrintNumericList(1, "Back");
            ConsoleUI.GetNumericOption(1, 1);
            
            return 0;
        }

        public override UIStep NextStep(int option) {
            return prevStep;
        }

        private UIStep prevStep;
    }
}