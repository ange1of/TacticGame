using System;

namespace game  {

    public class UnitsStack {
        public UnitsStack(Unit _unitsType, uint _unitsCount) {
            unitsType = _unitsType;

            if (_unitsCount > MAXSIZE) {
                throw new UnitsStackOverflowException($"Trying to make stack of \"{unitsType.type}\" with {_unitsCount} units.\nUnitsStack.MAXSIZE = {MAXSIZE}");
            }
            unitsCount = _unitsCount;
        }

        public UnitsStack(UnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            unitsCount = otherStack.unitsCount;
        }

        public UnitsStack(BattleUnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            unitsCount = otherStack.unitsCount;
        }

        public override string ToString() {
            return $"{{\"{unitsType.type}\": {unitsCount}}}";
        }

        public Unit unitsType { get; }
        public uint unitsCount { get; }
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("UnitsStack:MAXSIZE"));
    }

}