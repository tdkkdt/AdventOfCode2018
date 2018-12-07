using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day7 {
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

            public bool IsReady { get; set; }

            public Node(char value) {
                Value = value;
                Parents = new List<Node>();
            }

            public bool ParentsReady() {
                foreach (Node parent in Parents) {
                    if (!parent.IsReady)
                        return false;
                }
                return true;
            }
        }



        static void Main(string[] args) {
            Node[] nodes = new Node[26];
            while (!reader.EndOfStream) {
                string[] ss = reader.ReadLine().Split(' ');
                var parentValue = ss[1][0];
                var childValue = ss[7][0];
                Node parent = nodes[parentValue - 'A'];
                if (parent == null) {
                    parent = new Node(parentValue);
                    nodes[parentValue - 'A'] = parent;
                }
                Node childNode = nodes[childValue - 'A'];
                if (childNode == null) {
                    childNode = new Node(childValue);
                    nodes[childValue - 'A'] = childNode;
                }
                childNode.Parents.Add(parent);
            }

            StringBuilder sb = new StringBuilder(26);

            for (int i = 0; i < 26; i++) {
                for (int j = 0; j < 26; j++) {
                    Node node = nodes[j];
                    if (node == null)
                        continue;
                    if (node.IsReady)
                        continue;
                    if (!node.ParentsReady())
                        continue;
                    node.IsReady = true;
                    sb.Append(node.Value);
                    break;
                }
            }
            writer.Write(sb.ToString());
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
