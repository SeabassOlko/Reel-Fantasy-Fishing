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

    [SerializeField]
    string leaderboardID = "HeaviestCatchID";

    int count = 0;
    int maxHighscores = 10;

    float refreshTimer = 2.0f;
    float currentTime = 0;

    MainMenuManager mainMenuManagerRef;

    private void Start()
    {
        mainMenuManagerRef = FindAnyObjectByType<MainMenuManager>();
    }

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await AuthenticationService.Instance.UpdatePlayerNameAsync(LoadSaveManager.Instance.gameData.PlayerUsername);
        }

        UpdatePlayerLeaderBoard();
        LoadLeaderboard();
    }

    async void UpdatePlayerLeaderBoard()
    {
        if (LoadSaveManager.Instance.gameData.highScores.heaviestWeight > 0)
        {
            try
            {
                await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, LoadSaveManager.Instance.gameData.highScores.heaviestWeight);
            }
            catch (LeaderboardsException e)
            {
                Debug.Log(e.Reason);
            }
        }
    }

    public void LoadBoard()
    {
        LoadLeaderboard();
    }

    void Update()
    {
        if (currentTime < refreshTimer)
            currentTime += Time.deltaTime;
        else
        {
            currentTime -= refreshTimer;
            if (!mainMenuManagerRef.IsLeaderboardLoaded())
                LoadLeaderboard();
        }
    }

    async void LoadLeaderboard()
    {
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
                tabInfo.SetTabInfo(entry.Rank + 1, entry.PlayerName, (float)entry.Score);

                count++;
            }
            else
                break;
        }

    }


}
