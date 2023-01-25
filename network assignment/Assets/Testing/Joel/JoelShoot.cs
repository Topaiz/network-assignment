using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class JoelShoot : AttributesSync {
    [SynchronizableField] public int health = 100;

    [SerializeField] public List<GameObject> bullets = new List<GameObject>();
    [SerializeField] private int indexToSpawn;
    [SerializeField] private Transform firepoint;

    public Alteruna.Avatar avatar;
    private Spawner spawner;

    private void Start() {
        spawner = GameObject.FindGameObjectWithTag("Multiplayer").GetComponent<Spawner>();
    }

    private void Update() {
        if (!avatar.IsMe)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
        }
    }

    //Other player can't join if you've shot once??? What 
    void Shoot() {
        bullets.Add(spawner.Spawn(indexToSpawn, firepoint.position, firepoint.rotation, 
            new Vector3(0.1f, 0.1f, 0.1f)));
    }
    
}
