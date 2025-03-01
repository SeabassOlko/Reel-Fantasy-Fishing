using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Exceptions;
using Unity.Services.Leaderboards.Models;
using Unity.Mathematics;

public class LeaderBoardInitializer : MonoBehaviour
{
    [SerializeField]
    float startingTabPosition = 300.0f, tabSpacing = 100.0f;

    [SerializeField]
    GameObject leaderboardInfoPrefab;

    [SerializeField]
    GameObject contentBox;

    bool leaderboardActive = false;

    // Leaderboard ID's
    public const string heaviestCatchLeaderboardID = "HeaviestCatchID";
    public const string bestRunLeaderboardID = "BestRunID";

    string currentLeaderboardID = heaviestCatchLeaderboardID;

    int count = 0;
    int maxHighscores = 10;

    float refreshTimer = 2.0f;
    float currentTime = 0;

    MainMenuManager mainMenuManagerRef;

    private void Start()
    {
        mainMenuManagerRef = FindAnyObjectByType<MainMenuManager>();
    }

    public async void InitializeLeaderboard()
    {
        Debug.Log("Initialization Called");
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await AuthenticationService.Instance.UpdatePlayerNameAsync(LoadSaveManager.Instance.gameData.PlayerUsername);
        }

        UpdatePlayerLeaderBoard();
        LoadLeaderboard(heaviestCatchLeaderboardID);
        leaderboardActive = true;
    }

    async void UpdatePlayerLeaderBoard()
    {
        Debug.Log("Update Player board called");
        LeaderboardEntry HeaviestScoreEntry;
        LeaderboardEntry BestRunEntry; 

        try
        {
            HeaviestScoreEntry = await LeaderboardsService.Instance.GetPlayerScoreAsync(heaviestCatchLeaderboardID);
        }
        catch (LeaderboardsException e)
        {
            Debug.Log(e.Reason);
            HeaviestScoreEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(heaviestCatchLeaderboardID, LoadSaveManager.Instance.gameData.highScores.heaviestWeight);
        }

        try
        {
            BestRunEntry = await LeaderboardsService.Instance.GetPlayerScoreAsync(bestRunLeaderboardID);
        }
        catch (LeaderboardsException e)
        {
            Debug.Log(e.Reason);
            BestRunEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync(bestRunLeaderboardID, LoadSaveManager.Instance.gameData.highScores.highestGoldAmount);
        }

        if (LoadSaveManager.Instance.gameData.highScores.heaviestWeight > HeaviestScoreEntry.Score)
        {
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(heaviestCatchLeaderboardID, LoadSaveManager.Instance.gameData.highScores.heaviestWeight);
            }
            catch (LeaderboardsException e)
            {
                Debug.Log(e.Reason);
            }
        }

        if (LoadSaveManager.Instance.gameData.highScores.highestGoldAmount > BestRunEntry.Score)
        {
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(bestRunLeaderboardID, LoadSaveManager.Instance.gameData.highScores.highestGoldAmount);
            }
            catch (LeaderboardsException e)
            {
                Debug.Log(e.Reason);
            }
        }
        }

    public void LoadBoard(string leaderboardID)
    {
        LoadLeaderboard(leaderboardID);
    }

    void Update()
    {
        if (!leaderboardActive)
        {
            if (LoadSaveManager.Instance.gameData.PlayerUsername != "")
            {
                InitializeLeaderboard();
                leaderboardActive = true;
            }
            else
                return;
        }

        if (currentTime < refreshTimer)
            currentTime += Time.deltaTime;
        else
        {
            currentTime -= refreshTimer;
            if (!mainMenuManagerRef.IsLeaderboardLoaded())
                LoadLeaderboard(currentLeaderboardID);
        }
    }

    async void LoadLeaderboard(string leaderboardID)
    {
        currentLeaderboardID = leaderboardID;
        count = 0;

        foreach (Transform child in contentBox.transform)
        {
            Destroy(child.gameObject);
        }

        LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

        foreach (LeaderboardEntry entry in leaderboardScoresPage.Results)
        {
            if (count < maxHighscores && count <= leaderboardScoresPage.Total)
            {
                Vector3 spawnPos = new Vector3(0, startingTabPosition - (tabSpacing * count), 0);
                GameObject leaderboardInfo = Instantiate(leaderboardInfoPrefab, contentBox.transform.position, contentBox.transform.rotation);
                
                leaderboardInfo.transform.SetParent(contentBox.transform);
                leaderboardInfo.GetComponent<RectTransform>().anchoredPosition = spawnPos;
                leaderboardInfo.GetComponent<RectTransform>().localScale = Vector3.one;

                LeaderboardTabInfo tabInfo = leaderboardInfo.GetComponent<LeaderboardTabInfo>();
                tabInfo.SetTabInfo(entry.Rank + 1, entry.PlayerName, (float)entry.Score, leaderboardID == heaviestCatchLeaderboardID);

                count++;
            }
            else
                break;
        }

    }


}
