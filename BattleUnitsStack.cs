using System;

namespace game  {

    class BattleUnitsStack {
        public BattleUnitsStack(Unit _unitsType, uint _unitsCount) {
            unitsType = _unitsType;
            if (_unitsCount > MAXSIZE) {
                throw new UnitsStackOverflowException($"Trying to make battle stack of \"{unitsType.type}\" with {_unitsCount} units.\nBattleUnitsStack.MAXSIZE = {MAXSIZE}");
            }
            totalHitPoints = unitsType.hitPoints * _unitsCount;
        }

        public BattleUnitsStack(BattleUnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.totalHitPoints;
        }

        public BattleUnitsStack(UnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.unitsCount * unitsType.hitPoints;
        }

        public void Damage(uint damageHP) {
            totalHitPoints = ((int)totalHitPoints-(int)damageHP > 0) ? (totalHitPoints - damageHP) : 0;
        }

        public void Heal(uint healHP) {
            if (totalHitPoints + healHP > MAXSIZE) {
                throw new UnitsStackOverflowException($"Trying to heal battle stack of \"{unitsType.type}\" to {totalHitPoints+healHP} HP ({(uint)Math.Ceiling((double)(totalHitPoints+healHP)/unitsType.hitPoints)} units).\nUnitsStack.MAXSIZE = {MAXSIZE}");
            }
            totalHitPoints += healHP;
        }

        public override string ToString() {
            return $"{{\"{unitsType.type}\": {totalHitPoints}}}";
        }

        public Unit unitsType { get; }
        public uint unitsCount {
            get {
                return (uint)Math.Ceiling((double)totalHitPoints/unitsType.hitPoints);
            }
        }
        public uint totalHitPoints { get; private set; }
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("BattleUnitsStack:MAXSIZE"));
    }

}