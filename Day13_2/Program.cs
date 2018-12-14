using System;
using System.Collections.Generic;
using System.IO;

namespace Day13_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        enum Direction {
            Up,
            Left,
            Right,
            Down
        }

        enum RoadType {
            Normal,
            Cross,
            Rotate1,
            Rotate2,
            None,
        }

        enum CrossDirection {
            Left,
            Straight,
            Right
        }

        class Car : IComparable<Car>, IComparable {
            public int X { get; set; }
            public int Y { get; set; }
            public Direction Direction { get; set; }
            public CrossDirection CrossDirection { get; set; }

            public Car(int x, int y, Direction direction) {
                X = x;
                Y = y;
                Direction = direction;
            }

            public int CompareTo(Car other) {
                if(ReferenceEquals(this, other))
                    return 0;
                if(ReferenceEquals(null, other))
                    return 1;
                int yComparison = Y.CompareTo(other.Y);
                if(yComparison != 0)
                    return yComparison;
                return X.CompareTo(other.X);
            }

            public int CompareTo(object obj) {
                if(ReferenceEquals(null, obj))
                    return 1;
                if(ReferenceEquals(this, obj))
                    return 0;
                if(!(obj is Car))
                    throw new ArgumentException($"Object must be of type {nameof(Car)}");
                return CompareTo((Car)obj);
            }

            public static bool operator <(Car left, Car right) {
                return Comparer<Car>.Default.Compare(left, right) < 0;
            }

            public static bool operator >(Car left, Car right) {
                return Comparer<Car>.Default.Compare(left, right) > 0;
            }

            public static bool operator <=(Car left, Car right) {
                return Comparer<Car>.Default.Compare(left, right) <= 0;
            }

            public static bool operator >=(Car left, Car right) {
                return Comparer<Car>.Default.Compare(left, right) >= 0;
            }
        }

        static int maxX = 0;
        static int maxY = 0;

        static void Main(string[] args) {
            List<RoadType[]> map = new List<RoadType[]>();
            List<Car> cars = new List<Car>();
            while(!reader.EndOfStream) {
                var s = reader.ReadLine();
                var roadRow = new RoadType[s.Length];
                maxX = s.Length;
                map.Add(roadRow);
                for(int i = 0; i < s.Length; i++) {
                    switch(s[i]) {
                        case '+':
                            roadRow[i] = RoadType.Cross;
                            break;
                        case '>':
                            cars.Add(new Car(i, maxY, Direction.Right));
                            break;
                        case '<':
                            cars.Add(new Car(i, maxY, Direction.Left));
                            break;
                        case '^':
                            cars.Add(new Car(i, maxY, Direction.Up));
                            break;
                        case 'v':
                            cars.Add(new Car(i, maxY, Direction.Down));
                            break;
                        case ' ':
                            roadRow[i] = RoadType.None;
                            break;
                        case '\\':
                            roadRow[i] = RoadType.Rotate1;
                            break;
                        case '/':
                            roadRow[i] = RoadType.Rotate2;
                            break;
                        default:
                            break;
                    }
                }
                maxY++;
            }

            while(cars.Count > 1) {
                List<Car> forRemove = new List<Car>();
                for(int i = 0; i < cars.Count; i++) {
                    var car = cars[i];
                    MakeMove(car, map);
                    if(map[car.Y][car.X] == RoadType.Cross) {
                        switch(car.Direction) {
                            case Direction.Up:
                                if(car.CrossDirection == CrossDirection.Left) {
                                    car.Direction = Direction.Left;
                                }
                                else if(car.CrossDirection == CrossDirection.Right) {
                                    car.Direction = Direction.Right;
                                }
                                break;
                            case Direction.Left:
                                if(car.CrossDirection == CrossDirection.Left) {
                                    car.Direction = Direction.Down;
                                }
                                else if(car.CrossDirection == CrossDirection.Right) {
                                    car.Direction = Direction.Up;
                                }
                                break;
                            case Direction.Right:
                                if(car.CrossDirection == CrossDirection.Left) {
                                    car.Direction = Direction.Up;
                                }
                                else if(car.CrossDirection == CrossDirection.Right) {
                                    car.Direction = Direction.Down;
                                }
                                break;
                            case Direction.Down:
                                if(car.CrossDirection == CrossDirection.Left) {
                                    car.Direction = Direction.Right;
                                }
                                else if(car.CrossDirection == CrossDirection.Right) {
                                    car.Direction = Direction.Left;
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        car.CrossDirection = (CrossDirection)(((int)car.CrossDirection + 1) % 3);
                    }
                    else if(map[car.Y][car.X] == RoadType.Rotate1 || map[car.Y][car.X] == RoadType.Rotate2) {
                        switch(car.Direction) {
                            case Direction.Up:
                                switch(map[car.Y][car.X]) {
                                    case RoadType.Rotate1:
                                        car.Direction = Direction.Left;
                                        break;
                                    case RoadType.Rotate2:
                                        car.Direction = Direction.Right;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case Direction.Left:
                                switch(map[car.Y][car.X]) {
                                    case RoadType.Rotate1:
                                        car.Direction = Direction.Up;
                                        break;
                                    case RoadType.Rotate2:
                                        car.Direction = Direction.Down;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case Direction.Right:
                                switch(map[car.Y][car.X]) {
                                    case RoadType.Rotate1:
                                        car.Direction = Direction.Down;
                                        break;
                                    case RoadType.Rotate2:
                                        car.Direction = Direction.Up;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case Direction.Down:
                                switch(map[car.Y][car.X]) {
                                    case RoadType.Rotate1:
                                        car.Direction = Direction.Right;
                                        break;
                                    case RoadType.Rotate2:
                                        car.Direction = Direction.Left;
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    for(int j = 0; j < cars.Count; j++) {
                        if(i != j && cars[j].X == car.X && cars[j].Y == car.Y) {
                            forRemove.Add(cars[j]);
                            forRemove.Add(car);
                        }
                    }
                }
                foreach(var carForRemove in forRemove) {
                    var i = cars.IndexOf(carForRemove);
                    if (i >= 0) {
                        cars.RemoveAt(i);
                    }
                }
                cars.Sort();
            }
            writer.Write($"{cars[0].X},{cars[0].Y}");
            writer.Flush();
#if !ONLINE_JUDGE
            writer.Close();
#endif
        }

        static void MakeMove(Car car, List<RoadType[]> map) {
            switch(car.Direction) {
                case Direction.Up:
                    car.Y = car.Y - 1;
                    break;
                case Direction.Left:
                    car.X = car.X - 1;
                    break;
                case Direction.Right:
                    car.X = car.X + 1;
                    break;
                case Direction.Down:
                    car.Y = car.Y + 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
