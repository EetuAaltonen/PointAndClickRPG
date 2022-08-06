using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public TileData(TileIndex tileIndex, GameObject gameObject, string parentCategory)
    {
        m_TileIndex = tileIndex;
        m_GameObject = gameObject;
        m_ParentCategory = parentCategory;
    }

    public TileIndex m_TileIndex { get; set; }
    public GameObject m_GameObject { get; set; }
    public string m_ParentCategory { get; set; }
}
