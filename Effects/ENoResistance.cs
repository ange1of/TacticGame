using System;

namespace game {
    
    class ENoResistance : BaseEffect {
        public ENoResistance(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.NoResistance;
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.BeforeFightBack(attacker, target);

            if (effectOwner == attacker) {
                targetFB = target.fightedBack;
                target.fightedBack = true;
            }
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.AfterFightBack(attacker, target);

            if (effectOwner == attacker) {
                target.fightedBack = targetFB;
            }
        }

        private bool targetFB;
    }

}