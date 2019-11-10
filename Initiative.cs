using System;
using System.Collections.Generic;
using System.Linq;

namespace game {

    class Initiative {
        public Initiative(BattleArmy _firstArmy, BattleArmy _secondArmy) {
            firstArmy = new BattleArmy(_firstArmy);
            secondArmy = new BattleArmy(_secondArmy);
            
            allStacks = firstArmy.unitsStackList.Concat(secondArmy.unitsStackList).ToList();
        }

        // Пока не работает
        public BattleUnitsStack GetNextStep() {
            return (allStacks.Count != 0) ? allStacks[0] : null;
        }

        // Пока не работает
        public BattleArmy GetWinner() {
            return firstArmy;
        }

        private List<BattleUnitsStack> allStacks;
        private BattleArmy firstArmy { get; }
        private BattleArmy secondArmy { get; }
    }

}