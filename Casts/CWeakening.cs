using System;

namespace game {

    class CWeakening : ISingleCast, IEnemyCast {
        public CWeakening() {}

        public ICast Clone() {
            var result = new CWeakening();
            result.hasBeenCasted = hasBeenCasted;
            return result;
        }

        public void Cast(BattleUnitsStack caster, BattleUnitsStack target) {
            if (!hasBeenCasted) {
                target.AddModifier(MWeakening.Instance, 1);
                hasBeenCasted = true;
            }
        }
        public CastType type { get; } = CastType.Curse;
        public bool hasBeenCasted { get; private set; } = false;
    }

}