using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LogUtils
{
    static string TAG = "Merge: ";
    static bool isLogged = true;

    public static void Log(object mess)
    {
        if (isLogged)
            Debug.Log(TAG + mess);
    }

}
