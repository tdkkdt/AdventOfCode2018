using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            string inputValue = "190221";
            int[] inputData = inputValue.Select(c => c - '0').ToArray();
            LinkedList<byte> list = new LinkedList<byte>();
            var elf1 = list.AddFirst(3);
            var elf2 = list.AddLast(7);
            bool answerFound = false;
            while (!answerFound) {
                var v1 = elf1.Value;
                var v2 = elf2.Value;
                var newValue = v1 + v2;
                LinkedListNode<byte> last;
                if (newValue >= 10) {
                    list.AddLast((byte) (newValue / 10));
                    if (list.Count >= inputData.Length) {
                        answerFound = true;
                        last = list.Last;
                        for (int i = inputData.Length - 1; i >= 0; i--) {
                            if (last.Value != inputData[i]) {
                                answerFound = false;
                                break;
                            }
                            last = last.Previous;
                        }
                        if (answerFound) {
                            writer.Write(list.Count - inputData.Length);
                            break;
                        }
                    }
                }
                list.AddLast((byte) (newValue % 10));
                while (v1 != 255) {
                    elf1 = elf1.Next ?? list.First;
                    v1--;
                }
                while (v2 != 255) {
                    elf2 = elf2.Next ?? list.First;
                    v2--;
                }
                if (list.Count < inputData.Length) {
                    continue;
                }
                answerFound = true;
                last = list.Last;
                for(int i = inputData.Length - 1; i >= 0; i--) {
                    if(last.Value != inputData[i]) {
                        answerFound = false;
                        break;
                    }
                    last = last.Previous;
                }
                if(answerFound) {
                    writer.Write(list.Count - inputData.Length);
                }
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
