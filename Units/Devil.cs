using System;
using System.Collections.Generic;

namespace game {
    class Devil : Unit {
        private static Devil instance;

        private Devil() : base("Devil", 180, 27, 27, (45, 45), 11, new List<BaseEffect>(){}) {}

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