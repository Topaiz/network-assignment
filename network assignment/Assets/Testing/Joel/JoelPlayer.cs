using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using UnityEngine;

public class JoelPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slideTime;
    [SerializeField] private float maxVelocity;
    [SerializeField] private Vector2 moveDir;

    public GameObject PlayerPrefab;
    
    private Rigidbody2D rb;
    public Alteruna.Avatar avatar;
    private Rigidbody2DSynchronizable rbSync;
    
    private void Start() {
        avatar = GetComponent<Alteruna.Avatar>();
        if (!avatar.IsMe) 
            return;
        
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();

        if (GetComponent<Rigidbody2DSynchronizable>() != null) 
            rbSync = GetComponent<Rigidbody2DSynchronizable>();
    }

    void Update()
    {
        if (!avatar.IsMe) 
            return;
        
        FaceMouse(); 
        Inputs();
        //Movement();
    }
    
    private void FixedUpdate() 
    {
        if (!avatar.IsMe) 
            return;
        
        Schmovement();
    }
    void Inputs() 
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
    
        if (Input.GetMouseButtonDown(0)) {
            //Shoot();
        }
    
        moveDir = new Vector2(moveX, moveY).normalized;
    }
    
    void Schmovement() {
        //Rigid Movement
        //rb.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    
        //rb.velocity = Vector3.Slerp(rb.velocity, moveDir * speed, Time.deltaTime * slideTime);
        rbSync.velocity = new Vector2(Mathf.Lerp(rbSync.velocity.x, moveDir.x * speed, Time.deltaTime * slideTime), 
            Mathf.Lerp(rbSync.velocity.y, moveDir.y * speed, Time.deltaTime * slideTime));
    
        //Smooth Movement
    }

    void Movement() {
        transform.position += new Vector3(moveDir.x * speed * Time.deltaTime, moveDir.y * speed * Time.deltaTime, 0);
    }
    
    //Temporary
    void FaceMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        //transform.up = dir;
        
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        rbSync.rotation = angle - 90;
    }
}
