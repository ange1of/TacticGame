using System;

namespace game {

    class CAcceleration : ISingleCast, IFriendCast {
        public CAcceleration() {}

        public ICast Clone() {
            var result = new CAcceleration();
            result.hasBeenCasted = hasBeenCasted;
            return result;
        }
        public void Cast(BattleUnitsStack caster, BattleUnitsStack target) {
            if (!hasBeenCasted) {
                target.AddModifier(MAcceleration.Instance, 1);
                hasBeenCasted = true;
            }
        }
        public CastType type { get; } = CastType.Acceleration;
        public bool hasBeenCasted { get; private set; } = false;
    }

}