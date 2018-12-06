using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        class Point {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y) {
                X = x;
                Y = y;
            }
        }

        static void Main(string[] args) {
            List<Point> points = new List<Point>();
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;
            while(!reader.EndOfStream) {
                int x = NextInt();
                int y = NextInt();
                points.Add(new Point(x, y));
                minX = Math.Min(x, minX);
                minY = Math.Min(y, minY);
                maxX = Math.Max(x, maxX);
                maxY = Math.Max(y, maxY);
            }
            bool[] areaOnInfinite = new bool[points.Count];
            int[] counts = new int[points.Count];
            for(int x = minX; x <= maxX; x++) {
                for(int y = minY; y <= maxY; y++) {
                    int minI = -1;
                    int minValue = Int32.MaxValue;
                    for(int i = 0; i < points.Count; i++) {
                        var distance = Math.Abs(x - points[i].X) + Math.Abs(y - points[i].Y);
                        if(distance < minValue) {
                            minValue = distance;
                            minI = i;
                        }
                    }
                    for(int i = 0; i < points.Count; i++) {
                        var distance = Math.Abs(x - points[i].X) + Math.Abs(y - points[i].Y);
                        if(distance == minValue && minI != i) {
                            minI = -1;
                            break;
                        }
                    }
                    if(minI != -1) {
                        if(x == minX || x == maxX || y == minY || y == maxY) {
                            areaOnInfinite[minI] = true;
                        }
                        counts[minI]++;
                    }
                }
            }
            int maxCounts = 0;
            for(int index = 0; index < counts.Length; index++) {
                int count = counts[index];
                if(areaOnInfinite[index])
                    continue;
                maxCounts = Math.Max(count, maxCounts);
            }
            writer.Write(maxCounts);
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
