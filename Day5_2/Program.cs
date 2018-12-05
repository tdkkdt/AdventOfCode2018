using System;
using System.Collections.Generic;
using System.IO;

namespace Day5_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            int result = int.MaxValue;
            for(int i = 0; i < 26; i++) {
                char ignoredChar1 = (char)('a' + i);
                char ignoredChar2 = (char)('A' + i);
                reader = new StreamReader(@"..\..\input");
                var list = new LinkedList<char>();
                char c = (char)reader.Read();
                int n = 0;
                while(char.IsLetter(c)) {
                    if(c != ignoredChar1 && c != ignoredChar2) {
                        list.AddLast(c);
                        n++;
                    }
                    c = (char)reader.Read();
                }
                var currentNode = list.First;
                while(currentNode != list.Last && currentNode != null) {
                    if(Math.Abs(currentNode.Value - currentNode.Next.Value) != 32) {
                        currentNode = currentNode.Next;
                    }
                    else {
                        var newCurrentNode = currentNode.Previous ?? currentNode.Next.Next;
                        list.Remove(currentNode.Next);
                        list.Remove(currentNode);
                        currentNode = newCurrentNode;
                        n -= 2;
                    }
                }
                result = Math.Min(n, result);
            }
            writer.Write(result);


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
