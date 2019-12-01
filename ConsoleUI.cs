using System;
using System.Collections.Generic;

namespace game {
    static class ConsoleUI {
        public static void Print(string str) {
            Console.Write(str);
        }

        public static void PrintLine(string str) {
            Console.WriteLine(str);
        }

        public static void PrintBlock(string str) {
            Console.WriteLine($"\n{str}\n");
        }

        public static void PrintList(string prefix, params string[] arr) {
            foreach (string elem in arr) {
                Console.WriteLine($"{prefix}{elem}");
            }
        }

        public static void PrintList(string prefix, IEnumerable<object> arr) {
            foreach (var elem in arr) {
                Console.WriteLine($"{prefix}{elem.ToString()}");
            }
        }

        public static void PrintNumericList(int startNum, params string[] arr) {
            int i = startNum;
            foreach (string elem in arr) {
                Console.WriteLine($"{i++}: {elem}");
            }
        }

        public static string GetOption() {
            return Console.ReadLine();
        }

        public static int GetNumericOption(int minValue, int maxValue) {
            int chosenAction;
            int.TryParse(Console.ReadLine(), out chosenAction);
            while (!(chosenAction >= minValue && chosenAction <= maxValue)) {
                Console.WriteLine("Incorrect number, try again");
                int.TryParse(Console.ReadLine(), out chosenAction);
            }
            return chosenAction;
        }

    }
}