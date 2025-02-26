using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatchMenu : BaseMenu
{
    public Button sellButton;
    public Image fishImage;
    public TMP_Text fishName;
    public TMP_Text fishWeight;
    public TMP_Text fishValue;

    int fishRealValue;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Catch;
        sellButton.onClick.AddListener(() => LeaveCatchMenu());
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public void SetUpFishInfo(Sprite fishSprite, string fishN, float weight, int value)
    {
        fishImage.sprite = fishSprite;
        fishName.text = fishN;
        fishWeight.text = "Weight: " + weight +"lb";
        fishValue.text = "Value: " + value + "g";
        fishRealValue = value;
        if (weight > LoadSaveManager.Instance.gameData.highScores.heaviestWeight)
        {
            LoadSaveManager.Instance.gameData.highScores.heaviestWeight = weight;
        }
    }

    void LeaveCatchMenu()
    {
        PlayerInventory inventory = FindAnyObjectByType<PlayerInventory>();
        inventory.AddGold(fishRealValue);
        inventory.SaveInventory();
        JumpBack();
        FishingMenu fishingMenu = FindAnyObjectByType<FishingMenu>();
        fishingMenu.UpdateCoins();
        fishingMenu.UpdateBait();
    }
}
