using System;

namespace game {

    static class Action {
        public static void Attack(BattleUnitsStack attackingStack, BattleUnitsStack defendingStack) {
            attackingStack.state = State.MadeMove;

            AttackWithModifiers(attackingStack, defendingStack);
            if (!defendingStack.fightedBack) {
                defendingStack.fightedBack = true;
                AttackWithModifiers(defendingStack, attackingStack);
            }
        }

        private static void AttackWithModifiers(BattleUnitsStack attackingStack, BattleUnitsStack defendingStack) {
            Unit attackingUnit = attackingStack.metaUnit;
            Unit defendingUnit = defendingStack.metaUnit;
            (uint minDamage, uint maxDamage) finalDamage;
            
            if (attackingUnit.attack > defendingUnit.defence) {
                finalDamage.minDamage = (uint)Math.Round(attackingStack.unitsCount * attackingUnit.damage.minDamage * (1 + 0.05 * (attackingUnit.attack - defendingUnit.defence)));
                finalDamage.maxDamage = (uint)Math.Round(attackingStack.unitsCount * attackingUnit.damage.maxDamage * (1 + 0.05 * (attackingUnit.attack - defendingUnit.defence)));
            }
            else {
                finalDamage.minDamage = (uint)Math.Round(attackingStack.unitsCount * attackingUnit.damage.minDamage / (1 + 0.05 * (defendingUnit.defence - attackingUnit.attack)));
                finalDamage.maxDamage = (uint)Math.Round(attackingStack.unitsCount * attackingUnit.damage.maxDamage / (1 + 0.05 * (defendingUnit.defence - attackingUnit.attack)));
            }

            var random = new Random();
            defendingStack.Damage((uint)random.Next((int)finalDamage.minDamage, (int)finalDamage.maxDamage));
        }

        public static void Cast(BattleUnitsStack stack) {
            stack.state = State.MadeMove;
            Console.WriteLine("CASTED AMAZING SPELL!");
        }

        public static void Wait(BattleUnitsStack waitingStack) {
            waitingStack.state = State.Awaiting;
        }

        public static void Defend(BattleUnitsStack defendingStack) {
            defendingStack.state = State.Defending;
            defendingStack.AddModifier(Defense.Instance, 1);
        }
    }

}