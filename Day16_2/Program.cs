using System;
using System.Collections.Generic;
using System.IO;
using Enumerable = System.Linq.Enumerable;

namespace Day16_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif

        enum Instruction {
            addr = 2,
            addi = 14,
            mulr = 6,
            muli = 4,
            banr = 7,
            bani = 11,
            borr = 1,
            bori = 8,
            setr = 12,
            seti = 15,
            gtir = 5,
            gtri = 3,
            gtrr = 13,
            eqir = 0,
            eqri = 9,
            eqrr = 10
        }

        static class VM {
            public static int[] Registers { get; set; }

            static bool CheckRegisterNumber(int r) {
                return 0 <= r && r <= 3;
            }

            public static bool RunInstruction(Instruction instruction, int a, int b, int c) {
                switch(instruction) {
                    case Instruction.addr:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(b) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] + Registers[b];
                        break;
                    case Instruction.addi:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] + b;
                        break;
                    case Instruction.mulr:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(b) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] * Registers[b];
                        break;
                    case Instruction.muli:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] * b;
                        break;
                    case Instruction.banr:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(b) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] & Registers[b];
                        break;
                    case Instruction.bani:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] & b;
                        break;
                    case Instruction.borr:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(b) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] | Registers[b];
                        break;
                    case Instruction.bori:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a] | b;
                        break;
                    case Instruction.setr:
                        if(!CheckRegisterNumber(a) || !CheckRegisterNumber(c))
                            return false;
                        Registers[c] = Registers[a];
                        break;
                    case Instruction.seti:
                        if(!CheckRegisterNumber(c))
                            return false;
                        Registers[c] = a;
                        break;
                    case Instruction.gtir:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(b)) {
                            return false;
                        }
                        Registers[c] = a > Registers[b] ? 1 : 0;
                        break;
                    case Instruction.gtri:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(a)) {
                            return false;
                        }
                        Registers[c] = Registers[a] > b ? 1 : 0;
                        break;
                    case Instruction.gtrr:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(a) || !CheckRegisterNumber(b)) {
                            return false;
                        }
                        Registers[c] = Registers[a] > Registers[b] ? 1 : 0;
                        break;
                    case Instruction.eqir:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(b)) {
                            return false;
                        }
                        Registers[c] = a == Registers[b] ? 1 : 0;
                        break;
                    case Instruction.eqri:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(a)) {
                            return false;
                        }
                        Registers[c] = Registers[a] == b ? 1 : 0;
                        break;
                    case Instruction.eqrr:
                        if(!CheckRegisterNumber(c) || !CheckRegisterNumber(a) || !CheckRegisterNumber(b)) {
                            return false;
                        }
                        Registers[c] = Registers[a] == Registers[b] ? 1 : 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(instruction), instruction, null);
                }
                return true;
            }
        }

        static void Main(string[] args) {
            int ans = 0;
            HashSet<Instruction>[] ops = new HashSet<Instruction>[16];
            VM.Registers = new int[4];
            while (!reader.EndOfStream) {
                int op = NextInt();
                int a = NextInt();
                int b = NextInt();
                int c = NextInt();
                VM.RunInstruction((Instruction) op, a, b, c);
            }
            writer.Write(VM.Registers[0]);
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
