using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTestCircle : MonoBehaviour
{
    public float CurrentSize = 5;
    [SerializeField]
    private Texture2D tex;

    [SerializeField]
    private Sprite sp;

    [SerializeField]
    private float size = 1;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        
        sp = Sprite.Create(tex, 
            new Rect(0.0f, 0.0f, tex.width, tex.height),
            new Vector2(0.5f, 0.5f), 100.0f);

        sr.sprite = sp;
        sr.color = Color.blue;

        gameObject.AddComponent<CircleCollider2D>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;

        transform.localScale = new Vector3(size, size, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseSize(float increase)
    {
        // calculate increase of area
        float relIncrease = 1 + increase / size;
        // calculate current area
        float area = Mathf.PI * (size / 2.0f) * (size / 2.0f);
        // increase the area and recieve diameter (size = r * 2)
        size = Mathf.Sqrt(area * relIncrease / Mathf.PI) * 2;

        transform.localScale = new Vector3(size, size, 0);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<MTestCircle>().IncreaseSize(size);
            Destroy(gameObject);
        }
    }
}
