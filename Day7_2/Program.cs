using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day7_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        class Node {
            public char Value { get; }

            public List<Node> Parents { get; }

            public int IsReady { get; set; }

            public int DefaultDuration { get; }

            public Node(char value) {
                Value = value;
                Parents = new List<Node>();
                DefaultDuration = value - 'A' + 61;
                IsReady = DefaultDuration;
            }

            public bool ParentsReady() {
                foreach(Node parent in Parents) {
                    if(parent.IsReady != 0)
                        return false;
                }
                return true;
            }
        }



        static void Main(string[] args) {
            Node[] nodes = new Node[26];
            int n = 0;
            while(!reader.EndOfStream) {
                string[] ss = reader.ReadLine().Split(' ');
                var parentValue = ss[1][0];
                var childValue = ss[7][0];
                Node parent = nodes[parentValue - 'A'];
                if(parent == null) {
                    parent = new Node(parentValue);
                    nodes[parentValue - 'A'] = parent;
                    n++;
                }
                Node childNode = nodes[childValue - 'A'];
                if(childNode == null) {
                    childNode = new Node(childValue);
                    nodes[childValue - 'A'] = childNode;
                    n++;
                }
                childNode.Parents.Add(parent);
            }

            Node[] workers = new Node[5];
            int seconds = 0;
            while (n > 0) {
                for (int i = 0; i < workers.Length; i++) {
                    var worker = workers[i];
                    if (worker == null)
                        continue;
                    worker.IsReady--;
                    if (worker.IsReady == 0) {
                        workers[i] = null;
                        n--;
                    }
                }
                foreach (Node node in nodes) {
                    if (node == null)
                        continue;
                    if (node.IsReady == 0)
                        continue;
                    if (!node.ParentsReady())
                        continue;
                    if (node.IsReady != node.DefaultDuration)
                        continue;
                    for (int i = 0; i < workers.Length; i++) {
                        if (workers[i] == null) {
                            workers[i] = node;
                            break;
                        }
                    }
                }
                seconds++;
            }
            writer.Write(seconds - 1);

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
