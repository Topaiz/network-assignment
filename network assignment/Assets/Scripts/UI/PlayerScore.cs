using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : AttributesSync
{
    [SynchronizableField]
    public int score = 0;

    [SerializeField]
    private int scoreToWin = 10;

    public Alteruna.Avatar avatar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddScore(int add)
    {
        score += add;
        // Has player won?
        return (score >= scoreToWin);
    }
}
