using System;
using System.Collections.Generic;

namespace game {
    class BoneDragon : Unit {
        private static BoneDragon instance;

        private BoneDragon() : base("BoneDragon", 150, 27, 28, (15, 30), 11, new List<BaseEffect>(){new EUndead()}) {}

        public static BoneDragon Instance {
            get {
                if (instance == null) {
                    instance = new BoneDragon();
                }
                return instance;
            }
        }
    }
}