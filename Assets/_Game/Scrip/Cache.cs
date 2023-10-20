using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    public static Dictionary<Collider, Character> dictScript = new Dictionary<Collider, Character>();
    public static Character GetSccrip(Collider col)
    {
        if (!dictScript.ContainsKey(col))
        {
            Character scrip = col.transform.GetComponent<Character>();
            dictScript.Add(col, scrip);
        }
        return dictScript[col];
    }
}