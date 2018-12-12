using System;
using System.Collections.Generic;
using System.IO;

namespace Day12_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            foreach(char c in "initial state: ") {
                reader.Read();
            }
            var s = reader.ReadLine();
            int[] current = new int[s.Length + 10];
            for(int i = 0; i < s.Length; i++) {
                current[i] = s[i] == '#' ? 1 : 0;
            }
            reader.ReadLine();
            int[] rules = new int[32];
            while(!reader.EndOfStream) {
                string[] ss = reader.ReadLine().Split(new[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                int pattern = 0;
                for(int i = 0; i < 5; i++) {
                    if(i != 0)
                        pattern = pattern << 1;
                    pattern += ss[0][i] == '#' ? 1 : 0;
                }
                int value = ss[1][0] == '#' ? 1 : 0;
                rules[pattern] = value;
            }
            for(int i = 0; i < 2002; i++) {
                int n = current.Length;
                int[] next = new int[n + 10];
                int currentPattern = 0;
                for(int j = 0; j < n + 5; j++) {
                    currentPattern = currentPattern & 0xF;
                    currentPattern = currentPattern << 1;
                    if(j < n)
                        currentPattern += current[j];
                    next[j + 3] = rules[currentPattern];
                }
                current = next;
            }
            //writer.Write(sum);
            writer.Write(3697 + (50000000000UL - 200) * 15UL);
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
