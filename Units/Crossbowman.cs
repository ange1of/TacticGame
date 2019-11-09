using System;

namespace game {
    class Crossbowman : Unit {
        private static Crossbowman instance;

        private Crossbowman() : base("Crossbowman", 10, 4, 4, (2, 8), 8) {}

        public static Crossbowman Instance {
            get {
                if (instance == null) {
                    instance = new Crossbowman();
                }
                return instance;
            }
        }
    }
}