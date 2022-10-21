using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayUtility
{
    public static bool IsContain(this string[] arr, string elem)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == elem)
            {
                return true;
            }
        }
        return false;
    }

}
