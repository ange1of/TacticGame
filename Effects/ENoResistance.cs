using System;

namespace game {
    
    class ENoResistance : BaseEffect {
        public ENoResistance(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.NoResistance;
        }
        public override BaseEffect Clone() {
            BaseEffect copy = new ENoResistance(wrappee, wrapper);
            copy.effectOwner = effectOwner;
            ((ENoResistance)copy).targetFB = targetFB;
            return copy;
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.BeforeFightBack(attacker, target);

            if (effectOwner == attacker) {
                targetFB = target.fightedBack;
                target.fightedBack = true;
            }
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            wrappee?.AfterFightBack(attacker, target);

            if (effectOwner == attacker) {
                target.fightedBack = targetFB;
            }
        }

        public override void Init(){
            wrappee?.Init();
        }
        public override void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.BeforeAttack(attacker, target);
        }
        public override void AfterAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            wrappee?.AfterAttack(attacker, target);
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

        private bool targetFB = true;
    }

}