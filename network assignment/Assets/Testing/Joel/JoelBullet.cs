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

    public JoelHealth Shooter;

    void Start()
    {
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        
        spawner = NetworkManager.Multiplayer.GetComponent<Alteruna.Spawner>();
        spawner.ForceSync = true;
    }
    
    // Update is called once per frame
    private void Update() {
        transform.localPosition += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        
        //Food
        if (col.gameObject.transform.parent == null) {
            return;
        }
        
        //Walls
        if (col.gameObject.transform.parent.GetComponentInChildren<JoelHealth>() == null) {
            spawner.Despawn(gameObject);
            return;
        }
        
        //Never works :^(
        if (col.gameObject.transform.parent.GetComponentInChildren<JoelHealth>() == Shooter) {
            spawner.Despawn(gameObject);
            print("SHOT SELF OOPSIE");
            return;
        }

        
        if (col.gameObject.transform.parent.CompareTag("Player")) {
            col.gameObject.transform.parent.Find("Health").GetComponent<JoelHealth>().TakeDamage(damage);
            if (col.gameObject.transform.parent.Find("Health").GetComponent<JoelHealth>().CurHealth <= 0) {
                if (Shooter != null)
                    Shooter.GetComponentInChildren<PlayerScore>().score++;
            }
        }
        
        if (gameObject != null)
            spawner.Despawn(gameObject);
            
    }
}
