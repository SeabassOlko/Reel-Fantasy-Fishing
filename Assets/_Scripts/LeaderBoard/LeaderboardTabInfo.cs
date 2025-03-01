using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardTabInfo : MonoBehaviour
{
    [SerializeField]
    TMP_Text standingText, usernameText, scoreText;

    public void SetTabInfo(int standing, string username, float score, bool weight)
    {
        standingText.text = standing.ToString();
        usernameText.text = username;
        if (weight) 
            scoreText.text = score.ToString();
        else
            scoreText.text = SetUpRunScore(score);
    }

    string SetUpRunScore(float score)
    {
        string runScoreText = "";

        if (score < 1000)
            runScoreText = score.ToString();
        else if (score < 1000000)
            runScoreText = (score / 1000).ToString() + "k";
        else if (score < 1000000000)
            runScoreText = (score / 1000000).ToString() + "m";


        return runScoreText;
    }
}
