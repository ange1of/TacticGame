using System;

namespace game {

    class MPunishingStrike : IAttackModifier {
        public uint ModifyAttack(uint baseAttack) {
            return baseAttack + 12;
        }
        public uint UnmodifyAttack(uint baseAttack) {
            return (baseAttack >= 12) ? baseAttack - 12 : 0;
        }
        public ModifierType type { get; } = ModifierType.PunishingStrike;
        
        public override string ToString() {
            return type.ToString();
        }

        private MPunishingStrike() {}
        public static MPunishingStrike Instance {
            get {
                if (instance == null) {
                    instance = new MPunishingStrike();
                }
                return instance;
            }
        }
        private static MPunishingStrike instance;
    }
}