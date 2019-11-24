using System;
using System.Text.Json;

namespace game {

    class Unit {

        public Unit(string _type, uint _hitPoints, uint _attack, int _defence, 
        (uint minDamage, uint maxDamage) _damage, double _initiative) {
            type = _type;
            hitPoints = _hitPoints;
            attack = _attack;
            defence = _defence;

            damage = (_damage.minDamage > _damage.maxDamage) ? (_damage.maxDamage, _damage.minDamage) : _damage;

            initiative = _initiative;
        }

        public override string ToString() {
            return $"{{\"type\": \"{type}\", \"hitPoints\": {hitPoints}, \"attack\": {attack}, \"defence\": {defence}, \"damage\": {{\"minDamage\": {damage.minDamage}, \"maxDamage\": {damage.maxDamage}}}, \"initiative\": {initiative}}}";
        }

        public string type { get; }
        public uint hitPoints { get; }
        public uint attack { get; }
        public int defence { get; }
        public (uint minDamage, uint maxDamage) damage { get; }
        public double initiative { get; }
    }
}