using System;

namespace game {
    interface ICast {
        CastType type { get; }
        void Cast(BattleUnitsStack castingStack, params BattleUnitsStack[] receivingStacks);
    }

    enum CastType {
        Curse,
        PunishingStrike,
        Weakening,
        Acceleration,
        Resurrection
    }

}