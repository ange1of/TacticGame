using System;

namespace game {
    public class BattleStartInterface : UIStep {
        public BattleStartInterface(Player _firstPlayer, Player _secondPlayer) {
            battle = new Battle(_firstPlayer, _secondPlayer);
            battle.Start();
        }

        public override int Actions() {
            return 0;
        }

        public override UIStep NextStep(int option) {
            if (battle.NextStack() != null) {
                return new TurnInterface(battle);
            }
            return null;
        }

        private Battle battle;
    }
}