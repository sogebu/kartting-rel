using UnityEngine;
using UnityEditor;

public class MeshDuplicator : EditorWindow
{
    [MenuItem("Tools/Mesh Duplicator")]
    public static void ShowWindow()
    {
        GetWindow<MeshDuplicator>("Mesh Duplicator");
    }

    private GameObject sourceObject;

    void OnGUI()
    {
        // If you had a GUIStyle initialization here, it should be safe.
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        myStyle.fontSize = 12;

        GUILayout.Label("Duplicate Mesh", EditorStyles.boldLabel);
        sourceObject = (GameObject)EditorGUILayout.ObjectField("Source Object", sourceObject, typeof(GameObject), true);

        if (GUILayout.Button("Duplicate Mesh", myStyle)) // Example usage
        {
            if (sourceObject != null)
            {
                Mesh sourceMesh = sourceObject.GetComponent<MeshFilter>().sharedMesh;
                if (sourceMesh != null)
                {
                    Mesh newMesh = new Mesh();
                    newMesh.name = sourceMesh.name + "_copy";
                    newMesh.vertices = sourceMesh.vertices;
                    newMesh.triangles = sourceMesh.triangles;
                    newMesh.uv = sourceMesh.uv;
                    newMesh.normals = sourceMesh.normals;
                    newMesh.colors = sourceMesh.colors;
                    newMesh.tangents = sourceMesh.tangents;

                    AssetDatabase.CreateAsset(newMesh, "Assets/" + newMesh.name + ".asset");
                    AssetDatabase.SaveAssets();
                    Debug.Log("Mesh duplicated: " + newMesh.name);
                }
                else
                {
                    Debug.LogError("No Mesh found in Source Object!");
                }
            }
            else
            {
                Debug.LogError("Source Object is null!");
            }
        }
    }
}