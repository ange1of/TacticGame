using System;

namespace game {

    class Player {
        public Player(Army _army) {
            army = new Army(_army);
        }
        public Player(Player otherPlayer) {
            army = new Army(otherPlayer.army);
        }

        public Army army { get; }
    }

}