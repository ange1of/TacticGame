using System;
using System.Collections.Generic;

namespace game {
    class Griffin : Unit {
        private static Griffin instance;

        private Griffin() : base("Griffin", 30, 7, 5, (5, 10), 15, new List<BaseEffect>(){}) {}

        public static Griffin Instance {
            get {
                if (instance == null) {
                    instance = new Griffin();
                }
                return instance;
            }
        }
    }
}