using System;

namespace game {

    class MDefense : IDefenseModifier {
        public int ModifyDefense(int baseDefence) {
            return (int)Math.Round(1.3 * baseDefence);
        }
        public int UnmodifyDefense(int baseDefence) {
            return (int)Math.Round(baseDefence / 1.3);
        }
        public ModifierType type { get; } = ModifierType.Defense;
        
        public override string ToString() {
            return type.ToString();
        }

        private MDefense() {}
        public static MDefense Instance {
            get {
                if (instance == null) {
                    instance = new MDefense();
                }
                return instance;
            }
        }
        private static MDefense instance;
    }
}