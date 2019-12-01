using System;

namespace game {

    class CResurrection : ISingleCast, IFriendCast {
        public CResurrection() {}

        public ICast Clone() {
            var result = new CResurrection();
            result.hasBeenCasted = hasBeenCasted;
            return result;
        }

        public void Cast(BattleUnitsStack caster, BattleUnitsStack target) {
            if (!hasBeenCasted) {
                target.Heal(100 * caster.unitsCount);
                hasBeenCasted = true;
            }
        }
        public CastType type { get; } = CastType.Resurrection;
        public bool hasBeenCasted { get; private set; } = false;
    }

}