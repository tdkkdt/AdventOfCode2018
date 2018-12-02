using System;
using System.Collections.Generic;
using System.IO;

namespace Day2_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            int twoTimesCount = 0;
            int threeTimesCount = 0;
            while (!reader.EndOfStream) {
                string boxID = reader.ReadLine();
                int[] charsCount = new int[26];
                int[] countsCount = new int[boxID.Length + 1];
                countsCount[0] = boxID.Length;
                foreach (char c in boxID) {
                    int charIndex = c - 'a';
                    countsCount[charsCount[charIndex]]--;
                    charsCount[charIndex]++;
                    countsCount[charsCount[charIndex]]++;
                }
                if (countsCount[2] > 0)
                    twoTimesCount++;
                if (countsCount[3] > 0)
                    threeTimesCount++;
            }
            writer.Write((ulong) twoTimesCount * (ulong) threeTimesCount);

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
