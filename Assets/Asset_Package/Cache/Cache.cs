using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache<T>
{
    private static Dictionary<Collider, T> dicT = new Dictionary<Collider, T>();

    public static T GetT(Collider collider)
    {
        if (!dicT.ContainsKey(collider))
        {
            dicT.Add(collider, collider.GetComponent<T>());
        }

        return dicT[collider];
    }
}
