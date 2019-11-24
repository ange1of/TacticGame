using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using Priority_Queue;
using System.Linq;
using System.Collections.Specialized;

namespace game
{
    class Program
    {
        public static void Main(string[] args)
        {
            var firstArmy = new Army(new UnitsStack(Angel.Instance, 3), new UnitsStack(Crossbowman.Instance, 3), new UnitsStack(Skeleton.Instance, 3), new UnitsStack(Hydra.Instance, 3));

            var secondArmy = new Army(new UnitsStack(Griffin.Instance, 3), new UnitsStack(Devil.Instance, 3), new UnitsStack(Lich.Instance, 3), new UnitsStack(BoneDragon.Instance, 3));

            var p1 = new Player(firstArmy, "loler"); // Angel 3, Crossbowman 3, Skeleton 3, Hydra 3
            var p2 = new Player(secondArmy, "keker"); // Griffin 3, Devil 3, Lich 3, BoneDragon 3

            var battle = new Battle(p1, p2);
            battle.Start();
        }
    }
}
