using System;
using System.Collections.Generic;
using System.IO;

namespace Day9_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            LinkedList<int> list = new LinkedList<int>();
            int playersCount = NextInt();
            ulong[] playersScore  = new ulong[playersCount];
            ulong maxScore = 0;
            int marblesCount = NextInt() + 1;
            var currentMarble = list.AddFirst(0);
            for (int i = 1; i < marblesCount; i++) {
                if (i % 23 != 0) {
                    currentMarble = list.AddAfter(currentMarble.Next ?? list.First, i);
                }
                else {
                    int playersIndex = i % playersCount;
                    playersScore[playersIndex] += (ulong) i;
                    var bonusScoreNode = currentMarble;
                    for (int j = 0; j < 7; j++) {
                        bonusScoreNode = bonusScoreNode.Previous ?? list.Last;
                    }
                    playersScore[playersIndex] += (ulong) bonusScoreNode.Value;
                    currentMarble = bonusScoreNode.Next ?? list.First;
                    list.Remove(bonusScoreNode);
                    maxScore = Math.Max(playersScore[playersIndex], maxScore);
                }
            }
            writer.Write(maxScore);
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
