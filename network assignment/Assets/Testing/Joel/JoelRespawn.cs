using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;

public class JoelRespawn : MonoBehaviour {
    public List<GameObject> Players = new List<GameObject>();
    [SerializeField] private List<Transform> spawnpoints = new List<Transform>();
    private Alteruna.Spawner spawner;
    private Alteruna.Avatar avatar;
    private Alteruna.Multiplayer multiplayer;

    [SerializeField] private GameObject playerPrefab;

    private int playerIndex = 0;

    

    private void Awake() {
        spawner = GameObject.Find("Multiplayer").GetComponent<Alteruna.Spawner>();
        multiplayer = GameObject.Find("Multiplayer").GetComponent<Alteruna.Multiplayer>();
    }

    public void Respawn(GameObject player, JoelHealth health) {
        //Destroy(player);
        
        //WE TELEPORT
        player.transform.position = new Vector2(1000, 1000);
        StartCoroutine(Timer(1f, player, health));
    }

    private IEnumerator Timer(float t, GameObject player, JoelHealth health) {
        yield return new WaitForSeconds(t);
        health.CurHealth = health.MaxHealth;
        player.transform.position = spawnpoints[0].position;

    }
    
    //TODO: Why do I get stuck for a second when respawning idk
}
