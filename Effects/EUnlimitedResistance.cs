using System;

namespace game {
    
    class EUnlimitedResistance : BaseEffect {
        public EUnlimitedResistance(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.UnlimitedResistance;
        }

        public override void BeforeFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.BeforeFightBack(attacker, target);

            if (effectOwner == target) {
                target.fightedBack = false;
            }
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            base.AfterFightBack(attacker, target);

            if (effectOwner == target) {
                target.fightedBack = false;
            }
        }
    }

}