using System.Collections.Generic;

namespace game {
    class Fury : Unit {
        private static Fury instance;

        private Fury() : base("Fury", 16, 5, 3, (5, 7), 16, new List<BaseEffect>(){new ENoResistance()}, new List<ICast>(){}) {}

        public static Fury Instance {
            get {
                if (instance == null) {
                    instance = new Fury();
                }
                return instance;
            }
        }
    }
}