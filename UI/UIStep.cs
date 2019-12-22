using System;

namespace game {
    public abstract class UIStep {
        public abstract int Actions();
        public abstract UIStep NextStep(int option);
    }
}