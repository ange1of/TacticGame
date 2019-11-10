using System;
using System.Collections.Generic;

namespace game {

    class Army {
        public Army(IEnumerable<UnitsStack> otherUnitsStackList) {
            foreach (UnitsStack currentStack in otherUnitsStackList) {
                if (_unitsStackList.Count == MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make army of [{string.Join(", ", otherUnitsStackList)}]\nArmy.MAXSIZE = {MAXSIZE}");
                }
                _unitsStackList.Add(new UnitsStack(currentStack));
            }
        }

        public Army(params UnitsStack[] otherUnitsStackList) {
            if (otherUnitsStackList.Length > MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make army of [{string.Join<UnitsStack>(", ", otherUnitsStackList)}]\nArmy.MAXSIZE = {MAXSIZE}");
            }
            foreach (UnitsStack currentStack in otherUnitsStackList) {
                _unitsStackList.Add(new UnitsStack(currentStack));
            }
        }

        public Army(Army otherArmy) {
            otherArmy.unitsStackList.ForEach((currentStack) => _unitsStackList.Add(new UnitsStack(currentStack)));
        }

        // В призываемых юнитах будет свойство уничтожения после конца битвы, поэтому ничего не переполнится
        public Army(BattleArmy otherArmy) {
            otherArmy.unitsStackList.ForEach((currentStack) => _unitsStackList.Add(new UnitsStack(currentStack)));
        }

        public override string ToString() {
            return "{\"unitsStackList\": [" + string.Join(",", _unitsStackList) + "]}";
        }

        public void AddStack(UnitsStack newUnitsStack) {
            if (_unitsStackList.Count == MAXSIZE) {
                throw new ArmyOverflowException($"Trying to add too many stacks to Army.\nArmy.MAXSIZE is {MAXSIZE}");
            }
            _unitsStackList.Add(new UnitsStack(newUnitsStack));
        }

        public void RemoveStack(UnitsStack stackToRemove) {
            _unitsStackList.Remove(stackToRemove);
        }
        public void Clear() {
            _unitsStackList.Clear();
        }
        
        public List<UnitsStack> unitsStackList {
            get {
                return new List<UnitsStack>(_unitsStackList);
            }
            private set {
                _unitsStackList = value;
            }
        }
        private List<UnitsStack> _unitsStackList = new List<UnitsStack>();
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("Army:MAXSIZE"));
    }
}