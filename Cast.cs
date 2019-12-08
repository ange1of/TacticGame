using System;

namespace game {
    public interface ICast {
        ICast Clone();
        string type { get; }
        bool hasBeenCasted { get; }
    }

    public interface ISingleCast : ICast {
        void Cast(BattleUnitsStack caster, BattleUnitsStack target);
    }

    public interface IFriendCast : ICast {}

    public interface IEnemyCast : ICast {}
 
    // enum CastType {
    //     Curse,
    //     PunishingStrike,
    //     Weakening,
    //     Acceleration,
    //     Resurrection
    // }

}