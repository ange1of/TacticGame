using System;

namespace game {

    class MAcceleration : IInitiativeModifier {
        public double ModifyInitiative(double initiative) {
            return initiative * 1.4;
        }
        public double UnmodifyInitiative(double initiative) {
            return initiative / 1.4;
        }
        public ModifierType type { get; } = ModifierType.Acceleration;
        
        public override string ToString() {
            return type.ToString();
        }

        private MAcceleration() {}
        public static MAcceleration Instance {
            get {
                if (instance == null) {
                    instance = new MAcceleration();
                }
                return instance;
            }
        }
        private static MAcceleration instance;
    }
}