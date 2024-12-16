using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;

public class FishingMenu : BaseMenu
{
    public Button exitButton;
    public Button baitButton;
    public Button shopButton;
    public Image currentBaitImage;
    public TMP_Text currentBaitText;
    public TMP_Text currentCoinText;

    public Sprite[] baitSprites;

    PlayerInventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        UpdateBait();
        UpdateCoins();
    }

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.InGame;
        shopButton.onClick.AddListener(() => EnterShop());
        exitButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        baitButton.onClick.AddListener(() => CycleBait());
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    void EnterShop()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.SetIsShopping(true);
        player.SetIsCasting(false);
        context.SetActiveState(MenuController.MenuStates.Shop);
    }

    void CycleBait()
    {
        inventory.ChangeBait();
        UpdateBait();
    }

    public void Casting()
    {
        context.SetActiveState(MenuController.MenuStates.Blank);
    }

    public void UpdateBait()
    {
        currentBaitImage.sprite = baitSprites[inventory.GetCurrentBaitType()];
        currentBaitText.text = "X " + inventory.GetCurrentBaitAmount();
    }

    public void UpdateCoins()
    {
        currentCoinText.text = "X " + inventory.GetGold();
    }

    public void EnterLoseMenu()
    {
        context.SetActiveState(MenuController.MenuStates.Lose);
    }
}
