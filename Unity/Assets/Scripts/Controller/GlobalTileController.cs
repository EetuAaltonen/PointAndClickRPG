using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTileController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_GlobalParents = new List<GameObject>();

    private List<TileData> m_TileData = new List<TileData>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject globalParent in m_GlobalParents)
        {
            Transform[] childTransforms = globalParent.GetComponentsInChildren<Transform>();
            foreach (Transform child in childTransforms)
            {
                if (child.gameObject.GetInstanceID() != globalParent.GetInstanceID())
                {
                    TileIndex tileIndex = UtilityTiles.LocationToTileIndex(child.position);
                    Debug.Log($"{child.gameObject.name} at {child.position} as {child.gameObject.GetInstanceID()} | Layer: {child.gameObject.layer}");
                    AddTileData(tileIndex, child.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTileData(TileIndex tileIndex, GameObject gameObject)
    {
        m_TileData.Add(new TileData(tileIndex, gameObject));
    }

    public bool RequestTileIndex(TileIndex checkTileIndex, GameObject gameObject)
    {
        TileData CallerTileData = null;
        bool isTileEmpty = true;
        foreach(TileData tileData in m_TileData)
        {
            if (tileData.m_GameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                CallerTileData = tileData;
            }
            if (tileData.m_TileIndex.X == checkTileIndex.X &&
                tileData.m_TileIndex.Z == checkTileIndex.Z)
            {
                isTileEmpty = false;
                var outline = tileData.m_GameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.OutlineWidth = 5;
                }
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
    public TileData(TileIndex tileIndex, GameObject gameObject)
    {
        m_TileIndex = tileIndex;
        m_GameObject = gameObject;
    }

    public TileIndex m_TileIndex { get; set; }
    public GameObject m_GameObject { get; }
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
