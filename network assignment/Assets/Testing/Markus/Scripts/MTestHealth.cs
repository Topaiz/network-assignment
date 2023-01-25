using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// deals with displaying player health

public class MTestHealth : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthText;

    [SerializeField]
    private MTestHealth1 playerHealth;


    private void Update()
    {
        
        healthText.text = this.playerHealth.score.ToString();
    }

}
