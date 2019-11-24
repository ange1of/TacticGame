using System;

namespace game {

    class Defense : IDefenseModifier {
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

        private Defense() {}
        public static Defense Instance {
            get {
                if (instance == null) {
                    instance = new Defense();
                }
                return instance;
            }
        }
        private static Defense instance;
    }
}