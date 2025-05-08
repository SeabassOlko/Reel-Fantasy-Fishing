using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SideMoveAnim))]
public class BaseMenu : MonoBehaviour
{
    [HideInInspector]
    public MenuController.MenuStates state;
    protected MenuController context;

    public virtual void InitState(MenuController ctx)
    {
        context = ctx;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateState() { }

    public void JumpBack()
    {
        context.JumpBack();
    }

    public void SetNextMenu(MenuController.MenuStates newState)
    {
        context.SetActiveState(newState);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
