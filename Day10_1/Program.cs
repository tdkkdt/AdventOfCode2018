using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Day10_1 {
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

            public int VX { get; }
            public int VY { get; }

            public Point(int x, int y, int vx, int vy) {
                X = x;
                Y = y;
                VX = vx;
                VY = vy;
            }
        }

        static void Main(string[] args) {
            List<Point> points = new List<Point>();
            const int bestAreasCount = 11;
            ulong[][] bestAreas = new ulong[bestAreasCount][];
            for (int i = 0; i < bestAreas.Length; i++) {
                bestAreas[i] = new ulong[2];
                bestAreas[i][0] = UInt64.MaxValue;
            }
            while (!reader.EndOfStream) {
                points.Add(new Point(NextInt(), NextInt(), NextInt(), NextInt()));
            }
            int minX;
            int maxX;
            int minY;
            int maxY;
            for (int i = 0; i < 100000; i++) {
                minX = int.MaxValue;
                maxX = int.MinValue;
                minY = int.MaxValue;
                maxY = int.MinValue;
                foreach (Point point in points) {
                    int x = point.X + i * point.VX;
                    int y = point.Y + i * point.VY;
                    minX = Math.Min(x, minX);
                    minY = Math.Min(y, minY);
                    maxX = Math.Max(x, maxX);
                    maxY = Math.Max(y, maxY);
                }
                ulong area = (ulong)(Math.Abs(maxX - minX) * Math.Abs(maxY - minY));
                if(area < bestAreas[bestAreasCount - 1][0]) {
                    bestAreas[bestAreasCount - 1][0] = area;
                    bestAreas[bestAreasCount - 1][1] = (ulong)i;
                }
                for(int j = bestAreasCount - 1; j > 0 && bestAreas[j - 1][0] > bestAreas[j][0]; j--) {
                    var t = bestAreas[j];
                    bestAreas[j] = bestAreas[j - 1];
                    bestAreas[j - 1] = t;
                }
            }

            int bestI = (int) bestAreas[0][1];
            minX = int.MaxValue;
            maxX = int.MinValue;
            minY = int.MaxValue;
            maxY = int.MinValue;
            foreach (Point point in points) {
                int x = point.X + bestI * point.VX;
                int y = point.Y + bestI * point.VY;
                minX = Math.Min(x, minX);
                minY = Math.Min(y, minY);
                maxX = Math.Max(x, maxX);
                maxY = Math.Max(y, maxY);
            }
            bool[,] map = new bool[Math.Abs(maxX - minX) + 1, Math.Abs(maxY - minY) + 1];
            foreach (Point point in points) {
                int x = point.X + bestI * point.VX;
                int y = point.Y + bestI * point.VY;
                map[x - minX, y - minY] = true;
            }
            for (int y = 0; y < map.GetLength(1); y++) {
                for (int x = 0; x < map.GetLength(0); x++) {
                    bool b = map[x, y];
                    writer.Write(b ? '#' : '.');
                }
                writer.WriteLine();
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
