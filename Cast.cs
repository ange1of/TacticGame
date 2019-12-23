using System;

namespace game {
    public interface ICast {
        ICast Clone();
        string type { get; }
        bool hasBeenCasted { get; }
        bool Applicable(BattleUnitsStack stack);
        string description { get; }
    }

    public interface ISingleCast : ICast {
        void Cast(BattleUnitsStack caster, BattleUnitsStack target);
    }

    public interface IFriendCast : ICast {}

    public interface IEnemyCast : ICast {}
    
}