using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using Priority_Queue;
using System.Linq;
using System.Collections.Specialized;
using System.Text.Json;

namespace game
{
    class Program
    {
        public static void Main(string[] args)
        {
            // var firstArmy = new Army(new UnitsStack(Angel.Instance, 3), new UnitsStack(Crossbowman.Instance, 3), new UnitsStack(Lich.Instance, 4), new UnitsStack(Hydra.Instance, 5));
            // var secondArmy = new Army(new UnitsStack(Griffin.Instance, 3), new UnitsStack(Devil.Instance, 2), new UnitsStack(Lich.Instance, 3), new UnitsStack(BoneDragon.Instance, 3));

            // var p1 = new Player(firstArmy, "Lol"); // Angel 3, Cyclops 3, Lich 4, Hydra 5
            // var p2 = new Player(secondArmy, "Kek"); // Griffin 3, Devil 2, Lich 3, BoneDragon 3
            // var battle = new Battle(p1, p2);
            // battle.Start();
            var a = new BattleUnitsStack(Crossbowman.Instance, 5);
            var b = new BattleUnitsStack()
            Console.WriteLine(JsonSerializer.Serialize(a));
        }
    }
}
