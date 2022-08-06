#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileIndex : System.IEquatable<TileIndex>
{
    public TileIndex(int x, int z)
    {
        X = x;
        Z = z;
    }

    public int X { get; }
    public int Z { get; }

    public override bool Equals(object? obj) => obj is TileIndex other && this.Equals(other);

    public bool Equals(TileIndex index) => X == index.X && Z == index.Z;

    public override int GetHashCode() => (X, Z).GetHashCode();

    public static bool operator ==(TileIndex index1, TileIndex index2) => index1.Equals(index2);

    public static bool operator !=(TileIndex index1, TileIndex index2) => !(index1 == index2);
}
