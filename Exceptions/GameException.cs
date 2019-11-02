using System;

namespace game {
    /* 
     * Base class for exceptions in Game
     */
    class GameException : Exception {

        public GameException(string message) : base (message) {
        }

    }
}