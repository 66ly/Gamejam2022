using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TetrisBlockCreator : EditorWindow
{
    private GameObject squarePrefab;
    private string prefabName;
    private string matrixString = "0,0,0,0,0\n0,0,0,0,0\n0,0,0,0,0\n0,0,0,0,0\n0,0,0,0,0";
    private string savePath = "Assets/Prefabs/TetrisBlock.prefab";
    
    [MenuItem("Tools/Tetris Block Creator")]
    public static void ShowWindow()
    {
        GetWindow<TetrisBlockCreator>("Tetris Block Creator");
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        squarePrefab = (GameObject)EditorGUILayout.ObjectField("Square Prefab", squarePrefab, typeof(GameObject), false);

        GUILayout.Label("Prefab Name", EditorStyles.boldLabel);
        prefabName = (string)EditorGUILayout.TextArea(prefabName);

        GUILayout.Label("Matrix (5x5)", EditorStyles.boldLabel);
        matrixString = EditorGUILayout.TextArea(matrixString, GUILayout.Height(100));

        GUILayout.Label("Save Path", EditorStyles.boldLabel);
        savePath = EditorGUILayout.TextField("Prefab Save Path", savePath);

        if (GUILayout.Button("Create Tetris Block"))
        {
            CreateTetrisBlock();
        }
    }

    private void CreateTetrisBlock()
    {
        if (squarePrefab == null)
        {
            Debug.LogError("Square Prefab is not assigned!");
            return;
        }

        GameObject parentObject = new GameObject(prefabName);
        parentObject.AddComponent<Rigidbody2D>();
        parentObject.AddComponent<BoxCollider2D>();
        parentObject.AddComponent<FollowMouse>();
        parentObject.AddComponent<StickObject>();

        string[] rows = matrixString.Split('\n');
        for (int y = 0; y < rows.Length; y++)
        {
            string[] cols = rows[y].Split(',');
            for (int x = 0; x < cols.Length; x++)
            {
                if (cols[x].Trim() == "1")
                {
                    GameObject square = PrefabUtility.InstantiatePrefab(squarePrefab) as GameObject;
                    square.transform.position = new Vector3(x, -y, 0); // Y is negative to make 0,0 the top-left
                    square.transform.SetParent(parentObject.transform, false);
                }
            }
        }

        // Save the created object as a prefab
        string finalPath = AssetDatabase.GenerateUniqueAssetPath(savePath);
        PrefabUtility.SaveAsPrefabAsset(parentObject, finalPath);
        DestroyImmediate(parentObject); // Destroy the temporary object in the scene
        Debug.Log("Tetris block prefab saved to: " + finalPath);
    }
}
