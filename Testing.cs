using System;
using System.Collections.Generic;

namespace game {

    static class Testing {
        public static void TestFunctionality() {
            // Unit unit = Angel.Instance;
            // Console.WriteLine(unit);
            
            // var stack = new UnitsStack(Angel.Instance, 30);
            // Console.WriteLine(stack);
            // try {
            //     var errorStack = new UnitsStack(Lich.Instance, 1000000);
            // }
            // catch (GameException ex) {
            //     Console.WriteLine(ex.Message);
            // }

            // var stackList = new List<UnitsStack>() { new UnitsStack(BoneDragon.Instance, 50), stack };
            // Console.WriteLine(string.Join<UnitsStack>(", ", stackList));

            // var army_1 = new Army(stackList);
            // Console.WriteLine(army_1);

            // stack = null; // Проверяем, что в листе и армии ничего не поменялось
            // Console.WriteLine(army_1);

            // var army_2 = new Army(army_1);
            // army_1 = null; // Проверяем, что армия скопировалась и не зависит от первой
            // Console.WriteLine(army_2);

            // try {
            //     var errorArmy = new Army(new UnitsStack(Shaman.Instance, 1), new UnitsStack(Crossbowman.Instance, 1), new UnitsStack(Devil.Instance, 1), new UnitsStack(Cyclops.Instance, 1), new UnitsStack(Skeleton.Instance, 1), new UnitsStack(Hydra.Instance, 1), new UnitsStack(Griffin.Instance, 1));
            // }
            // catch (GameException ex) {
            //     Console.WriteLine(ex.Message);
            // }

            // army_2.AddStack(new UnitsStack(Griffin.Instance, 12));
            // Console.WriteLine(army_2);

            // army_2.RemoveStack(army_2.unitsStackList[1]);
            // Console.WriteLine(army_2);

            // army_2.unitsStackList.Add(new UnitsStack(Hydra.Instance, 100));
            // Console.WriteLine(army_2); // Проверяем, что не можем изменить внутренний лист
            // army_2.unitsStackList[0] = new UnitsStack(Angel.Instance, 40);
            // Console.WriteLine(army_2); // Проверяем, что не можем изменить внутренний лист

            // army_2.Clear();
            // Console.WriteLine(army_2);

            // var battleStack = new BattleUnitsStack(Angel.Instance, 2);
            // battleStack.Damage(193);
            // Console.WriteLine($"{battleStack}\nunitsCount: {battleStack.unitsCount}");

            // battleStack.Damage(500);
            // Console.WriteLine($"{battleStack}\nunitsCount: {battleStack.unitsCount}");

            // battleStack.Heal(200);
            // Console.WriteLine($"{battleStack}\nunitsCount: {battleStack.unitsCount}");

            // try {
            //     battleStack.Heal(180000000);
            // }
            // catch (GameException ex) {
            //     Console.WriteLine(ex.Message);
            // }

            // var convertedStack = new UnitsStack(battleStack);
            // Console.WriteLine(convertedStack);

            // var player1 = new Player(new Army(new UnitsStack(Crossbowman.Instance, 20), new UnitsStack(Shaman.Instance, 32)));
            // var player2 = new Player(new Army(new UnitsStack(Angel.Instance, 2), new UnitsStack(BoneDragon.Instance, 5)));
            // var battle = new Battle(player1, player2);

            // Console.WriteLine(battle.intitativeScale.GetNextStep());
            // Console.WriteLine(battle.intitativeScale.GetWinner());
            // var army = new BattleArmy(new BattleUnitsStack(Angel.Instance, 2));
            // Console.WriteLine(army);
            // army.unitsStackList[0].Damage(20);
            // Console.WriteLine(army);
        }
    }

}