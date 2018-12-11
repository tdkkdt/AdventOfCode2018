using System;
using System.Collections.Generic;
using System.IO;

namespace Day11_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        static int GetPowerLevel(int x, int y, int SN) {
            var rackID = x + 10;
            var powerLevel = rackID * y;
            powerLevel += SN;
            powerLevel *= rackID;
            powerLevel = (powerLevel / 100) % 10;
            powerLevel -= 5;
            return powerLevel;
        }


        static void Main(string[] args) {
            int SN = 6042;
            int maxSum = int.MinValue;
            int maxX = -1;
            int maxY = -1;
            for (int x = 0; x < 300 - 3; x++) {
                for (int y = 0; y < 300 - 3; y++) {
                    int sum = 0;
                    for (int i = 0; i < 3; i++) {
                        for (int j = 0; j < 3; j++) {
                            int pl = GetPowerLevel(x + i, y + j, SN);
                            sum += pl;
                            if (sum > maxSum) {
                                maxSum = sum;
                                maxX = x;
                                maxY = y;
                            }
                        }
                    }
                }
            }
            writer.Write($"{maxX},{maxY}");
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
