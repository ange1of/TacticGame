using System;

namespace game {

    class Battle {
        public Battle(Player _firstPlayer, Player _secondPlayer) {
            firstPlayer = new Player(_firstPlayer);
            secondPlayer = new Player(_secondPlayer);
            intitativeScale = new Initiative(new BattleArmy(firstPlayer.army), new BattleArmy(secondPlayer.army));
        }

        public Initiative intitativeScale { get; }
        public Player firstPlayer { get; }
        public Player secondPlayer { get; }
    }

}