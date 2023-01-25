
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro scoreText;

    private PlayerScore playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<PlayerScore>();

    }

    private void Update()
    {
        if (scoreText == null || playerScore == null)
        {
            return;
        }

        if (playerScore.score < playerScore.winScore)
        {
            scoreText.text = "Score: " + this.playerScore.score.ToString();
        }
        else if (playerScore.score >= playerScore.winScore)
        {
            string playerName = "Player name without reference!";
            if (this.playerScore.avatar != null)
            {
                // Get winning player name
                playerName = this.playerScore.avatar.Possessor.Name;
            }

            scoreText.text = "Player " + playerName + " won!! Woop woop!";
            // TODO make game go pause pause?
        }
    }
}
