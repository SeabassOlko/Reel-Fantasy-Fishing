using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : BaseMenu
{
    public Button backButton;
    public Button settingsButton;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Credits;
        backButton.onClick.AddListener(JumpBack);
        settingsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Settings));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
