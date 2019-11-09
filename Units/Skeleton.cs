using System;

namespace game {
    class Skeleton : Unit {
        private static Skeleton instance;

        private Skeleton() : base("Skeleton", 5, 1, 2, (1, 1), 10) {}

        public static Skeleton Instance {
            get {
                if (instance == null) {
                    instance = new Skeleton();
                }
                return instance;
            }
        }
    }
}