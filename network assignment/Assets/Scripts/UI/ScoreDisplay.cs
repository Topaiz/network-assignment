using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro scoreText;

    [SerializeField]
    private PlayerScore playerScore;

    private void Update()
    {
        scoreText.text = "Score: " + this.playerScore.score.ToString();
    }
}
