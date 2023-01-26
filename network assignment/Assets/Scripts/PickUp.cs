using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PickUp : MonoBehaviour
{
    public int level;
    private static float baseFireRateModifier = 0.1f;
    private SpriteRenderer spriteRenderer;

    public float ModifyFireRate { get => baseFireRateModifier * level; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpConstructor(int level)
    {
        this.level = level;

        if (level > 1)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent.GetComponentInChildren<JoelShoot>().FireRateModifier += ModifyFireRate;
            Destroy(this.gameObject);
        }
    }

}
