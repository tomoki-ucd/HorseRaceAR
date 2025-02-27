using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomLogger
{
    public static void Print(object obj, string message)
    {
        Debug.Log($"[{obj.GetType().Name}] {message}");
    }
}
