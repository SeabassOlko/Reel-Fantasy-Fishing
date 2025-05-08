using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuCanvas, instructionsCanvas, usernameCreationCanvas, leaderboardCanvas;

    [SerializeField]
    float animTime = 1.0f;

    SideMoveAnim mainMenuAnim, instructionsAnim, leaderboardAnim;

    [SerializeField]
    TMP_Text coinHighscoreText, weightHighscoreText;

    LeaderBoardInitializer leaderboard;

    [Header("Leaderboard Texts")]
    [SerializeField]
    TMP_Text leaderboardScoreText, leaderboardScoreTypeText, leaderboardPlayerScoreTypeText, leaderboardListScoreType;

    bool leaderboardWeight = true, isLeaderboardVisible = false;

    private void Start()
    {
        leaderboard = FindAnyObjectByType<LeaderBoardInitializer>();
        leaderboardScoreText.text = Mathf.Round(LoadSaveManager.Instance.gameData.highScores.heaviestWeight * 100) / 100 + " lb";

        mainMenuAnim = mainMenuCanvas.GetComponent<SideMoveAnim>();
        instructionsAnim = instructionsCanvas.GetComponent<SideMoveAnim>();
        leaderboardAnim = leaderboardCanvas.GetComponent<SideMoveAnim>();

        mainMenuAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
        leaderboardCanvas.SetActive(false);
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
        SceneChangerUI changer = FindAnyObjectByType<SceneChangerUI>();
        changer.SetSceneToLoad("Level1");

        if (LoadSaveManager.Instance.gameData.playerStats.gameReset)
        {
            LoadSaveManager.Instance.ResetPlayerData();
            LoadSaveManager.Instance.Save();
        }

        changer.CloseScreen();
    }

    public void OpenInstructions()
    {
        //mainMenuCanvas.SetActive(false);
        mainMenuAnim.MoveOut(SideMoveAnim.MoveDirection.Left, animTime);
        instructionsCanvas.SetActive(true);
        instructionsAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
    }

    public void CloseInstructions() 
    {
        mainMenuCanvas?.SetActive(true);
        mainMenuAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
        //instructionsCanvas?.SetActive(false);
        instructionsAnim.MoveOut(SideMoveAnim.MoveDirection.Left, animTime);
    }

    public void OpenUsernameCreation()
    {
        mainMenuCanvas.SetActive(false);
        usernameCreationCanvas.SetActive(true);
    }

    public void CloseUsernameCreation()
    {
        mainMenuCanvas?.SetActive(true);
        mainMenuAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
        usernameCreationCanvas?.SetActive(false);
    }

    public void OpenLeaderboard()
    {
        //mainMenuCanvas.SetActive(false);
        mainMenuAnim.MoveOut(SideMoveAnim.MoveDirection.Left, animTime);
        leaderboardCanvas.SetActive(true);
        leaderboardAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
        isLeaderboardVisible = true;
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
        return isLeaderboardVisible;
    }

    public void CloseLeaderboard()
    {
        mainMenuCanvas?.SetActive(true);
        mainMenuAnim.MoveIn(SideMoveAnim.MoveDirection.Left, animTime);
        //leaderboardCanvas?.SetActive(false);
        leaderboardAnim.MoveOut(SideMoveAnim.MoveDirection.Left, animTime);
        isLeaderboardVisible = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
