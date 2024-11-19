using UnityEditor;
using UnityEngine;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissingScriptsInScene()
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject go in objects)
        {
            Component[] components = go.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script found on GameObject '{go.name}' in scene '{go.scene.name}'.", go);
                    count++;
                }
            }
        }

        Debug.Log($"Checked {objects.Length} GameObjects. Found {count} missing scripts.");
    }
}