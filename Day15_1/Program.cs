using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15_1 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        class Point:IComparable<Point>, IEquatable<Point> {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj))
                    return false;
                return obj is Point && Equals((Point) obj);
            }

            public bool Equals(Point other) {
                return other != null && (X == other.X && Y == other.Y);
            }

            public override int GetHashCode() {
                unchecked {
                    return (X * 397) ^ Y;
                }
            }
            public int CompareTo(Point other) {
                if (ReferenceEquals(this, other))
                    return 0;
                if (ReferenceEquals(null, other))
                    return 1;
                int yComparison = Y.CompareTo(other.Y);
                if (yComparison != 0)
                    return yComparison;
                return X.CompareTo(other.X);
            }

            public override string ToString() {
                return $"X = {X}, Y = {Y}";
            }
        }

        class Unit : Point, IComparable<Unit> {
            public char C { get; }

            public int Health { get; set; }

            public Unit(int x, int y, char c):base(x, y) {
                X = x;
                Y = y;
                C = c;
                Health = 200;
            }

            public int CompareTo(Unit other) {
                return CompareTo((Point) other);
            }

            public override string ToString() {
                return base.ToString() + $", H = {Health}, C = {C}";
            }
        }

        static int maxx;
        static int maxy;

        static void DisplayMap(List<char[]> map, int roundIndex, List<Unit> units) {
            //Console.Clear();
            //Console.WriteLine(roundIndex);
            //foreach(char[] c in map) {
            //    Console.WriteLine(c);
            //}
            //for(int i = 0; i < units.Count; i++) {
            //    if(i != 0 && units[i - 1].Y != units[i].Y) {
            //        Console.WriteLine();
            //    }
            //    Console.Write($"{units[i].Health} ");
            //}
            //Console.ReadLine();
        }

        static void Main(string[] args) {
            List<char[]> map = new List<char[]>();
            List<Unit> goblins = new List<Unit>();
            List<Unit> elfs = new List<Unit>();
            List<Unit> units = new List<Unit>();
            int j = 0;
            while (!reader.EndOfStream) {
                map.Add(reader.ReadLine().ToCharArray());
                for (int i = 0; i < map[j].Length; i++) {
                    if (map[j][i] == 'G') {
                        Unit unit = new Unit(i, j, 'G');
                        goblins.Add(unit);
                        units.Add(unit);
                    }
                    if (map[j][i] == 'E') {
                        Unit unit = new Unit(i, j, 'E');
                        elfs.Add(unit);
                        units.Add(unit);
                    }
                }
                j++;
            }
            maxx = map[0].Length;
            maxy = map.Count;
            int roundIndex = 0;
            while (goblins.Count != 0 && elfs.Count != 0) {
                for (int i = 0; i < units.Count; i++) {
                    Unit unit = units[i];
                    if (unit.Health > 0)
                        continue;
                    units.RemoveAt(i);
                    i--;
                }
                units.Sort();
                DisplayMap(map, roundIndex, units);
                bool reachEndOfCombat = false;
                foreach (var unit in units) {
                    if (unit.Equals(units[units.Count - 1])) {
                        reachEndOfCombat = true;
                    }
                    if (unit.Health <= 0)
                        continue;
                    List<Unit> enemies = unit.C == 'G' ? elfs : goblins;
                    MakeMove(unit, enemies, map);
                    Attack(unit, enemies);
                    for (int i = 0; i < enemies.Count; i++) {
                        Unit enemy = enemies[i];
                        if (enemy.Health > 0)
                            continue;
                        map[enemy.Y][enemy.X] = '.';
                        enemies.RemoveAt(i);
                        i--;
                    }
                    if (enemies.Count == 0)
                        break;
                }
                if ((goblins.Count == 0 || elfs.Count == 0) && !reachEndOfCombat)
                    break;
                roundIndex++;
            }
            int sum = 0;
            foreach (var goblin in goblins) {
                sum += goblin.Health;
            }
            foreach (var elf in elfs) {
                sum += elf.Health;
            }
            writer.Write((ulong) sum * (ulong) roundIndex);
            writer.Flush();
#if !ONLINE_JUDGE
            writer.Close();
#endif
        }

        static bool Attack(Unit unit, List<Unit> enemies) {
            int[] dx = {0, -1, 1, 0};
            int[] dy = {-1, 0, 0, 1};
            Unit target = null;
            foreach (Unit enemy in enemies) {
                if (enemy.Health <= 0)
                    continue;
                for (int i = 0; i < 4; i++) {
                    int x = unit.X + dx[i];
                    int y = unit.Y + dy[i];
                    if (enemy.X == x && enemy.Y == y && (target == null || enemy.Health < target.Health)) {
                        target = enemy;
                    }
                }
            }
            if (target == null)
                return false;
            target.Health -= 3;
            return true;
        }

        static bool MakeMove(Unit unit, List<Unit> enemies, List<char[]> map) {
            int[] dx = {0, -1, 1, 0};
            int[] dy = {-1, 0, 0, 1};
            HashSet<Point> targets = new HashSet<Point>();
            foreach (Unit enemy in enemies) {
                if (enemy.Health <= 0)
                    continue;
                for (int i = 0; i < 4; i++) {
                    int x = enemy.X + dx[i];
                    int y = enemy.Y + dy[i];
                    if (x < 0 || x >= maxx || y < 0 || y >= maxy)
                        continue;
                    if (map[y][x] == '.' || (x == unit.X && y == unit.Y)) {
                        targets.Add(new Point(x, y));
                    }
                }
            }
            if (targets.Count == 0) {
                return false;
            }
            int[,] movesMap = new int[maxx, maxy];
            for (int index0 = 0; index0 < movesMap.GetLength(0); index0++)
            for (int index1 = 0; index1 < movesMap.GetLength(1); index1++) {
                movesMap[index0, index1] = int.MaxValue;
            }
            movesMap[unit.X, unit.Y] = 0;
            int cp = 1;
            Queue<Point> q = new Queue<Point>();
            Point target = null;
            if (!targets.Contains(unit)) {
                q.Enqueue(unit);
                HashSet<Point> reachedTargets = new HashSet<Point>();
                while (reachedTargets.Count == 0 && q.Count > 0) {
                    int ncp = 0;
                    for (int ci = 0; ci < cp; ci++) {
                        var p = q.Dequeue();
                        int movesCount = movesMap[p.X, p.Y] + 1;
                        for (int i = 0; i < 4; i++) {
                            int x = p.X + dx[i];
                            int y = p.Y + dy[i];
                            if (x < 0 || x >= maxx || y < 0 || y >= maxy)
                                continue;
                            if (movesMap[x, y] <= movesCount)
                                continue;
                            if (map[y][x] != '.')
                                continue;
                            movesMap[x, y] = movesCount;
                            var np = new Point(x, y);
                            if (targets.Contains(np)) {
                                reachedTargets.Add(np);
                            }
                            else if (map[y][x] == '.') {
                                q.Enqueue(np);
                                ncp++;
                            }
                        }
                    }
                    cp = ncp;
                }
                target = reachedTargets.Min();
            }
            else {
                target = unit;
            }
            if (target == null)
                return false;
            if (!unit.Equals(target)) {
                int toTargetMovesCount = movesMap[target.X, target.Y];
                for (int index0 = 0; index0 < movesMap.GetLength(0); index0++)
                for (int index1 = 0; index1 < movesMap.GetLength(1); index1++) {
                    movesMap[index0, index1] = int.MaxValue;
                }
                movesMap[target.X, target.Y] = 0;
                q.Clear();
                q.Enqueue(target);
                cp = 1;
                for (int mi = 0; mi < toTargetMovesCount; mi++) {
                    int ncp = 0;
                    for (int ci = 0; ci < cp; ci++) {
                        var p = q.Dequeue();
                        int movesCount = movesMap[p.X, p.Y] + 1;
                        for (int i = 0; i < 4; i++) {
                            int x = p.X + dx[i];
                            int y = p.Y + dy[i];
                            if (x < 0 || x >= maxx || y < 0 || y >= maxy)
                                continue;
                            if (movesMap[x, y] <= movesCount)
                                continue;
                            if (map[y][x] != '.')
                                continue;
                            movesMap[x, y] = movesCount;
                            var np = new Point(x, y);
                            q.Enqueue(np);
                            ncp++;
                        }
                    }
                    cp = ncp;
                }
                for (int i = 0; i < 4; i++) {
                    int x = unit.X + dx[i];
                    int y = unit.Y + dy[i];
                    if (x < 0 || x >= maxx || y < 0 || y >= maxy)
                        continue;
                    if (movesMap[x, y] == toTargetMovesCount - 1) {
                        target = new Point(x, y);
                        break;
                    }
                }
            }
            map[unit.Y][unit.X] = '.';
            unit.X = target.X;
            unit.Y = target.Y;
            map[unit.Y][unit.X] = unit.C;
            return true;
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
