using System;

namespace game {

    class MCurse : IAttackModifier {
        public uint ModifyAttack(uint baseAttack) {
            return (baseAttack >= 12) ? baseAttack - 12 : 0;
        }
        public uint UnmodifyAttack(uint baseAttack) {
            return baseAttack + 12;
        }
        public ModifierType type { get; } = ModifierType.Curse;
        
        public override string ToString() {
            return type.ToString();
        }

        private MCurse() {}
        public static MCurse Instance {
            get {
                if (instance == null) {
                    instance = new MCurse();
                }
                return instance;
            }
        }
        private static MCurse instance;
    }
}