using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTileDataController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_GlobalParents = new List<GameObject>();

    private List<TileData> m_TileData = new List<TileData>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject globalParent in m_GlobalParents)
        {
            foreach (Transform child in globalParent.transform)
            {
                TileIndex tileIndex = UtilityTiles.LocationToTileIndex(child.position);
                //Debug.Log($"{child.gameObject.name} at {child.position} as {child.gameObject.GetInstanceID()} | Layer: {child.gameObject.layer}");

                AddTileData(tileIndex, child.gameObject, globalParent.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTileData(TileIndex tileIndex, GameObject gameObject, string parentCategory)
    {
        m_TileData.Add(new TileData(tileIndex, gameObject, parentCategory));
    }

    public TileData[] GetTileDataByIndex(TileIndex tileIndex)
    {
        List<TileData> foundTileData = new List<TileData>();

        foreach(TileData tileData in m_TileData)
        {
            if (tileData.m_TileIndex.Equals(tileIndex))
            {
                foundTileData.Add(tileData);
            }
        }
        return foundTileData.ToArray();
    }

    public TileData GetTileDataByInstanceId(int instanceID)
    {
        foreach(TileData tileData in m_TileData)
        {
            if (tileData.m_GameObject.GetInstanceID() == instanceID)
            {
                return tileData;
            }
        }
        return null;
    }

    public bool RequestTileIndexChange(TileIndex originalTileIndex, TileIndex targetTileIndex, int instanceID)
    {
        TileData originalTileData = GetTileDataByInstanceId(instanceID);
        TileData[] targetTileData = GetTileDataByIndex(targetTileIndex);
        bool isTileEmpty = true;

        foreach(TileData tileData in targetTileData)
        {
            if (tileData.m_ParentCategory != "GlobalGroundItems")
            {
                isTileEmpty = false;
                break;
            }
        }

        if (isTileEmpty)
        {
            if (originalTileData != null)
            {
                originalTileData.m_TileIndex = targetTileIndex;
            }
            else
            {
                isTileEmpty = false;
                Debug.Log($"Caller object tile data not found {gameObject.name}");
            }
        }

        return isTileEmpty;

        /*TileData CallerTileData = null;
        bool isTileEmpty = true;
        foreach(TileData tileData in m_TileData)
        {
            if (tileData.m_GameObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                CallerTileData = tileData;

                if (!isTileEmpty) break;
            }
            if (tileData.m_TileIndex.X == checkTileIndex.X &&
                tileData.m_TileIndex.Z == checkTileIndex.Z && tileData.m_GameObject != null)
            {
                isTileEmpty = false;
                var outline = tileData.m_GameObject.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.OutlineWidth = 5;
                }

                if (CallerTileData != null) break;
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
                Debug.Log($"Caller object tile data not found {gameObject.name}");
            }
        }

        return isTileEmpty;*/
    }

    public string[] RequestTileInteractions(TileIndex tileIndex)
    {
        List<string> interactions = new List<string>();
        TileData[] tileData = GetTileDataByIndex(tileIndex);

        foreach(TileData data in tileData)
        {
            if (data.m_GameObject != null)
            {
                IInteractable interactionComponent = data.m_GameObject.GetComponent<IInteractable>();
                if (interactionComponent != null)
                {
                    interactions.AddRange(interactionComponent.GetInteractionMenu());
                }
            }
        }

        return interactions.ToArray();
    }
}