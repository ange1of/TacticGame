using System;

namespace game  {

    class UnitsStack {
        public UnitsStack(Unit _unitsType, uint _unitsCount) {
            unitsType = _unitsType.Copy();

            if (_unitsCount > MAXSIZE) {
                throw new ArgumentException($"Trying to make stack of {unitsType} with more than {MAXSIZE} units");
            }
            unitsCount = _unitsCount;
        }

        public UnitsStack(UnitsStack otherStack) {
            unitsType = otherStack.unitsType.Copy();
            unitsCount = otherStack.unitsCount;
        }

        public UnitsStack Copy() {
            return new UnitsStack(this);
        }

        public string PrintStack() {
            return $"\"{unitsType.type}\": \"{unitsCount}\"";
        }

        public const int MAXSIZE = 999999;
        public readonly Unit unitsType;
        public readonly uint unitsCount;
    }

}