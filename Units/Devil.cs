using System;

namespace game {
    class Devil : Unit {
        private static Devil instance;

        private Devil() : base("Devil", 180, 27, 27, (45, 45), 11) {}

        public static Devil Instance {
            get {
                if (instance == null) {
                    instance = new Devil();
                }
                return instance;
            }
        }
    }
}