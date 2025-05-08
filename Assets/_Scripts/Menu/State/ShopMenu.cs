using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopMenu : BaseMenu
{
    public Button closeButton;
    public Button commonBaitButton;
    public Button castersBaitButton;
    public Button anglersBaitButton;
    public Button enchantedBaitButton;

    [SerializeField]
    TMP_Text coinText, commonBaitText, castersBaitText, anglersBaitText, enchantedBaitText;

    [SerializeField]
    int commonBaitCost, castersBaitCost, anglersBaitCost, enchantedBaitCost;

    PlayerInventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<PlayerInventory>();
        UpdateCointText();
        UpdateBaits();
    }

    void Update()
    {
        UpdateCointText();
        UpdateBaits();
    }

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Shop;
        closeButton.onClick.AddListener(() => ExitShop());
        commonBaitButton.onClick.AddListener(() => BuyCommonBait());
        castersBaitButton.onClick.AddListener(() => BuyCastersBait());
        anglersBaitButton.onClick.AddListener(() => BuyAnglersBait());
        enchantedBaitButton.onClick.AddListener(() => BuyEnchantedBait());
    }

    public override void EnterState()
    {
        base.EnterState();
        GetComponent<SideMoveAnim>().MoveIn(SideMoveAnim.MoveDirection.Up, 0.5f);
    }
    
    public void ExitShop()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.SetIsShopping(false);
        player.SetIsCasting(true);
        JumpBack();
        FishingMenu fishingMenu = FindAnyObjectByType<FishingMenu>();
        fishingMenu.UpdateCoins();
        fishingMenu.UpdateBait();
    }

    void BuyCommonBait()
    {
        if (inventory.SpendGold(commonBaitCost))
        {
            inventory.AddCommonBait();
            UpdateCointText();
            UpdateBaits();
        }
    }

    void BuyCastersBait()
    {
        if (inventory.SpendGold(castersBaitCost))
        {
            inventory.AddCastersBait();
            UpdateCointText();
            UpdateBaits();
        }
    }

    void BuyAnglersBait()
    {
        if (inventory.SpendGold(anglersBaitCost))
        { 
            inventory.AddAnglersBait();
            UpdateCointText();
            UpdateBaits();
        }
    }

    void BuyEnchantedBait()
    {
        if (inventory.SpendGold(enchantedBaitCost))
        {
            inventory.AddEnchantedBait();
            UpdateCointText();
            UpdateBaits();
        }
    }

    public void UpdateCointText()
    {
        coinText.text = "X " + inventory.GetGold();
    }

    public void UpdateBaits()
    {
        commonBaitText.text = "X " + inventory.GetCommonBait();
        castersBaitText.text = "X " + inventory.GetCastersBait();
        anglersBaitText.text = "X " + inventory.GetAnglersBait();
        enchantedBaitText.text = "X " + inventory.GetEnchantedBait();
    }

}
