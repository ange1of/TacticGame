using System;

namespace game {

    class ArmyOverflowException : GameException {

        public ArmyOverflowException(string message) : base(message) {}

    }

}