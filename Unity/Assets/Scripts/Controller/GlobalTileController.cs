using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTileController : MonoBehaviour
{
    [SerializeField]
    private List<TileData> m_TileData = new List<TileData>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach(GameObject character in gameObjects)
        {
            TileIndex tileIndex = UtilityTiles.LocationToTileIndex(character.transform.position);
            Debug.Log($"{character.name} at {character.transform.position} with {character.GetInstanceID()} | {character.tag}");
            m_TileData.Add(new TileData(tileIndex, character.GetInstanceID(), character.tag));
        }

        gameObjects = GameObject.FindGameObjectsWithTag("Structure");
        foreach(GameObject structure in gameObjects)
        {
            Debug.Log($"{structure.name} at {structure.transform.position} with {structure.GetInstanceID()} | {structure.tag}");
            TileIndex tileIndex = UtilityTiles.LocationToTileIndex(structure.transform.position);
            m_TileData.Add(new TileData(tileIndex, structure.GetInstanceID(), structure.tag));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool RequestTileIndex(TileIndex checkTileIndex, int instanceId)
    {
        TileData CallerTileData = null;
        bool isTileEmpty = true;
        foreach(TileData tileData in m_TileData)
        {
            if (tileData.m_InstanceId == instanceId)
            {
                CallerTileData = tileData;
            }
            if (tileData.m_TileIndex.X == checkTileIndex.X &&
                tileData.m_TileIndex.Z == checkTileIndex.Z)
            {
                isTileEmpty = false;
                break;
            }
        }

        if (isTileEmpty)
        {
            if (CallerTileData != null)
            {
                CallerTileData.m_TileIndex = checkTileIndex;
            }
            else
            {
                isTileEmpty = false;
            }
        }

        return isTileEmpty;
    }
}

public class TileData
{
    public TileData(TileIndex tileIndex, int instanceId, string tag)
    {
        m_TileIndex = tileIndex;
        m_InstanceId = instanceId;
        m_Tag = tag;
    }

    public TileIndex m_TileIndex  { get; set; }
    public int m_InstanceId  { get; }
    public string m_Tag  { get; }
}

public struct TileIndex
{
    public TileIndex(int x, int z)
    {
        X = x;
        Z = z;
    }

    public int X { get; }
    public int Z { get; }

    public override string ToString() => $"({X}, {Z})";
}
