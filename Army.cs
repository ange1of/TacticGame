using System;
using System.Collections.Generic;

namespace game {
    class Army {
        public Army(List<UnitsStack> _unitsStackList) {
            if (_unitsStackList.Count > MAXSIZE) {
                throw new ArgumentException($"Trying to make army with more than {MAXSIZE} stacks");
            }
            _unitsStackList.ForEach((item) => unitsStackList.Add(item.Copy()));
        }

        public Army(Army otherArmy) {
            otherArmy.unitsStackList.ForEach((item) => unitsStackList.Add(item.Copy()));
        }

        public string PrintArmy() {
            var armyString = "\"Army\": {\n";
            foreach (var stack in unitsStackList) {
                armyString += "\t" + stack.PrintStack() + ",\n";
            }
            return armyString + "}";
        }

        public readonly List<UnitsStack> unitsStackList = new List<UnitsStack>();
        private const int MAXSIZE = 6;
    }
}