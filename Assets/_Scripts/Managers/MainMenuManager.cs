using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuCanvas, instructionsCanvas, usernameCreationCanvas, leaderboardCanvas;

    [SerializeField]
    TMP_Text coinHighscoreText, weightHighscoreText;

    LeaderBoardInitializer leaderboard;

    [Header("Leaderboard Texts")]
    [SerializeField]
    TMP_Text leaderboardScoreText, leaderboardScoreTypeText, leaderboardPlayerScoreTypeText, leaderboardListScoreType;

    bool leaderboardWeight = true;

    private void Start()
    {
        leaderboard = FindAnyObjectByType<LeaderBoardInitializer>();
        leaderboardScoreText.text = Mathf.Round(LoadSaveManager.Instance.gameData.highScores.heaviestWeight * 100) / 100 + " lb";
    }

    void SetHighscoreTexts()
    {
        weightHighscoreText.text = Mathf.Round(LoadSaveManager.Instance.gameData.highScores.heaviestWeight * 100) / 100 + " lb";
        coinHighscoreText.text = "X " + LoadSaveManager.Instance.gameData.highScores.highestGoldAmount;
    }

    void Update()
    {
        SetHighscoreTexts();    
    }

    public void PlayGame()
    {
        if (LoadSaveManager.Instance.gameData.playerStats.gameReset)
        {
            LoadSaveManager.Instance.ResetPlayerData();
            LoadSaveManager.Instance.Save();
        }
        
        SceneManager.LoadScene("Level1");
    }

    public void OpenInstructions()
    {
        mainMenuCanvas.SetActive(false);
        instructionsCanvas.SetActive(true);
    }

    public void CloseInstructions() 
    {
        mainMenuCanvas?.SetActive(true);
        instructionsCanvas?.SetActive(false);
    }

    public void OpenUsernameCreation()
    {
        mainMenuCanvas.SetActive(false);
        usernameCreationCanvas.SetActive(true);
    }

    public void CloseUsernameCreation()
    {
        mainMenuCanvas?.SetActive(true);
        usernameCreationCanvas?.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        mainMenuCanvas.SetActive(false);
        leaderboardCanvas.SetActive(true);
    }

    public void SwitchLeaderboard()
    {
        if (leaderboardWeight)
        {
            leaderboardWeight = false;
            leaderboardScoreText.text = LoadSaveManager.Instance.gameData.highScores.highestGoldAmount + " gp";
            leaderboardPlayerScoreTypeText.text = "Your Best Run!";
            leaderboardScoreTypeText.text = "Best Run";
            leaderboardListScoreType.text = "gp";
            leaderboard.LoadBoard(LeaderBoardInitializer.bestRunLeaderboardID);
        }
        else
        {
            leaderboardWeight = true;
            leaderboardScoreText.text = Mathf.Round(LoadSaveManager.Instance.gameData.highScores.heaviestWeight * 100) / 100 + " lb";
            leaderboardPlayerScoreTypeText.text = "Your Heaviest Fish!";
            leaderboardScoreTypeText.text = "Heaviest Fish";
            leaderboardListScoreType.text = "lbs";
            leaderboard.LoadBoard(LeaderBoardInitializer.heaviestCatchLeaderboardID);
        }

    }

    public bool IsLeaderboardLoaded()
    {
        return leaderboardCanvas.activeSelf;
    }

    public void CloseLeaderboard()
    {
        mainMenuCanvas?.SetActive(true);
        leaderboardCanvas?.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
