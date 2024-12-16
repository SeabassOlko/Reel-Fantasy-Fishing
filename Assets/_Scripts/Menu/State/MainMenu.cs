using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public Button playButton;
    public Button settingsButton;
    public Button creditsButton;
    public Button quitButton;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.MainMenu;
        playButton.onClick.AddListener(() => SceneManager.LoadScene("Level"));
        settingsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Settings));
        creditsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Credits));
        quitButton.onClick.AddListener(() => QuitGame());
    }

    public override void EnterState()
    {
        base.EnterState();
    }
}
