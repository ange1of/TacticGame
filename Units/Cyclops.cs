using System;
using System.Collections.Generic;

namespace game {
    class Cyclops : Unit {
        private static Cyclops instance;

        private Cyclops() : base("Cyclops", 85, 20, 15, (18, 26), 10, new List<BaseEffect>(){new EShooter()}, new List<ICast>(){}) {}

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