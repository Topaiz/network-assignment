using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JoelBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Alteruna.Spawner spawner;

    [SerializeField] private int damage = 50;
    [SerializeField] private float speed = 20;

    public GameObject Shooter;

    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        
        spawner = GameObject.FindGameObjectWithTag("Multiplayer").GetComponent<Alteruna.Spawner>();
    }
    
    // Update is called once per frame
    private void Update() {
        transform.localPosition += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.parent.gameObject == Shooter) 
            return;
        
        if (col.gameObject.transform.parent.CompareTag("Player")) {
            col.gameObject.transform.parent.Find("Health").GetComponent<JoelHealth>().TakeDamage(damage);
            if (col.gameObject.transform.parent.Find("Health").GetComponent<JoelHealth>().CurHealth <= 0) {
                if (Shooter != null)
                    Shooter.GetComponentInChildren<PlayerScore>().score++;
            }
        }
        
        //Check list if object is already despawned or whateveridk how to do it help
        if (gameObject != null)
            //Destroy(gameObject);
            spawner.Despawn(gameObject);
            
    }
}
