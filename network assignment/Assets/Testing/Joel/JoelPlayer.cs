using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoelPlayer : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slideTime;
    [SerializeField] private float maxVelocity;
    [SerializeField] private Vector2 moveDir;
    
    private Rigidbody2D rb;
    private Alteruna.Avatar avatar;
    
    private void Start() {
        avatar = GetComponent<Alteruna.Avatar>();
        if (!avatar.IsMe) 
            return;
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!avatar.IsMe) 
            return;
        
        FaceMouse(); 
        Inputs();
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
            Shoot();
        }
    
        moveDir = new Vector2(moveX, moveY).normalized;
    }
    
    void Schmovement() {
        //Rigid Movement
        //rb.velocity = new Vector2(moveDir.x * speed, moveDir.y * speed);
    
        //rb.velocity = Vector3.Slerp(rb.velocity, moveDir * speed, Time.deltaTime * slideTime);
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, moveDir.x * speed, Time.deltaTime * slideTime), 
            Mathf.Lerp(rb.velocity.y, moveDir.y * speed, Time.deltaTime * slideTime));
    
        //Smooth Movement
    }
    
    //Temporary
    void FaceMouse() {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
    }
    
    void Shoot() {
        print("Pew");
    }
}
