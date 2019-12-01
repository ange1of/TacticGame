using System;
using System.Collections.Generic;

namespace game {
    class Angel : Unit {
        private static Angel instance;

        private Angel() : base("Angel", 180, 27, 27, (45, 45), 11, new List<BaseEffect>(){}) {}

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