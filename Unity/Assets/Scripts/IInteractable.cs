using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    TileIndex GetTileIndex();

    string[] GetInteractionMenu();
}
