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
            // var firstArmy = new Army(new UnitsStack(Angel.Instance, 3), new UnitsStack(Crossbowman.Instance, 3), new UnitsStack(Lich.Instance, 4), new UnitsStack(Hydra.Instance, 5), new UnitsStack(Fury.Instance, 1));
            // var secondArmy = new Army(new UnitsStack(Griffin.Instance, 3), new UnitsStack(Devil.Instance, 2), new UnitsStack(Cyclops.Instance, 3), new UnitsStack(BoneDragon.Instance, 3), new UnitsStack(Shaman.Instance, 2));

            // var p1 = new Player(firstArmy, "Ovosh");
            // var p2 = new Player(secondArmy, "Frukt");
            // var battle = new Battle(p1, p2);
            // battle.Start(); 
            // foreach (var type in ModHandler.GetAllUnits()) {
            //     Console.WriteLine(type.Name);
            // }
            // Console.WriteLine(Directory.Exists(Path.Join(Environment.CurrentDirectory, Config.GetValue("ModsPath"))));
            // string path = Path.Join(Environment.CurrentDirectory, Config.GetValue("BaseEntitiesPath"));
            // foreach (var t in Assembly.LoadFile(path).GetTypes()) {
            //     Console.WriteLine(t);
            // }
            var g = new Game();
        }
    }
}
