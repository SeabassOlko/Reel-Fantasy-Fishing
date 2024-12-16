using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelingMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Reel;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public void FishEscaped()
    {
        JumpBack();
    }

    public void FishCaught()
    {
        JumpBack();
        context.SetActiveState(MenuController.MenuStates.Catch);
    }
}
