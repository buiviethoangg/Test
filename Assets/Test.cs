using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.GraphicsBuffer;

public class Test : MonoBehaviour
{
    [SerializeField] private AssetReference _obj;
    [SerializeField] private AssetLabelReference _objLayble;


    AsyncOperationHandle handle;
    private void Start()
    {
        
    }
    public void Spawn()
    {
        handle = Addressables.LoadAssetsAsync<GameObject>(_objLayble, (GameObject result) =>
        {
            Instantiate(result);
        });
    }

    public void DeSpawn()
    {
        Addressables.Release(handle);
    }
}

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Test))]
public class UnitEditor : Editor
{
    public SerializedProperty Link;
    public SerializedProperty ModelName;
    public SerializedProperty ColorOutline;
    public SerializedProperty SizeLine;
    Test script;
    void OnEnable()
    {
        script = (Test)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
        if (GUILayout.Button("Test"))
        {
            Spawn();
        }   
        if (GUILayout.Button("DeSpawn"))
        {
            DeSpawn();
        }

        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }

    private void Spawn()
    {
        script.Spawn();
    }  
    private void DeSpawn()
    {
        script.DeSpawn();
    }
}
#endif