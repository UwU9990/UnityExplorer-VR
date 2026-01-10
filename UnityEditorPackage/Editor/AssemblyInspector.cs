using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public static class AssemblyInspector
{
    [MenuItem("UnityExplorer/Debug/Print Loaded Assemblies")]
    public static void PrintLoadedAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Debug.Log($"Loaded assemblies: {assemblies.Length}");
        foreach (var a in assemblies.OrderBy(x => x.FullName))
        {
            try { Debug.Log(a.FullName); } catch (Exception e) { Debug.LogError($"Error listing assembly: {e.Message}"); }
        }
    }

    [MenuItem("UnityExplorer/Debug/Find ExplorerBehaviour")]
    public static void FindExplorerBehaviour()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Debug.Log("Searching for type 'ExplorerBehaviour'...");
        bool foundAny = false;
        foreach (var a in assemblies)
        {
            try
            {
                var t1 = a.GetType("ExplorerBehaviour", false);
                var t2 = a.GetType("UnityExplorer.ExplorerBehaviour", false);
                if (t1 != null || t2 != null)
                {
                    var t = t1 ?? t2;
                    Debug.Log($"Found {t.FullName} in assembly {a.FullName}");
                    foundAny = true;
                    continue;
                }
                foreach (var t in a.GetTypes().Where(tt => tt.Name == "ExplorerBehaviour"))
                {
                    Debug.Log($"Found {t.FullName} in assembly {a.FullName}");
                    foundAny = true;
                }
            }
            catch { }
        }
        if (!foundAny) Debug.Log("Type not found in loaded assemblies.");
    }
}
