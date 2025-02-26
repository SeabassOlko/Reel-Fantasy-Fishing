using System.Linq;
using TMPro;
using UnityEngine;

public class UsernameCreationManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField usernameInput;

    public void ConfirmUsername()
    {
        if (usernameInput.text == "" || CheckBannedWords(usernameInput.text))
        {
           usernameInput.text = string.Empty;
        }
        else
        {
            LoadSaveManager.Instance.gameData.PlayerUsername = usernameInput.text;
            LoadSaveManager.Instance.Save();
            FindAnyObjectByType<MainMenuManager>().CloseUsernameCreation();
        }    
    }

    bool CheckBannedWords(string name)
    {
        BannedWordsList list = new BannedWordsList();

        foreach (string word in list.bannedWords)
        {
            if (usernameInput.text.Contains(word))
                return true;
        }

        return false;
    }
}
