using System;

namespace game {

    class Player {
        public Player(Army _army, string _nickname) {
            army = new Army(_army);
            nickname = _nickname;
        }
        public Player(Player otherPlayer) {
            army = new Army(otherPlayer.army);
            nickname = otherPlayer.nickname;
        }

        public void UpdateArmy(BattleArmy battleArmy) {
            army = new Army(battleArmy);
        }

        public string nickname { get; }
        public Army army { get; private set; }
    }

}