using System;
using System.Collections;
using System.Collections.Generic;


public static class ListExtensions  {
    public static void Shuffle<T>(this IList<T> list, Random rnd) {
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j) {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public static bool WithinRange<T>(this IList<T> list, int index){
        if (index >= 0 && index < list.Count) return true;
        else return false;
    }
}
