using System;
using BaseEntities;
using System.IO;
using System.Reflection;

namespace game
{
    class Program
    {
        public static void Main(string[] args)
        {
            var firstPlayer = new Player();
            var secondPlayer = new Player();

            UIStep step = new InitialInterface(firstPlayer, secondPlayer);
            while (step != null) {
                step = step.NextStep(step.Actions());
            }
        }
    }
}
