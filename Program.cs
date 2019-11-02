using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            // var unit = Angel.Instance;
            var stack = new UnitsStack(Angel.Instance, 30);
            var a = new List<UnitsStack>() { new UnitsStack(Angel.Instance, 50), stack };
            var b = new Army(a);
            Console.WriteLine(b);
            stack = null;
            Console.WriteLine(b);
            var c = new Army(b);
            b = null;
            Console.WriteLine(c);
        }
    }
}
