using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    public static Multiplayer Multiplayer { get; private set; }
    // private static GameObject _prefab;
    // [SerializeField, HideInInspector] private GameObject prefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // DestroyImmediate(gameObject.GetComponent<Multiplayer>());
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Multiplayer = gameObject.GetComponent<Multiplayer>();
            DontDestroyOnLoad(Instance);
            // SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

//     private void OnSceneLoad(Scene scene, LoadSceneMode mode)
//     {
//         Multiplayer[] multiplayerInstances = FindObjectsOfType<Multiplayer>();
//     
//         gameObject.GetComponent<Multiplayer>().enabled = multiplayerInstances.Length <= 1;
//     }
// #if UNITY_EDITOR
//     private void OnValidate()
//     {
//         _prefab = prefab;
//     }
//
//     private void Reset()
//     {
//         prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
//     }
// #endif
//     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//     private static void OnLoad()
//     {
//         _prefab = Instance.prefab;
//         Instantiate(_prefab);
//     }
}