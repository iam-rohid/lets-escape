using System;
using UnityEngine;

public class LogService : MonoBehaviour
{
    public static void Log(object message)
    {
        DateTime now = DateTime.Now;
        Debug.Log($"{now} - {message}");
    }

    public static void LogError(object message)
    {
        DateTime now = DateTime.Now;
        Debug.LogError($"{now} - {message}");
    }

    public static void LogWarning(object message)
    {
        DateTime now = DateTime.Now;
        Debug.LogWarning($"{now} - {message}");
    }
}