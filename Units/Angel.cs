using System;

namespace game {
    class Angel : Unit {
        private static Angel instance;

        private Angel() : base("angel", 180, 27, 27, (45, 45), 11) {}

        public static Angel Instance {
            get {
                if (instance == null) {
                    instance = new Angel();
                }
                return instance;
            }
        }
    }
}