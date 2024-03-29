﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mayhem
{
    struct Hex : IEquatable<Hex>
    {
        public int Q { get; private set; }
        public int R { get; private set; }

        public Hex[] Neighbors
        {
            get
            {
                return new[]
                {
                    new Hex(Q - 1, R),
                    new Hex(Q, R - 1),
                    new Hex(Q + 1, R - 1),
                    new Hex(Q + 1, R),
                    new Hex(Q, R + 1),
                    new Hex(Q - 1, R + 1)
                };
            }
        }

        public Hex GetNeighbor(int dir)
        {
            switch (dir)
            {
                case 0: return new Hex(Q - 1, R);
                case 1: return new Hex(Q, R - 1);
                case 2: return new Hex(Q + 1, R - 1);
                case 3: return new Hex(Q + 1, R);
                case 4: return new Hex(Q, R + 1);
                case 5: return new Hex(Q - 1, R + 1);
                default: throw new ArgumentOutOfRangeException("dir", "Direction must be 0–5.");
            }
        }

        public int Distance { get { return Math.Max(Math.Abs(Q), Math.Max(Math.Abs(R), Math.Abs(-Q - R))); } }

        public IEnumerable<int> GetEdges(int size)
        {
            // Don’t use ‘else’ because multiple conditions could apply
            if (Q + R == -size)
                yield return 0;
            if (R == -size)
                yield return 1;
            if (Q == size)
                yield return 2;
            if (Q + R == size)
                yield return 3;
            if (R == size)
                yield return 4;
            if (Q == -size)
                yield return 5;
        }

        public override string ToString() { return string.Format("({0}, {1})", Q, R); }

        public Hex(int q, int r) : this() { Q = q; R = r; }

        public bool Equals(Hex other) { return Q == other.Q && R == other.R; }
        public override bool Equals(object obj) { return obj is Hex && Equals((Hex) obj); }
        public static bool operator ==(Hex one, Hex two) { return one.Q == two.Q && one.R == two.R; }
        public static bool operator !=(Hex one, Hex two) { return one.Q != two.Q || one.R != two.R; }
        public override int GetHashCode() { return Q * 47 + R; }

        public static Hex operator +(Hex one, Hex two) { return new Hex(one.Q + two.Q, one.R + two.R); }
        public static Hex operator -(Hex one, Hex two) { return new Hex(one.Q - two.Q, one.R - two.R); }

        public Hex Rotate(int rotation)
        {
            switch (((rotation % 6) + 6) % 6)
            {
                case 0: return this;
                case 1: return new Hex(-R, Q + R);
                case 2: return new Hex(-Q - R, Q);
                case 3: return new Hex(-Q, -R);
                case 4: return new Hex(R, -Q - R);
                case 5: return new Hex(Q + R, -Q);
            }
            throw new ArgumentException("Rotation must be between 0 and 5.", "rotation");
        }
    }
}
