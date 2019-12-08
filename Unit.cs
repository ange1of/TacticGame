using System;
using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

namespace game {

    public class Unit {

        public Unit(string _type, uint _hitPoints, uint _attack, int _defence, 
        (uint minDamage, uint maxDamage) _damage, double _initiative, IEnumerable<BaseEffect> _effects, IEnumerable<ICast> _casts) {
            type = _type;
            hitPoints = _hitPoints;
            attack = _attack;
            defence = _defence;

            damage = (_damage.minDamage > _damage.maxDamage) ? (_damage.maxDamage, _damage.minDamage) : _damage;

            initiative = _initiative;

            _Effects = _effects.Select(x => x.Clone()).ToImmutableList();
            _Casts = _casts.Select(x => x.Clone()).ToImmutableList();
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

        
        public List<BaseEffect> Effects {
            get {
                return _Effects.Select(x => x.Clone()).ToList();
            }
        }
        private ImmutableList<BaseEffect> _Effects;

        public List<ICast> Casts {
            get {
                return _Casts.Select(x => x.Clone()).ToList();
            }
        }
        private ImmutableList<ICast> _Casts;
    }
}