using System;
using System.Collections.Generic;
using System.IO;

namespace Day2_2 {
    class Program {
#if ONLINE_JUDGE
        private static readonly StreamReader reader = new StreamReader(Console.OpenStandardInput(1024 * 10), System.Text.Encoding.ASCII, false, 1024 * 10);
        private static readonly StreamWriter writer = new StreamWriter(Console.OpenStandardOutput(1024 * 10), System.Text.Encoding.ASCII, 1024 * 10);
#else
        private static readonly StreamWriter writer = new StreamWriter(@"..\..\output");
        private static readonly StreamReader reader = new StreamReader(@"..\..\input");
#endif
        class Node {
            public Node Parent { get; }
            public Node[] Children { get; }
            internal char Value { get; }
            public Node(char value, Node parent) {
                Children = new Node[26];
                Value = value;
                Parent = parent;
            }
        }

        static void AddString(Node root, string value) {
            Node current = root;
            foreach (char c in value) {
                int charIndex = c - 'a';
                Node next = current.Children[charIndex];
                if (next == null) {
                    next = new Node(c, current);
                    current.Children[charIndex] = next;
                }
                current = next;
            }
        }

        static string FindSameBoxID(Node current, string value, int position, int differsCount) {
            if (position == value.Length) {
                if (differsCount == 0)
                    return null;
                char[] result = new char[position];
                for (int i = position - 1; i >= 0; i--) {
                    result[i] = current.Value;
                    current = current.Parent;
                }
                return new string(result);
            }
            if (differsCount > 0) {
                int charIndex = value[position] - 'a';
                Node next = current.Children[charIndex];
                return next == null ? null : FindSameBoxID(next, value, position + 1, differsCount);
            }
            foreach (Node child in current.Children) {
                if (child == null)
                    continue;
                var r = FindSameBoxID(child, value, position + 1, differsCount + (child.Value == value[position] ? 0 : 1));
                if (r != null) {
                    return r;
                }
            }
            return null;
        }

        static void Main(string[] args) {
            Node root = new Node('\0', null);
            while (!reader.EndOfStream) {
                var boxID = reader.ReadLine();
                var same = FindSameBoxID(root, boxID, 0, 0);
                if (same == null) {
                    AddString(root, boxID);
                }
                else {
                    for (int i = 0; i < boxID.Length; i++) {
                        if (boxID[i] == same[i]) {
                            writer.Write(boxID[i]);
                        }
                    }
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
