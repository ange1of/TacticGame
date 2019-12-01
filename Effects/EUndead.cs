using System;

namespace game {

    class EUndead : BaseEffect {
        public EUndead(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.Undead;
        }
        public override BaseEffect Clone() {
            BaseEffect copy = new EUndead(wrappee, wrapper);
            copy.effectOwner = effectOwner;
            return copy;
        }

        public override void AfterAttack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterAttack(attacker, target);

            if (effectOwner == target && effectOwner.totalHitPoints < effectOwner.initHitPoints) {
                effectOwner.ressurectable = true;
            }
        }
        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterFightBack(attacker, target);

            if (effectOwner == attacker && effectOwner.totalHitPoints < effectOwner.initHitPoints) {
                effectOwner.ressurectable = true;
            }
        }
        public override void AfterCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.AfterCast(cast, caster, targets);

            foreach (var target in targets) {
                if (effectOwner == target && effectOwner.totalHitPoints == effectOwner.initHitPoints) {
                    effectOwner.ressurectable = false;
                    break;
                }
            }
        }
        
        public override void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.BeforeAttack(attacker, target);
        }
        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.BeforeFightBack(attacker, target);
        }
        public override void BeforeCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            wrappee?.BeforeCast(cast, caster, targets);
        }
        public override void NewRound() {
            wrappee?.NewRound();
        }
    }

}