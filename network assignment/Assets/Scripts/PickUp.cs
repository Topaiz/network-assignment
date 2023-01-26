using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PickUp : MonoBehaviour
{
    public int level;
    private static int baseScore = 50;
    private SpriteRenderer spriteRenderer;

    public int ScoreAmount { get => baseScore * level; }

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
            //collision.GetComponentInChildren<PlayerDataSynchronizable>().Score += ScoreAmount;

            Destroy(this.gameObject);
        }
    }

}
