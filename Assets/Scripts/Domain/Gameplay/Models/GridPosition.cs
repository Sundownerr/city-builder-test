﻿using System;

namespace Domain.Gameplay.Models
{
    [Serializable]
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int X;
        public int Y;

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(GridPosition other) =>
            X == other.X && Y == other.Y;

        public override bool Equals(object obj) =>
            obj is GridPosition other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(X, Y);

        public static bool operator ==(GridPosition left, GridPosition right) =>
            left.Equals(right);

        public static bool operator !=(GridPosition left, GridPosition right) =>
            !left.Equals(right);
    }
}