using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardTabInfo : MonoBehaviour
{
    [SerializeField]
    TMP_Text standingText, usernameText, weightText;

    public void SetTabInfo(int standing, string username, float weight)
    {
        standingText.text = standing.ToString();
        usernameText.text = username;
        weightText.text = weight.ToString();
    }
}
