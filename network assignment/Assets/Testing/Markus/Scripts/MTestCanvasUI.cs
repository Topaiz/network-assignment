using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Alteruna;

public class MTestCanvasUI : MonoBehaviour// Synchronizable
{
    public TextMeshProUGUI[] scoreTexts;

    //private Dictionary<string, int> playerInfos = new Dictionary<string, int>();

    private readonly string scoreText = " | Score: ";

    MTestScore scores;

    // Start is called before the first frame update
    void Start()
    {
        scores = GetComponent<MTestScore>();
        //scores.playerInfos = playerInfos;
        AddNewPlayer("Bosse", 124);
        AddNewPlayer("Po", 4);
        AddNewPlayer("Donau", 67);
        AddNewPlayer("ume river", 35);
        AddNewPlayer("Hasse", 19);
    }

    // Update is called once per frame
    void Update()
    {
        // If the value of our float has changed, sync it with the other players in our playroom.
        //if (playerInfos != oldPlayerInfos)
        //{
        //    // Store the updated value
        //    oldPlayerInfos = playerInfos;

        //    // Tell Alteruna Multiplayer that we want to commit our data.
        //    Commit();
        //}

        //// Update the Synchronizable
        //base.SyncUpdate();
    }

    public void AddNewPlayer(string playerID, int score)
    {
        if (!scores.playerInfos.ContainsKey(playerID))
        {
            scores.playerInfos[playerID] = score;
        }
        ChangeScore(playerID, score);
    }

    public void RemovePlayer(string playerID)
    {
        scores.playerInfos.Remove(playerID);
    }

    public void ChangeScore(string playerID, int newScore)
    {
        if (scores.playerInfos.ContainsKey(playerID))
        {
            scores.playerInfos[playerID] = newScore;
        }
        SortOutScores();
    }

    private void SortOutScores()
    {
        // sort out the scores
        Dictionary<string, int> newScores = scores.playerInfos.OrderByDescending(
            pair => pair.Value).Take(3).ToDictionary(pair => pair.Key, pair => pair.Value);

        // Display the scores
        int index = 0;
        foreach (var score in newScores)
        {
            if (index < 3)
            {
                scoreTexts[index].text = score.Key + scoreText + score.Value;
                index++;
            }
            else
            {
                break;

            }
        }
    }

    private Dictionary<string, int> oldPlayerInfos = new Dictionary<string, int>();

    //public override void DisassembleData(Reader reader, byte LOD)
    //{
    //    playerInfos = (Dictionary<string, int>)reader.ReadObject();
    //    // Set our data to the updated value we have recieved from another player.
        
    //    // Save the new data as our old data, otherwise we will immediatly think it changed again.
    //    oldPlayerInfos = playerInfos;
    //}

    //public override void AssembleData(Writer writer, byte LOD)
    //{
    //    // Write our data so that it can be sent to the other players in our playroom.
    //    writer.WriteObject(playerInfos);
    //}
}
