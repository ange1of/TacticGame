using System;

namespace game {

    class BattleUnit {

        public BattleUnit(Unit _baseUnit) {
            baseUnit = _baseUnit;

            hitPoints = _baseUnit.hitPoints;
            attack = _baseUnit.attack;
            defence = _baseUnit.defence;
            damage = (_baseUnit.damage.minDamage > _baseUnit.damage.maxDamage) ? (_baseUnit.damage.maxDamage, _baseUnit.damage.minDamage) : _baseUnit.damage;
            initiative = _baseUnit.initiative;
        }

        public override string ToString() {
            return $"{{\"type\": \"{baseUnit.type}\", \"hitPoints\": {hitPoints}, \"attack\": {attack}, \"defence\": {defence}, \"damage\": {{\"minDamage\": {damage.minDamage}, \"maxDamage\": {damage.maxDamage}}}, \"initiative\": {initiative}}}";
        }

        public Unit baseUnit { get; }

        public uint hitPoints { get; set; }
        public uint attack { get; set; }
        public int defence { get; set; }
        public (uint minDamage, uint maxDamage) damage { get; set; }
        public double initiative { get; set; }
    }

}