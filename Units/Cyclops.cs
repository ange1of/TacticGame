using System;

namespace game {
    class Cyclops : Unit {
        private static Cyclops instance;

        private Cyclops() : base("Cyclops", 85, 20, 15, (18, 26), 10) {}

        public static Cyclops Instance {
            get {
                if (instance == null) {
                    instance = new Cyclops();
                }
                return instance;
            }
        }
    }
}