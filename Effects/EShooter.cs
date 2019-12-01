using System;

namespace game {

    class EShooter : BaseEffect {
        public EShooter(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.Shooter;  
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.BeforeFightBack(attacker, target);

            targetFB = target.fightedBack;
            target.fightedBack = true;
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.AfterFightBack(attacker, target);

            target.fightedBack = targetFB;
        }

        private bool targetFB;
    }
}