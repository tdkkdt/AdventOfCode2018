using System;
using System.Collections.Generic;
using System.IO;

namespace Day14_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            int inputValue = 190221;
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(7);
            var elf1 = 0;
            var elf2 = 1;
            while (list.Count < inputValue + 10) {
                var v1 = list[elf1];
                var v2 = list[elf2];
                var newValue = v1 + v2;
                if (newValue >= 10) {
                    list.Add(newValue / 10);
                }
                list.Add(newValue % 10);
                elf1 = (elf1 + v1 + 1) % list.Count;
                elf2 = (elf2 + v2 + 1) % list.Count;
            }
            for (int i = 0; i < 10; i++) {
                writer.Write(list[inputValue + i]);
            }
            writer.Flush();
#if !ONLINE_JUDGE
            writer.Close();
#endif
        }
        private static int NextInt() {
            int c;
            int res = 0;
            do {
                c = reader.Read();
                if(c == -1)
                    return res;
            } while(c != '-' && (c < '0' || c > '9'));
            int sign = 1;
            if(c == '-') {
                sign = -1;
                c = reader.Read();
            }
            res = c - '0';
            while(true) {
                c = reader.Read();
                if(c < '0' || c > '9')
                    return res * sign;
                res *= 10;
                res += c - '0';
            }
        }
        private static long NextLong() {
            int c;
            long res = 0;
            do {
                c = reader.Read();
                if(c == -1)
                    return res;
            } while(c != '-' && (c < '0' || c > '9'));
            int sign = 1;
            if(c == '-') {
                sign = -1;
                c = reader.Read();
            }
            res = c - '0';
            while(true) {
                c = reader.Read();
                if(c < '0' || c > '9')
                    return res * sign;
                res *= 10;
                res += c - '0';
            }
        }
    }
}
