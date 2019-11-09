using System;

namespace game {
    class Shaman : Unit {
        private static Shaman instance;

        private Shaman() : base("Shaman", 40, 12, 10, (7, 12), 10.5) {}

        public static Shaman Instance {
            get {
                if (instance == null) {
                    instance = new Shaman();
                }
                return instance;
            }
        }
    }
}