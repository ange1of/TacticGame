using System;

namespace game {

    class CCurse : ICast {
        public void Cast(BattleUnitsStack stack, params BattleUnitsStack[] receivers) {
            if (receivers.Length != 1) {
                throw new GameException($"Cast Curse has invalid count of receivers :{receivers.Length}");
            }
            receivers[0].AddModifier(MCurse.Instance, 1);
        }
        public CastType type { get; } = CastType.Curse;

        private CCurse() {}
        public static CCurse Instance {
            get {
                if (instance == null) {
                    instance = new CCurse();
                }
                return instance;
            }
        }
        private static CCurse instance;
    }

}