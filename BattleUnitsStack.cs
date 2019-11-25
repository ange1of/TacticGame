using System;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace game  {

    class BattleUnitsStack {
        public BattleUnitsStack(Unit _unitsType, uint _unitsCount) {
            unitsType = _unitsType;
            if (_unitsCount > MAXSIZE) {
                throw new UnitsStackOverflowException($"Trying to make battle stack of \"{unitsType.type}\" with {_unitsCount} units.\nBattleUnitsStack.MAXSIZE = {MAXSIZE}");
            }
            totalHitPoints = unitsType.hitPoints * _unitsCount;
            state = State.NotMadeMove;
            fightedBack = false;
            initHitPoints = totalHitPoints;
            _metaUnit = new BattleUnit(_unitsType);
            Modifiers = new OrderedDictionary();
            parentArmy = null;
        }

        public BattleUnitsStack(BattleUnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.totalHitPoints;
            state = otherStack.state;
            fightedBack = otherStack.fightedBack;
            initHitPoints = otherStack.initHitPoints;
            _metaUnit = new BattleUnit(otherStack.metaUnit);
            Modifiers = new OrderedDictionary();
            foreach (IModifier mod in otherStack.Modifiers.Keys) {
                Modifiers.Add(mod, (double)otherStack.Modifiers[mod]);
            }
            parentArmy = null;
        }

        public BattleUnitsStack(UnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.unitsCount * unitsType.hitPoints;
            state = State.NotMadeMove;
            fightedBack = false;
            initHitPoints = totalHitPoints;
            _metaUnit = new BattleUnit(otherStack.unitsType);
            Modifiers = new OrderedDictionary();
            parentArmy = null;
        }

        public uint Damage(uint damageHP) {
            uint oldHP = totalHitPoints;
            totalHitPoints = ((int)totalHitPoints-(int)damageHP > 0) ? (totalHitPoints - damageHP) : 0;
            Console.WriteLine($"{unitsType.type} got {oldHP-totalHitPoints} damage");
            return oldHP-totalHitPoints;
        }

        public uint Heal(uint healHP) {
            uint oldHP = totalHitPoints;
            totalHitPoints = (totalHitPoints + healHP < initHitPoints) ? totalHitPoints + healHP : initHitPoints;
            Console.WriteLine($"{unitsType.type} healed {oldHP-totalHitPoints} hp");
            return totalHitPoints - oldHP;
        }

        public override string ToString() {
            return $"{{\"{unitsType.type}\": {totalHitPoints}}}";
        }

        public void AddModifier(IModifier modifier, int movesCount) {
            if (Modifiers.Contains(modifier)) {
                Modifiers[modifier] = (double)movesCount;
            }
            else {
                Modifiers.Add(modifier, (double)movesCount);
                if (modifier is IDamageModifier) {
                    _metaUnit.damage = ((IDamageModifier)modifier).ModifyDamage(_metaUnit.damage);
                }
                if (modifier is IAttackModifier) {
                    _metaUnit.attack = ((IAttackModifier)modifier).ModifyAttack(_metaUnit.attack);
                }
                if (modifier is IDefenseModifier) {
                    _metaUnit.defence = ((IDefenseModifier)modifier).ModifyDefense(_metaUnit.defence);
                }
                if (modifier is IHitPointsModifier) {
                    uint newHitPoints = ((IHitPointsModifier)modifier).ModifyHitPoints(_metaUnit.hitPoints);
                    initHitPoints = newHitPoints * unitsCount;
                    totalHitPoints = (uint)(Math.Floor((double)totalHitPoints / (double)_metaUnit.hitPoints) * newHitPoints + totalHitPoints % _metaUnit.hitPoints);
                    _metaUnit.hitPoints = newHitPoints;
                }
                if (modifier is IInitiativeModifier) {
                    _metaUnit.initiative = ((IInitiativeModifier)modifier).ModifyInitiative(_metaUnit.initiative);
                }
            }
        }

        public void RemoveModifier(IModifier modifier) {
            if (modifier is IDamageModifier) {
                _metaUnit.damage = ((IDamageModifier)modifier).UnmodifyDamage(_metaUnit.damage);
            }
            if (modifier is IAttackModifier) {
                _metaUnit.attack = ((IAttackModifier)modifier).UnmodifyAttack(_metaUnit.attack);
            }
            if (modifier is IDefenseModifier) {
                _metaUnit.defence = ((IDefenseModifier)modifier).UnmodifyDefense(_metaUnit.defence);
            }
            if (modifier is IHitPointsModifier) {
                uint newHitPoints = ((IHitPointsModifier)modifier).UnmodifyHitPoints(_metaUnit.hitPoints);
                initHitPoints = unitsCount * newHitPoints;
                totalHitPoints = (uint)(Math.Floor((double)totalHitPoints / (double)_metaUnit.hitPoints) * newHitPoints + totalHitPoints % _metaUnit.hitPoints);
                _metaUnit.hitPoints = newHitPoints;
            }
            if (modifier is IInitiativeModifier) {
                _metaUnit.initiative = ((IInitiativeModifier)modifier).UnmodifyInitiative(_metaUnit.initiative);
            }
            Modifiers.Remove(modifier);
        }

        public void NewRound() {
            state = State.NotMadeMove;
            fightedBack = false;
            var expiredModifiers = new List<IModifier>();
            foreach (IModifier key in Modifiers.Keys) {
                if ((double)Modifiers[key] == 1.0) {
                    expiredModifiers.Add(key);
                }
                else {
                    Modifiers[key] = (double)Modifiers[key] - 1;
                }
            }
            expiredModifiers.ForEach(mod => RemoveModifier((IModifier)mod));
        }

        public OrderedDictionary Modifiers { get; private set; } = new OrderedDictionary();

        public Unit unitsType { get; }
        public Unit metaUnit {
            get {
                return new Unit(unitsType.type, _metaUnit.hitPoints, _metaUnit.attack, _metaUnit.defence, _metaUnit.damage, _metaUnit.initiative);
            }
        }
        private BattleUnit _metaUnit;
        public uint unitsCount {
            get {
                return (uint)Math.Ceiling((double)totalHitPoints/_metaUnit.hitPoints);
            }
        }
        public bool fightedBack { get; set; }
        public State state { get; set; }
        public BattleArmy parentArmy { get; set; }
        public uint initHitPoints { get; private set; }
        public uint totalHitPoints { get; private set; }
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("BattleUnitsStack:MAXSIZE"));
    }

}