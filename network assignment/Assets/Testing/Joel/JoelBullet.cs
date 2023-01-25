using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoelBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Alteruna.Spawner spawner;

    [SerializeField] private int damage = 50;

    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        
        spawner = GameObject.FindGameObjectWithTag("Multiplayer").GetComponent<Alteruna.Spawner>();
    }
    
    // Update is called once per frame
    private void Update() {
        transform.localPosition += transform.up * 10 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.transform.parent.CompareTag("Player")) {
            col.gameObject.transform.parent.Find("Health").GetComponent<JoelHealth>().TakeDamage(damage);
            print("OUCH!");
        }
        
        //Check list if object is already despawned or whatever
        if (gameObject != null)
            spawner.Despawn(gameObject);
    }
}
