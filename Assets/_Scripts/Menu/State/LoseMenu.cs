using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : BaseMenu
{
    [SerializeField]
    Button menuButton;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Lose;
        menuButton.onClick.AddListener(() => BackToMenu());
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
