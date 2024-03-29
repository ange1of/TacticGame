using System;

namespace game {

    public abstract class BaseEffect {

        public BaseEffect() {
            wrappee = null;
            wrapper = null;
        }

        public BaseEffect(BaseEffect _wrappee = null, BaseEffect _wrapper = null) {
            wrappee = _wrappee;
            wrapper = _wrapper;
        }

        public BaseEffect(BaseEffect otherEffect) {
            wrappee = otherEffect.wrappee;
            wrapper = otherEffect.wrapper;
            type = otherEffect.type;
        }

        public abstract BaseEffect Clone();

        public abstract void Init();

        // attacker - стек, который бьет
        // target - стек, который бьют
        public abstract void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target);
        public abstract void AfterAttack(BattleUnitsStack attacker,BattleUnitsStack target);

        // attacker - стек, который изначально атаковал
        // target - стек, который дает сдачу
        public abstract void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target);
        public abstract void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target);

        public abstract void BeforeCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets);
        public abstract void AfterCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets);

        public abstract void NewRound();

        public BaseEffect wrappee;
        public BaseEffect wrapper;
        public BattleUnitsStack effectOwner;

        public string type { get; protected set; }
        public string description { get; protected set; }
    }

}

