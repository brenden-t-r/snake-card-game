using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }

    public static List<T> ShuffleFisherYates<T>(List<T> list)
    {
        for (int i = list.Count-1; i > 0; i--)
        {
            int rnd = Random.Range(0,i);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }

        return list;
    }
}
