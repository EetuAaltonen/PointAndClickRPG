using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string[] m_Interactions;

    private string[] m_DefaultInteractions = {
        "Walk Here",
        "Inspect",
        "Cancel"
    };

    public void Awake()
    {
        m_Interactions = m_Interactions.Concat(m_DefaultInteractions).ToArray();
    }

    public TileIndex GetTileIndex()
    {
        return UtilityTiles.LocationToTileIndex(transform.position);;
    }

    public string[] GetInteractionMenu()
    {
        return m_Interactions;
    }
}
