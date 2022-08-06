using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityGameObjects
{
    public static void DeleteAllChild(Transform parentTransform)
    {
        foreach (Transform child in parentTransform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
