using System;
using System.Collections.Generic;

namespace game {
    class Hydra : Unit {
        private static Hydra instance;

        private Hydra() : base("Hydra", 80, 15, 12, (7, 14), 7, new List<BaseEffect>(){}) {}

        public static Hydra Instance {
            get {
                if (instance == null) {
                    instance = new Hydra();
                }
                return instance;
            }
        }
    }
}