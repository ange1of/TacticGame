using System;

namespace game {

    class EUndead : BaseEffect {
        public EUndead(BaseEffect _wrappee = null, BaseEffect _wrapper = null) : base(_wrappee, _wrapper) {
            type = EffectType.Undead;
        }

        public override void AfterAttack(BattleUnitsStack attacker, BattleUnitsStack target) {
            if (effectOwner == target && effectOwner.totalHitPoints < effectOwner.initHitPoints) {
                effectOwner.ressurectable = true;
            }
        }

        public override void AfterFightBack(BattleUnitsStack attacker, BattleUnitsStack target) {
            if (effectOwner == attacker && effectOwner.totalHitPoints < effectOwner.initHitPoints) {
                effectOwner.ressurectable = true;
            }
        }

        public override void AfterCast(ICast cast, BattleUnitsStack caster, params BattleUnitsStack[] targets) {
            foreach (var target in targets) {
                if (effectOwner == target && effectOwner.totalHitPoints == effectOwner.initHitPoints) {
                    effectOwner.ressurectable = false;
                    break;
                }
            }
        }
    }

}