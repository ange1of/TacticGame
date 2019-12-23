using System;

namespace game {

    public interface IModifier {
        string type { get; }
        string ToString();
        string description { get; }
    }

    public interface IDamageModifier : IModifier {
        (uint minDamage, uint MaxDamage) ModifyDamage((uint minDamage, uint MaxDamage) damage);
        (uint minDamage, uint MaxDamage) UnmodifyDamage((uint minDamage, uint MaxDamage) damage);
    }

    public interface IAttackModifier : IModifier {
        uint ModifyAttack(uint attack);
        uint UnmodifyAttack(uint attack);
    }

    public interface IDefenseModifier : IModifier {
        int ModifyDefense(int defence);
        int UnmodifyDefense(int defence);
    }

    public interface IHitPointsModifier : IModifier {
        uint ModifyHitPoints(uint hitPoints);
        uint UnmodifyHitPoints(uint hitPoints);
    }

    public interface IInitiativeModifier : IModifier {
        double ModifyInitiative(double initiative);
        double UnmodifyInitiative(double initiative);
    }
}