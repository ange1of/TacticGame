using System;

namespace game {
    
    class ECleanShot : BaseEffect {
        public ECleanShot(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.CleanShot;
        }
        public override BaseEffect Clone() {
            BaseEffect copy = new ECleanShot(wrappee, wrapper);
            copy.effectOwner = effectOwner;
            ((ECleanShot)copy).initialDefence = initialDefence;
            return copy;
        }

        public override void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.BeforeAttack(attacker, target);

            if (effectOwner == attacker) {
                initialDefence = target.defence;
                target.defence = 0;
            }
        }

        public override void AfterAttack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterAttack(attacker, target);

            if (effectOwner == attacker) {
                target.defence = initialDefence;
            }
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.BeforeFightBack(attacker, target);
        }
        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterFightBack(attacker, target);
        }
        public override void BeforeCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.BeforeCast(cast, caster, targets);
        }
        public override void AfterCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.AfterCast(cast, caster, targets);
        }
        public override void NewRound() {
            wrappee?.NewRound();
        }

        private int initialDefence = 0;
    }

}