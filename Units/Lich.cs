using System;
using System.Collections.Generic;

namespace game {
    class Lich : Unit {
        private static Lich instance;

        private Lich() : base("Lich", 50, 15, 15, (12, 17), 10, new List<BaseEffect>(){}) {}

        public static Lich Instance {
            get {
                if (instance == null) {
                    instance = new Lich();
                }
                return instance;
            }
        }
    }
}