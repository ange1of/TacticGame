using System;

namespace game {
    class Unit {
        public Unit(string _type, uint _hitPoints, uint _attack, int _defence, 
        (uint minDamage, uint maxDamage) _damage, double _initiative) {
            type = _type;
            hitPoints = _hitPoints;
            attack = _attack;
            defence = _defence;

            if (_damage.minDamage > _damage.maxDamage) {
                throw new ArgumentException($"Unit {_type}: MinDamage is greater than MaxDamage");
            }
            damage = _damage;

            initiative = _initiative;
        }

        public Unit(Unit otherUnit) {
            type = otherUnit.type;
            hitPoints = otherUnit.hitPoints;
            attack = otherUnit.attack;
            defence = otherUnit.defence;
            damage = otherUnit.damage;
            initiative = otherUnit.initiative;
        }

        public string PrintUnit() {
            return $"\"{type}\": {{\n" + 
            $"\t\"hitPoints\": \"{hitPoints}\",\n" + 
            $"\t\"attack\": \"{attack}\",\n" +
            $"\t\"defence\": \"{defence}\",\n" +
            $"\t\"damage\": \"{damage}\",\n" +
            $"\t\"initiative\": \"{initiative}\"\n}}";
        }

        public Unit Copy() {
            return new Unit(this);
        }

        public readonly string type;
        public readonly uint hitPoints;
        public readonly uint attack;
        public readonly int defence;
        public readonly (uint minDamage, uint maxDamage) damage;
        public readonly double initiative;
    }
}
