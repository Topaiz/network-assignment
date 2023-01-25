using Alteruna;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MTestScore : AttributesSync
{
    [SynchronizableField]
    public Dictionary<string, int> playerInfos = new Dictionary<string, int>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
