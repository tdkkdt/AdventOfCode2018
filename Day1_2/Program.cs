using System;
using System.Collections.Generic;
using System.IO;

namespace Day1_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            int frequency = 0;
            HashSet<int> was = new HashSet<int>();
            was.Add(0);
            int? result = null;
            while (!result.HasValue) {
                reader = new StreamReader(@"..\..\input");
                while (!reader.EndOfStream) {
                    int number = NextInt();
                    frequency += number;
                    if (!was.Add(frequency)) {
                        result = frequency;
                        break;
                    }
                }
            }
            writer.Write(result.Value);
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
            } while(c != '-' && c != '+' && (c < '0' || c > '9'));
            int sign = 1;
            if(c == '-') {
                sign = -1;
            }
            c = reader.Read();
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
