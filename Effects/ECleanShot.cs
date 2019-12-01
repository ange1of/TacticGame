using System;

namespace game {
    
    class ECleanShot : BaseEffect {
        public ECleanShot(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.CleanShot;
        }

        public override void BeforeAttack(BattleUnitsStack attacker,BattleUnitsStack target) {
            base.BeforeAttack(attacker, target);

            if (effectOwner == attacker) {
                initialDefence = target.defence;
                target.defence = 0;
            }
        }

        public override void AfterAttack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.AfterAttack(attacker, target);

            if (effectOwner == attacker) {
                target.defence = initialDefence;
            }
        }

        private int initialDefence = 0;
    }

}