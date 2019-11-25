using System;

namespace game {

    class MWeakening : IDefenseModifier {
        public int ModifyDefense(int baseDefense) {
            return (baseDefense >= 12) ? baseDefense - 12 : 0;
        }
        public int UnmodifyDefense(int baseDefense) {
            return baseDefense + 12;
        }
        public ModifierType type { get; } = ModifierType.Weakening;
        
        public override string ToString() {
            return type.ToString();
        }

        private MWeakening() {}
        public static MWeakening Instance {
            get {
                if (instance == null) {
                    instance = new MWeakening();
                }
                return instance;
            }
        }
        private static MWeakening instance;
    }
}