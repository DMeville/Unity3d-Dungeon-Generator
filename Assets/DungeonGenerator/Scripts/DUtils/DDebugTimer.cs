using UnityEngine;
using System.Collections;
using System;

public static class DDebugTimer {

    private static bool init = false;
    private static DateTime before;
    private static DateTime now;

    private static void Init(){
        if(!init){
            init = true;
            before = DateTime.Now;
        }
    }

    public static int Start() {
        Init();
        return Lap();
    }

    public static int Lap() {
        now = DateTime.Now;
        int diff = now.Subtract(before).Milliseconds;
        before = now;
        return diff;
    }
}
