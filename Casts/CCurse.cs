using System;

namespace game {

    class CCurse : ISingleCast, IEnemyCast {
        public CCurse() {}

        public ICast Clone() {
            var result = new CCurse();
            result.hasBeenCasted = hasBeenCasted;
            return result;
        }

        public void Cast(BattleUnitsStack caster, BattleUnitsStack target) {
            if (!hasBeenCasted) {
                target.AddModifier(MCurse.Instance, 1);
                hasBeenCasted = true;
            }
        }

        public CastType type { get; } = CastType.Curse;
        public bool hasBeenCasted { get; private set; } = false;
    }

}