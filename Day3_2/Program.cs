﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Day3_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static StreamReader reader = new StreamReader(@"..\..\input");
#endif

        static void Main(string[] args) {
            int[,] map = new int[1000, 1000];
            while(!reader.EndOfStream) {
                int boxID = NextInt();
                int left = NextInt();
                int top = NextInt();
                int width = NextInt();
                int height = NextInt();
                for(int i = 0; i < width; i++) {
                    for(int j = 0; j < height; j++) {
                        map[left + i, top + j]++;
                    }
                }
            }
            reader = new StreamReader(@"..\..\input");
            while(!reader.EndOfStream) {
                int boxID = NextInt();
                int left = NextInt();
                int top = NextInt();
                int width = NextInt();
                int height = NextInt();
                bool good = true;
                for(int i = 0; i < width && good; i++) {
                    for(int j = 0; j < height && good; j++) {
                        good = map[left + i, top + j] == 1;
                    }
                }
                if(good) {
                    writer.Write(boxID);
                    break;
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
