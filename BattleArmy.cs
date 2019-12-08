using System;
using System.Collections.Generic;
using System.Linq;

namespace game {

    public class BattleArmy {
        public BattleArmy(IEnumerable<BattleUnitsStack> otherUnitsStackList) {
            foreach (BattleUnitsStack currentStack in otherUnitsStackList) {
                if (_unitsStackList.Count == MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make battle army of [{string.Join(", ", otherUnitsStackList)}]\nBattleArmy.MAXSIZE = {MAXSIZE}");
                }
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            }
        }

        public BattleArmy(params BattleUnitsStack[] otherUnitsStackList) {
            if (otherUnitsStackList.Length > MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make battle army of [{string.Join<BattleUnitsStack>(", ", otherUnitsStackList)}]\nBattleArmy.MAXSIZE = {MAXSIZE}");
            }
            foreach (BattleUnitsStack currentStack in otherUnitsStackList) {
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            }
        }

        public BattleArmy(BattleArmy otherArmy) {
            otherArmy.unitsStackList.ForEach((currentStack) => {
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            });
        }

        public BattleArmy(IEnumerable<UnitsStack> otherUnitsStackList) {
            foreach (UnitsStack currentStack in otherUnitsStackList) {
                if (_unitsStackList.Count == MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make battle army of [{string.Join(", ", otherUnitsStackList)}]\nBattleArmy.MAXSIZE = {MAXSIZE}");
                }
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            }
        }

        public BattleArmy(params UnitsStack[] otherUnitsStackList) {
            if (otherUnitsStackList.Length > MAXSIZE) {
                    throw new ArmyOverflowException($"Trying to make battle army of [{string.Join<UnitsStack>(", ", otherUnitsStackList)}]\nBattleArmy.MAXSIZE = {MAXSIZE}");
            }
            foreach (UnitsStack currentStack in otherUnitsStackList) {
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            }
        }

        public BattleArmy(Army otherArmy) {
            otherArmy.unitsStackList.ForEach((currentStack) => {
                var newStack = new BattleUnitsStack(currentStack);
                newStack.parentArmy = this;
                _unitsStackList.Add(newStack);
            });
        }

        public override string ToString() {
            return "{\"unitsStackList\": [" + string.Join(",", _unitsStackList) + "]}";
        }

        public void AddStack(BattleUnitsStack newUnitsStack) {
            if (_unitsStackList.Count == MAXSIZE) {
                throw new ArmyOverflowException($"Trying to add too many stacks to BattleArmy.\nBattleArmy.MAXSIZE is {MAXSIZE}");
            }
            _unitsStackList.Add(new BattleUnitsStack(newUnitsStack));
        }

        public void RemoveStack(BattleUnitsStack stackToRemove) {
            _unitsStackList.Remove(stackToRemove);
        }
        public void Clear() {
            _unitsStackList.Clear();
        }
        
        public List<BattleUnitsStack> unitsStackList {
            get {
                return new List<BattleUnitsStack>(_unitsStackList);
            }
            private set {
                _unitsStackList = value;
            }
        }

        public bool isAlive {
            get {
                return _unitsStackList.Any(stack => stack.unitsCount > 0);
            }
        }
        private List<BattleUnitsStack> _unitsStackList = new List<BattleUnitsStack>();
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("BattleArmy:MAXSIZE"));
    }
}