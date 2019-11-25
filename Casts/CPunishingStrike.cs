using System;

namespace game {

    class CPunishingStrike : ICast {
        public void Cast(BattleUnitsStack stack, params BattleUnitsStack[] receivers) {
            if (receivers.Length != 1) {
                throw new GameException($"Cast Curse has invalid count of receivers :{receivers.Length}");
            }
            receivers[0].AddModifier(MPunishingStrike.Instance, 1);
        }
        public CastType type { get; } = CastType.PunishingStrike;

        private CPunishingStrike() {}
        public static CPunishingStrike Instance {
            get {
                if (instance == null) {
                    instance = new CPunishingStrike();
                }
                return instance;
            }
        }
        private static CPunishingStrike instance;
    }

}