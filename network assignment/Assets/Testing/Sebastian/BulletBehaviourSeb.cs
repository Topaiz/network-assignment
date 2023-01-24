using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourSeb : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 1f;
    void Start()
    {
        Invoke(nameof(DestroyInstance), lifetime);
    }

    void Update()
    {
        transform.Translate(transform.up * (speed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

    private void DestroyInstance()
    {
        Destroy(gameObject);
    }
}
