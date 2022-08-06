using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItemStack : MonoBehaviour, IInteractable
{
    private string[] m_Interactions;

    [SerializeField]
    private List<string> m_ItemStack = new List<string>();

    public void Awake()
    {
        UpdateInteractionList();
    }

    public TileIndex GetTileIndex()
    {
        return UtilityTiles.LocationToTileIndex(transform.position);;
    }

    public string[] GetInteractionMenu()
    {
        return m_Interactions;
    }

    private void UpdateInteractionList()
    {
        List<string> takeInteractions = new List<string>();
        foreach (var item in m_ItemStack)
        {
            takeInteractions.Add($"Take {item}");
        }
        m_Interactions = takeInteractions.ToArray();
    }
}
