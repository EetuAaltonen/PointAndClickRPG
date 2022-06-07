using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityTiles
{
    public static Vector3 LocationToTile(Vector3 location)
    {
        float tileX = RoundToTile(location.x);
        float tileZ = RoundToTile(location.z);

        return new Vector3(tileX, location.y, tileZ);
    }

    public static float RoundToTile(float value)
    {
        float halfGrid = 0.5f;
        float offset = 0;
        float adjust = 0;

        float tile = Mathf.Round(value);
        offset = Mathf.Abs(tile) % (halfGrid * 2);
        adjust = offset < halfGrid ? halfGrid - offset : -(offset - halfGrid);
        tile += tile >= 0 ? adjust : -adjust;

        return tile;
    }
}
