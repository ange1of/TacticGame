using System;

namespace game {
    interface ICast {
        ICast Clone();
        CastType type { get; }
        bool hasBeenCasted { get; }
    }

    interface ISingleCast : ICast {
        void Cast(BattleUnitsStack caster, BattleUnitsStack target);
    }

    interface IFriendCast : ICast {}

    interface IEnemyCast : ICast {}
 
    enum CastType {
        Curse,
        PunishingStrike,
        Weakening,
        Acceleration,
        Resurrection
    }

}