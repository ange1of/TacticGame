using System;
using System.Collections.Generic;

namespace game {
    class Crossbowman : Unit {
        private static Crossbowman instance;

        private Crossbowman() : base("Crossbowman", 10, 4, 4, (2, 8), 8, new List<BaseEffect>(){new EShooter(), new ECleanShot()}, new List<ICast>(){}) {}

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