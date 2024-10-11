using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ExtensionList
{
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> ExtractRandom<T>(this List<T> list, int amout)
    {
        var r = new List<T>();
        for (int i = 0; i < amout; i++)
        {
            var index = Random.Range(0, list.Count);
            var e = list[index];
            list.RemoveAt(index);
            r.Add(e);
        }
        return r;
    }

    public static T RandomRullete<T>(this List<T> list, Func<T, float> predicate)
    {
        if (list.Count <= 0)
        {
            return default(T);
        }

        var pairs = new List<(T, float)>();
        for (int i = 0; i < list.Count(); i++)
        {
            var value = predicate(list[i]);
            pairs.Add((list[i], value));
        }

        var total = pairs.Sum(p => p.Item2);
        var rand = Random.Range((float)0, total);

        var cur = 0f;
        for (int i = 0; i < pairs.Count; i++)
        {
            cur += pairs[i].Item2;
            if (rand <= cur)
            {
                return pairs[i].Item1;
            }
        }
        return default(T);
    }
}