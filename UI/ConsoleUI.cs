using System;
using System.Collections.Generic;
using System.Threading;

namespace game {
    static class ConsoleUI {

        public static void NewScreen(int sleepTime = 500) {
            Thread.Sleep(sleepTime);
            Console.Clear();
        }

        public static void PrintList(string prefix="", params string[] arr) {
            foreach (string elem in arr) {
                Console.WriteLine($"{prefix}{elem}");
            }
        }

        public static void PrintList(IEnumerable<object> arr, string prefix="") {
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

        public static void PrintNumericList(int startNum, IEnumerable<string> arr) {
            int i = startNum;
            foreach (string elem in arr) {
                Console.WriteLine($"{i++}: {elem}");
            }
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