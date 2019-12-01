using System;

namespace game {

    class BaseEffect {

        public BaseEffect(BaseEffect _wrappee = null, BaseEffect _wrapper = null) {
            wrappee = _wrappee;
            wrapper = _wrapper;
        }

        public BaseEffect(BaseEffect otherEffect) {
            wrappee = otherEffect.wrappee;
            wrapper = otherEffect.wrapper;
            type = otherEffect.type;
        }

        // attacker - стек, который бьет
        // target - стек, который бьют
        public virtual void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.BeforeAttack(attacker, target);
        }
        public virtual void AfterAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.AfterAttack(attacker, target);
        }

        // attacker - стек, который изначально атаковал
        // target - стек, который дает сдачу
        public virtual void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.BeforeFightBack(attacker, target);
        }
        public virtual void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterFightBack(attacker, target);
        }

        public virtual void BeforeCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.BeforeCast(cast, caster, targets);
        }
        public virtual void AfterCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.AfterCast(cast, caster, targets);
        }

        public virtual void NewRound(BattleUnitsStack parentStack) {
            wrappee?.NewRound(parentStack);
        }

        public BaseEffect wrappee;
        public BaseEffect wrapper;
        public BattleUnitsStack effectOwner;
        public EffectType type { get; protected set; }
    }

    enum EffectType {
        CleanShot,
        NoResistance,
        Shooter,
        Undead,
        UnlimitedResistance
    }

}

