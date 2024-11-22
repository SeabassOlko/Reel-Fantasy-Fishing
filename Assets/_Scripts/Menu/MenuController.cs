using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //reference to all the menus created in unity
    public BaseMenu[] concreteMenus;

    //State reference for all the menus. Can be later seperated into a different file, used for dictionary keys
    public enum MenuStates
    {
        MainMenu, Settings, Pause, InGame, Credits
    }

    //Current state reference
    BaseMenu currentState;

    //Menu States as key wich will provide appropriate concrete menu
    Dictionary<MenuStates, BaseMenu> menuMap = new Dictionary<MenuStates, BaseMenu>();

    //MenuStack that will be populated as teh game menu's are traversed
    Stack<MenuStates> menuStack = new Stack<MenuStates>();

    //the initial state to push onto the stack
    public MenuStates initialState = MenuStates.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (concreteMenus.Length <= 0)
        {
            concreteMenus = gameObject.GetComponentsInChildren<BaseMenu>(true);
        }

        //loop through each menu and initialize their state in the dicitonary
        foreach (BaseMenu menu in concreteMenus)
        {
            if (menu == null) continue;
            menu.InitState(this);

            if (menuMap.ContainsKey(menu.state))
                Debug.Log($"{menu.state} is already in menuMap, overwriting the state with a new concrete menu");
            menuMap.Add(menu.state, menu);
        }

        SetActiveState(initialState);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 1)
            SetActiveState(MenuStates.MainMenu);
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuMap.ContainsKey(newState))
        {
            Debug.Log($"{newState} was not found in the menu dicitonary");
            return;
        }

        if (currentState == menuMap[newState])
        {
            Debug.Log($"{newState} is our current state. Menu did not change");
            return;
        }

        if (currentState != null)
        {
            currentState.ExitState();
            //here where you may do an exit menu animation
            currentState.gameObject.SetActive(false);
        }

        currentState = menuMap[newState];
        //here where you may do an enter menu animation
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack) menuStack.Push(newState);
    }
}
