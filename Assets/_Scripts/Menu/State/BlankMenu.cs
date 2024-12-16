using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Blank;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public void SetHook()
    {
        JumpBack();
        context.SetActiveState(MenuController.MenuStates.Reel);
    }
}
