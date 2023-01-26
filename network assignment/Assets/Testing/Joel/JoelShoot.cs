using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class JoelShoot : MonoBehaviour {
    [SerializeField] public List<GameObject> bullets = new List<GameObject>();
    [SerializeField] private int indexToSpawn;
    [SerializeField] private Transform firepoint;

    [SerializeField] private float maxShootCooldown;
    [SerializeField] private float curShootCooldown;
    [SerializeField] private float baseFireRate;
    public float FireRateModifier;

    public Alteruna.Avatar avatar;
    private Spawner spawner;

    private GameObject[] players;
    private List<GameObject> players2 = new List<GameObject>();

    private JoelRespawn respawn;

    private void Start() {
        spawner = NetworkManager.Multiplayer.gameObject.GetComponent<Spawner>();
        respawn = GameObject.Find("Respawn").GetComponent<JoelRespawn>();
        respawn.Players.Add(gameObject);
    }

    private void Update() {
        if (!avatar.IsMe)
            return;

        if (curShootCooldown > 0) {
            curShootCooldown -= (Time.deltaTime * (baseFireRate + FireRateModifier));
        }
        
        if (Input.GetKey(KeyCode.Mouse0) && curShootCooldown <= 0 && respawn.Players.Count > 1) {
            Shoot();
            curShootCooldown = maxShootCooldown;
        }
    }

    //Other player can't join if you've shot once??? What 
    void Shoot() {
        GameObject bullet = spawner.Spawn(indexToSpawn, firepoint.position, firepoint.rotation,
            new Vector3(0.1f, 0.1f, 0.1f));
        bullet.GetComponent<JoelBullet>().Shooter = transform.parent.gameObject;
        
        bullets.Add(bullet);
    }
    
}
