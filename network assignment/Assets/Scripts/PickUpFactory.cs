using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class PickUpFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;
    private Alteruna.Spawner spawner;
    Vector3 scale;

    [SerializeField] private float maxTimer;
    [SerializeField] private float curTimer;
    

    private void Awake()
    {
        spawner = NetworkManager.Multiplayer.gameObject.GetComponent<Alteruna.Spawner>();
        spawner.SpawnableObjects.Add(foodPrefab);
        //spawner.ForceSync = true;
        scale = new Vector3(0.5f, 0.5f, 1f);
        curTimer = maxTimer;
    }

    private void Update() {
        curTimer -= Time.deltaTime;

        if (curTimer <= 0) {
            Create(UnityEngine.Random.Range(1,3),new Vector2(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-4f, 4f)));
            curTimer = maxTimer;
        }
    }

    public GameObject Create(int level, Vector3 pos)
    {
        GameObject PickUp = spawner.Spawn(1, pos, Quaternion.identity, scale);
        PickUp.GetComponent<PickUp>().PickUpConstructor(level);

        return PickUp;
    }
}
