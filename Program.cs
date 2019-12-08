using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using Priority_Queue;
using System.Linq;
using System.Collections.Specialized;
using System.Text.Json;
using System.IO;

namespace game
{
    class Program
    {
        public static void Main(string[] args)
        {
            // var firstArmy = new Army(new UnitsStack(Angel.Instance, 3), new UnitsStack(Crossbowman.Instance, 3), new UnitsStack(Lich.Instance, 4), new UnitsStack(Hydra.Instance, 5), new UnitsStack(Fury.Instance, 1));
            // var secondArmy = new Army(new UnitsStack(Griffin.Instance, 3), new UnitsStack(Devil.Instance, 2), new UnitsStack(Cyclops.Instance, 3), new UnitsStack(BoneDragon.Instance, 3), new UnitsStack(Shaman.Instance, 2));

            // var p1 = new Player(firstArmy, "Ovosh");
            // var p2 = new Player(secondArmy, "Frukt");
            // var battle = new Battle(p1, p2);
            // battle.Start();

            // string unitFilesPath = Path.Join(Environment.CurrentDirectory, @"Units\");
            // if (Directory.Exists(unitFilesPath)) {
            //     var a = Directory.EnumerateFiles(unitFilesPath, "*.cs");
            //     Console.WriteLine(string.Join("\n", a));
            // }
            // else {
            //     Console.WriteLine("NOPE");
            // }
            
            // string path = Path.Join(Environment.CurrentDirectory, @"Units\");
            foreach (var t in typeof(Unit).Assembly.GetTypes()) {
                if (t.IsSubclassOf(typeof(Unit))) {
                    Console.WriteLine(t.Name);
                }
            }
        }
    }
}
