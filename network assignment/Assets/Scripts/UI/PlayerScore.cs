using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : AttributesSync
{
    [SynchronizableField]
    public int score = 0;

    public int winScore = 10;

    public Alteruna.Avatar avatar;

    private void Start()
    {
        avatar = GetComponentInParent<Alteruna.Avatar>();
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (avatar.IsMe && Input.GetKeyUp(KeyCode.P))
    //    {
    //        score++;
    //    }
    ////    if (avatar.IsMe && score >= winScore)
    ////    {
    ////        BroadcastRemoteMethod("PlayerWon");
    ////    }
    //}

    [SynchronizableMethod]
    private void PlayerWon()
    {
        Debug.Log("A player won with score: " + score);
        score = -1;
    }

}
