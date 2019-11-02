using System;
using System.Collections.Generic;

namespace game {

    class Army {
        public Army(IEnumerable<UnitsStack> _unitsStackList) {
            foreach (UnitsStack _currentStack in _unitsStackList) {
                if (unitsStackList.Count == MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make army of [{string.Join(", ", _unitsStackList)}]\nArmy.MAXSIZE = {MAXSIZE}");
                }
                unitsStackList.Add(new UnitsStack(_currentStack));
            }
        }

        public Army(params UnitsStack[] _unitsStackList) {
            if (_unitsStackList.Length > MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make army of [{string.Join<UnitsStack>(", ", _unitsStackList)}]\nArmy.MAXSIZE = {MAXSIZE}");
            }
            foreach (UnitsStack _currentStack in _unitsStackList) {
                unitsStackList.Add(new UnitsStack(_currentStack));
            }
        }

        public Army(Army otherArmy) {
            otherArmy.unitsStackList.ForEach((_currentStack) => unitsStackList.Add(new UnitsStack(_currentStack)));
        }

        public override string ToString() {
            return "{\"unitsStackList\": [" + string.Join(",", unitsStackList) + "]}";
        }

        public void AddStack(UnitsStack newUnitsStack) {
            if (unitsStackList.Count > MAXSIZE) {
                throw new ArmyOverflowException($"Trying to add too many stacks to Army.\nArmy.MAXSIZE is {MAXSIZE}");
            }
            unitsStackList.Add(new UnitsStack(newUnitsStack));
        }

        public void RemoveStack(UnitsStack stackToRemove) {
            unitsStackList.Remove(stackToRemove);
        }
        public void RemoveStack(int index) {
            unitsStackList.RemoveAt(index);
        }
        public void Clear() {
            unitsStackList.Clear();
        }
        
        public List<UnitsStack> unitsStackList { get; } = new List<UnitsStack>();
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("Army:MAXSIZE"));
    }
}