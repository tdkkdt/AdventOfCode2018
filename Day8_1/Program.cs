using System;
using System.Collections.Generic;
using System.IO;

namespace Day8_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            Stack<int> childrenCount = new Stack<int>();
            Stack<int> metadataCount = new Stack<int>();
            ulong sum = 0;
            while (!reader.EndOfStream) {
                childrenCount.Push(NextInt());
                metadataCount.Push(NextInt());
                while(childrenCount.Count > 0 && childrenCount.Peek() == 0) {
                    childrenCount.Pop();
                    int mc = metadataCount.Pop();
                    for(int i = 0; i < mc; i++) {
                        sum += (ulong)NextInt();
                    }
                }
                if (childrenCount.Count > 0)
                    childrenCount.Push(childrenCount.Pop() - 1);
            }
            writer.Write(sum);
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
