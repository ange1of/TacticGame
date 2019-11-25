using System;

namespace game {

    interface IModifier {
        ModifierType type { get; }
        string ToString();
    }

    interface IDamageModifier : IModifier {
        (uint minDamage, uint MaxDamage) ModifyDamage((uint minDamage, uint MaxDamage) damage);
        (uint minDamage, uint MaxDamage) UnmodifyDamage((uint minDamage, uint MaxDamage) damage);
    }

    interface IAttackModifier : IModifier {
        uint ModifyAttack(uint attack);
        uint UnmodifyAttack(uint attack);
    }

    interface IDefenseModifier : IModifier {
        int ModifyDefense(int defence);
        int UnmodifyDefense(int defence);
    }

    interface IHitPointsModifier : IModifier {
        uint ModifyHitPoints(uint hitPoints);
        uint UnmodifyHitPoints(uint hitPoints);
    }

    interface IInitiativeModifier : IModifier {
        double ModifyInitiative(double initiative);
        double UnmodifyInitiative(double initiative);
    }

    enum ModifierType {
        Defense,
        Curse,
        PunishingStrike,
        Weakening,
        Acceleration,
        Resurrection
    }
}