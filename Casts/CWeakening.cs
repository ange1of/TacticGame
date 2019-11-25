using System;

namespace game {

    class CWeakening : ICast {
        public void Cast(BattleUnitsStack stack, params BattleUnitsStack[] receivers) {
            if (receivers.Length != 1) {
                throw new GameException($"Cast Curse has invalid count of receivers :{receivers.Length}");
            }
            receivers[0].AddModifier(MWeakening.Instance, 1);
        }
        public CastType type { get; } = CastType.Curse;

        private CWeakening() {}
        public static CWeakening Instance {
            get {
                if (instance == null) {
                    instance = new CWeakening();
                }
                return instance;
            }
        }
        private static CWeakening instance;
    }

}