using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MTestPlayer : MonoBehaviour
{
    public Alteruna.Avatar Avatar;
    public int health = 5;

    [SerializeField]
    private MTestCamera mCamera;
 
    [SerializeField]
    private int moveSpeed = 5;

    [SerializeField]
    private Transform moveTransform;
    private Transform rotTransform;

    Vector2 mousePos;
    Vector2 objectPos;

    // Start is called before the first frame update
    void Start()
    {
        rotTransform = transform;

        GameObject goCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mCamera = goCamera.GetComponent<MTestCamera>();
        mCamera.SetPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Avatar.IsMe)
        {
            Movement();
            UpdateRotation();
        }
    }

    void Movement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        
        moveTransform.position += vInput *
            moveSpeed * Time.deltaTime * rotTransform.up;
        moveTransform.position += hInput *
            moveSpeed * Time.deltaTime * rotTransform.right;
    }

    public void UpdateRotation()
    {
        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(rotTransform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        rotTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
