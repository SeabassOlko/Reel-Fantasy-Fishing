using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuCanvas, instructionsCanvas;

    [SerializeField]
    TMP_Text coinHighscoreText, weightHighscoreText;

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
