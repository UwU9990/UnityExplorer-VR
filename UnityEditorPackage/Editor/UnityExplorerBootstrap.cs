using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public static class UnityExplorerBootstrap
{
    [MenuItem("UnityExplorer/Force Setup")]
    public static void ForceSetup()
    {
        Type found = null;
        foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
        {
            found = a.GetType("ExplorerBehaviour") ?? a.GetType("UnityExplorer.ExplorerBehaviour");
            if (found != null) break;
        }
        if (found == null)
        {
            Debug.LogError("ExplorerBehaviour type not found in loaded assemblies.");
            return;
        }
        var m = found.GetMethod("Setup", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (m == null)
        {
            Debug.LogError("Setup() not found on ExplorerBehaviour.");
            return;
        }
        m.Invoke(null, null);
        Debug.Log("Invoked ExplorerBehaviour.Setup()");
    }
}
