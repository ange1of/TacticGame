using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

namespace game {

    public class BattleUnitsStack {
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

            foreach (var effect in _unitsType.Effects) {
                AddEffect(effect, double.PositiveInfinity);
            }
            GetLastEffect()?.Init();
            foreach (var cast in _unitsType.Casts) {
                Casts.Add(cast);
            }

            parentArmy = null;
        }

        public BattleUnitsStack(BattleUnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.totalHitPoints;
            state = otherStack.state;
            fightedBack = otherStack.fightedBack;
            initHitPoints = otherStack.initHitPoints;
            _metaUnit = new BattleUnit(otherStack.metaUnit);

            foreach (IModifier mod in otherStack.Modifiers.Keys) {
                Modifiers.Add(mod, (double)otherStack.Modifiers[mod]);
            }
            foreach (BaseEffect effect in otherStack.Effects.Keys) {
                AddEffect(effect, double.PositiveInfinity);
            }
            GetLastEffect()?.Init();

            foreach (var cast in otherStack.Casts) {
                Casts.Add(cast);
            }
            parentArmy = otherStack.parentArmy;
        }

        public BattleUnitsStack(UnitsStack otherStack) {
            unitsType = otherStack.unitsType;
            totalHitPoints = otherStack.unitsCount * unitsType.hitPoints;
            state = State.NotMadeMove;
            fightedBack = false;
            initHitPoints = totalHitPoints;
            _metaUnit = new BattleUnit(otherStack.unitsType);

            foreach (BaseEffect effect in otherStack.unitsType.Effects) {
                AddEffect(effect, double.PositiveInfinity);
            }
            GetLastEffect()?.Init();
            foreach (var cast in otherStack.unitsType.Casts) {
                Casts.Add(cast);
            }
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
            return $"{unitsType.type}: {totalHitPoints} ({(!fightedBack ? "FB" : "")}) E({string.Join(", ", GetEffects())}) C({string.Join(", ", Casts.Select(x => x.type))}) M({string.Join(", ", GetModifiers())})";
        }

        public void AddModifier(IModifier modifier, double movesCount) {
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

        public void AddEffect(BaseEffect effect, double movesCount) {
            effect = effect.Clone();
            foreach (BaseEffect existingEffect in Effects.Keys) {
                if (effect.type == existingEffect.type) {
                    Effects[existingEffect] = movesCount;
                    return;
                }
            }
            if (Effects.Count > 0) {
                var lastEffect = GetLastEffect();
                lastEffect.wrapper = effect;
                effect.wrappee = lastEffect;
            }
            effect.effectOwner = this;
            Effects.Add(effect, movesCount);
        }

        public void RemoveEffect(BaseEffect effect) {
            var wrappee = effect.wrappee;
            var wrapper = effect.wrapper;

            if (wrappee != null) {
                wrappee.wrapper = effect.wrapper;
            }
            if (wrapper != null) {
                wrapper.wrappee = effect.wrappee;
            }
            Effects.Remove(effect);
        }

        public BaseEffect GetLastEffect() {
            BaseEffect result = null;
            foreach (BaseEffect key in Effects.Keys) {
                result = key;
            }
            return result;
        }

        public void NewRound() {
            state = State.NotMadeMove;
            fightedBack = false;
            var expiredModifiers = new List<IModifier>();
            var newModifiers = new OrderedDictionary();
            foreach (IModifier key in Modifiers.Keys) {
                if ((double)Modifiers[key] == double.PositiveInfinity) {
                    newModifiers[key] = double.PositiveInfinity;
                }
                else if ((double)Modifiers[key] != double.PositiveInfinity && (int)((double)Modifiers[key]) != 1){
                    newModifiers[key] = (double)((int)((double)Modifiers[key]) - 1);
                }
                else {
                    expiredModifiers.Add(key);
                }
            }
            expiredModifiers.ForEach(mod => RemoveModifier((IModifier)mod));
            Modifiers = newModifiers;

            var expiredEffects = new List<BaseEffect>();
            var newEffects = new OrderedDictionary();
            foreach (BaseEffect key in Effects.Keys) {
                if ((double)Effects[key] == double.PositiveInfinity) {
                    newEffects[key] = double.PositiveInfinity;
                }
                else if ((double)Effects[key] != double.PositiveInfinity && (int)((double)Effects[key]) != 1) {
                    newEffects[key] = (double)((int)((double)Effects[key]) - 1);
                }
                else {
                    expiredEffects.Add(key);
                }
            }
            expiredEffects.ForEach(effect => RemoveEffect(effect));
            Effects = newEffects;

            GetLastEffect()?.NewRound();
        }

        public List<string> GetModifiers() {
            var result = new List<string>();
            foreach (var mod in Modifiers.Keys) {
                result.Add(((IModifier)mod).type);
            }
            return result;
        }
        public List<string> GetEffects() {
            var result = new List<string>();
            foreach (var mod in Effects.Keys) {
                result.Add(((BaseEffect)mod).type);
            }
            return result;
        }

        private OrderedDictionary Modifiers = new OrderedDictionary();
        private OrderedDictionary Effects = new OrderedDictionary();
        public List<ICast> Casts { get; private set; } = new List<ICast>();

        public Unit unitsType { get; }
        public Unit metaUnit {
            get {
                return new Unit(unitsType.type, _metaUnit.hitPoints, _metaUnit.attack, _metaUnit.defence, _metaUnit.damage, _metaUnit.initiative, new List<BaseEffect>(), new List<ICast>());
            }
        }
        private BattleUnit _metaUnit;
        
        public uint hitPoints {
            get { return _metaUnit.hitPoints; }
        }
        public uint attack {
            get { return _metaUnit.attack; }
            set { _metaUnit.attack = value; }
        }
        public int defence {
            get { return _metaUnit.defence; }
            set { _metaUnit.defence = value; }
        }
        public (uint minDamage, uint maxDamage) damage {
            get { return _metaUnit.damage; }
            set { _metaUnit.damage = value; }
        }
        public double initiative {
            get { return _metaUnit.initiative; }
            set { _metaUnit.initiative = value; }
        }

        public uint unitsCount {
            get {
                return (uint)Math.Ceiling((double)totalHitPoints/_metaUnit.hitPoints);
            }
        }
        public bool fightedBack { get; set; }
        public State state { get; set; }
        public bool ressurectable { get; set; } = false;

        public BattleArmy parentArmy { get; set; }

        public uint initHitPoints { get; private set; }
        public uint totalHitPoints { get; private set; }
        public static readonly uint MAXSIZE = uint.Parse(Config.GetValue("BattleUnitsStack:MAXSIZE"));
    }

}