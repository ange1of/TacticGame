using System;

namespace game {

    class CPunishingStrike : ISingleCast, IEnemyCast {
        public CPunishingStrike() {}

        public ICast Clone() {
            var result = new CPunishingStrike();
            result.hasBeenCasted = hasBeenCasted;
            return result;
        }

        public void Cast(BattleUnitsStack caster, BattleUnitsStack target) {
            if (!hasBeenCasted) {
                target.AddModifier(MPunishingStrike.Instance, 1);
                hasBeenCasted = true;
            }
        }
        public CastType type { get; } = CastType.PunishingStrike;
        public bool hasBeenCasted { get; private set; } = false;
    }

}