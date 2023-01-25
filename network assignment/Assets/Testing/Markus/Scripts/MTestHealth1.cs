using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// deals with player health and death
public class MTestHealth1 : AttributesSync
{
    [SynchronizableField]
    public int score = 90;
    [SynchronizableField]
    public int damage = 1;

    [SynchronizableField]
    public string hej = "Hej";

    public Alteruna.Avatar avatar;

    public MTestCanvasUI canvasUI;

    private void Awake()
    {
        canvasUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MTestCanvasUI>();
    }

    [SynchronizableMethod]
    private void Die()
    {

        canvasUI.AddNewPlayer(hej, score);
        Debug.Log(hej + " score: " + score);
        // do dead stuff
    }
}
