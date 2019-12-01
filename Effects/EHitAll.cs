using System;

namespace game {

    class EHitAll : BaseEffect {
        public EHitAll(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.HitAll;  
        }
        public override BaseEffect Clone() {
            BaseEffect copy = new EHitAll(wrappee, wrapper);
            copy.effectOwner = effectOwner;
            ((EHitAll)copy).beforeHP = beforeHP;
            return copy;
        }

        public override void AfterAttack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterAttack(attacker, target);

            if (effectOwner == attacker) {
                foreach (var opponentStack in target.parentArmy.unitsStackList) {
                    if (opponentStack == target || opponentStack.totalHitPoints == 0) { continue; }
                    Action.PureAttack(effectOwner, opponentStack);
                }
            }
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.BeforeFightBack(attacker, target);

            if (effectOwner == target) {
                beforeHP = attacker.totalHitPoints;
            }
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterFightBack(attacker, target);

            if (effectOwner == target && attacker.totalHitPoints < beforeHP) {
                foreach (var opponentStack in attacker.parentArmy.unitsStackList) {
                    if (opponentStack == attacker || opponentStack.totalHitPoints == 0) { continue; }
                    Action.PureAttack(effectOwner, opponentStack);
                }
            }
        }

        public override void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.BeforeAttack(attacker, target);
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

        private uint beforeHP = 0;
    }
}