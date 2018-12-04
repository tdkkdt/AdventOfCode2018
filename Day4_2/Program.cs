using System;
using System.Collections.Generic;
using System.IO;

namespace Day4_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        public class Event {
            public DateTime DT { get; }
            public string EventMessage { get; }

            public Event(DateTime dt, string eventMessage) {
                DT = dt;
                EventMessage = eventMessage;
            }
        }

        static void Main(string[] args) {
            List<Event> events = new List<Event>();
            while(!reader.EndOfStream) {
                int year = NextInt();
                int month = NextInt();
                int day = NextInt();
                int hour = NextInt();
                int minute = NextInt();
                reader.Read(); //space
                string message = reader.ReadLine();
                events.Add(new Event(new DateTime(year, month, day, hour, minute, 0), message));
            }

            events.Sort((a, b) => a.DT.CompareTo(b.DT));

            Dictionary<int, int> sleepCount = new Dictionary<int, int>();
            Dictionary<int, int[]> sleepMap = new Dictionary<int, int[]>();
            int currentGuard = -1;
            DateTime whenFalls = new DateTime();
            int bestGuard = -1;
            int bestMinute = -1;
            int bestMinuteValue = 0;
            foreach(var e in events) {
                if(e.EventMessage.StartsWith("Guard")) {
                    var ss = e.EventMessage.Split(new[] { ' ', '#' }, StringSplitOptions.RemoveEmptyEntries);
                    int guardID = int.Parse(ss[1]);
                    currentGuard = guardID;
                    continue;
                }
                if(e.EventMessage.StartsWith("falls")) {
                    whenFalls = e.DT;
                    continue;
                }
                if(e.EventMessage.StartsWith("wakes")) {
                    int newSleep = e.DT.Subtract(whenFalls).Minutes;
                    int[] map;
                    int count;
                    if(!sleepCount.TryGetValue(currentGuard, out count)) {
                        sleepCount.Add(currentGuard, newSleep);
                        map = new int[60];
                        sleepMap.Add(currentGuard, map);
                    }
                    else {
                        sleepCount[currentGuard] = count + newSleep;
                        map = sleepMap[currentGuard];
                    }
                    for(int i = 0; i < newSleep; i++) {
                        map[whenFalls.Minute + i]++;
                        if (bestGuard == -1 || map[whenFalls.Minute + i] > bestMinuteValue) {
                            bestMinuteValue = map[whenFalls.Minute + i];
                            bestMinute = whenFalls.Minute + i;
                            bestGuard = currentGuard;
                        }
                    }
                }
            }
            writer.Write(bestGuard * bestMinute);
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
