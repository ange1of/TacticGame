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
            var firstArmy = new Army(new UnitsStack(Angel.Instance, 3), new UnitsStack(Crossbowman.Instance, 3), new UnitsStack(Lich.Instance, 4), new UnitsStack(Hydra.Instance, 5), new UnitsStack(Fury.Instance, 1));
            var secondArmy = new Army(new UnitsStack(Griffin.Instance, 3), new UnitsStack(Devil.Instance, 2), new UnitsStack(Cyclops.Instance, 3), new UnitsStack(BoneDragon.Instance, 3), new UnitsStack(Shaman.Instance, 2));

            var p1 = new Player(firstArmy, "Lol");
            var p2 = new Player(secondArmy, "Kek");
            var battle = new Battle(p1, p2);
            battle.Start();

            // BaseEffect a = new EShooter();
            // BaseEffect b = new ECleanShot();
            // BaseEffect c = new EUndead();
            // Console.WriteLine($"{a.GetType()}\n{b.GetType()}");
            // var stack = new BattleUnitsStack(Angel.Instance, 3);
            // stack.AddModifier(MDefense.Instance, 1);
            // stack.NewRound();

            // var devil = new BattleUnitsStack(Devil.Instance, 1);
            // var fury = new BattleUnitsStack(Fury.Instance, 4);

            // Action.Attack(fury, devil);
            // Console.WriteLine($"{fury}\n{devil}");
        }
    }
}
