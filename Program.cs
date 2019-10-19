using System;
using System.Collections.Generic;


namespace game {

    class Program {
        static void Main(string[] args) {
            // Создаем юнитов разных типов
            var lich = new Lich();
            var angel = new Angel();
            var devil = new Devil();
            var fury = new Fury();
            var skeleton = new Skeleton();
            var shaman = new Shaman();
            var griffin = new Griffin();

            Console.WriteLine(angel.PrintUnit()+"\n");
            Console.WriteLine(fury.PrintUnit()+"\n");

            // Создаем стеки, которые не должны падать
            var stack1 = new UnitsStack(lich, 10);
            Console.WriteLine(stack1.PrintStack());

            var stack2 = new UnitsStack(angel, 2);
            Console.WriteLine(stack2.PrintStack());

            var stack3 = new UnitsStack(devil, 30);
            Console.WriteLine(stack3.PrintStack());

            var stack4 = new UnitsStack(fury, 85);
            Console.WriteLine(stack4.PrintStack());

            try {
                // Должно упасть, т.к. больше 999999
                var stack5 = new UnitsStack(skeleton, 1000000);
                Console.WriteLine(stack5.PrintStack());
            }
            catch (Exception e) {
                Console.WriteLine(e.Message+"\n");
            }

            var list = new List<UnitsStack>();

            var army = new Army(list); // Пустая армия
            Console.WriteLine(army.PrintArmy()+"\n");

            list.AddRange(new UnitsStack[] {stack1, stack2, stack3, stack4});
            
            army = new Army(list); // Армия с 4 стеками
            Console.WriteLine(army.PrintArmy()+"\n");

            list.Add(stack1);
            list.Add(stack2);
            list.Add(stack3);

            try {
                // Падает, т.к. в листе 7 стеков
                army = new Army(list);
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            // var list = new List<UnitsStack>() {stack1, stack2, stack3};
            // var army = new Army(list);
            // Console.WriteLine(army.PrintArmy()+"\n");
            // list.Add(stack4);
            // Console.WriteLine(army.PrintArmy());
        }
    }
}
