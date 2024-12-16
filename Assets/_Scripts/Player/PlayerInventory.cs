using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    int commonBait = 3, castersBait = 0, anglersBait = 0, enchantedBait = 0;
    int gold = 50;
    int totalGold = 0;

    enum CurrentBait
    {
        CommonBait, CastersBait, AnglersBait, EnchantedBait
    }

    CurrentBait currentBait = CurrentBait.CommonBait;

    void Start()
    {
        LoadSaveManager.GameData.PlayerStats stats = LoadSaveManager.Instance.gameData.playerStats;

        gold = stats.gold;
        totalGold = stats.totalGold;
        commonBait = stats.commonBait;
        castersBait = stats.castersBait;
        anglersBait = stats.anglersBait;
        enchantedBait = stats.enchantedBait;
    }

    public void SaveInventory()
    {
        LoadSaveManager.GameData.PlayerStats stats = LoadSaveManager.Instance.gameData.playerStats;

        stats.gold = gold;
        stats.totalGold = totalGold;
        stats.commonBait = commonBait;
        stats.castersBait = castersBait;
        stats.anglersBait = anglersBait;
        stats.enchantedBait = enchantedBait;

        LoadSaveManager.Instance.Save();
    }

    public void ChangeBait()
    {
        if (currentBait == CurrentBait.EnchantedBait)
            currentBait = 0;
        else
            currentBait++;
    }

    public int GetCurrentBaitType()
    {
        return (int)currentBait;
    }

    public int GetCurrentBaitAmount()
    {
        switch (currentBait) 
        {
            case CurrentBait.CommonBait:
                return commonBait;
            case CurrentBait.CastersBait:
                return castersBait;
            case CurrentBait.AnglersBait:
                return anglersBait;
            case CurrentBait .EnchantedBait:
                return enchantedBait;
        }
        return 0;
    }

    public void AddCommonBait()
    {
        commonBait++;
    }

    public void AddCastersBait()
    {
        castersBait++;
    }

    public void AddAnglersBait()
    {
        anglersBait++;
    }

    public void AddEnchantedBait()
    {
        enchantedBait++;
    }

    public void UseCurrentBait()
    {
        switch (currentBait) 
        { 
        case CurrentBait.CommonBait:
            commonBait--;
            break;
        case CurrentBait.CastersBait:
            castersBait--;
            break;
        case CurrentBait.AnglersBait:
            anglersBait--;
            break;
        case CurrentBait.EnchantedBait:
            enchantedBait--;
            break;
        }
    }

    public int GetTotalBait()
    {
        return commonBait + castersBait + anglersBait + enchantedBait;
    }
    public int GetCommonBait()
    {
        return commonBait;
    }
    public int GetCastersBait()
    {
        return castersBait;
    }
    public int GetAnglersBait()
    {
        return anglersBait;
    }
    public int GetEnchantedBait()
    {
        return enchantedBait;
    }

    public int GetGold()
    {
        return gold;
    }

    public bool SpendGold(int amount)
    {
        if (gold - amount >= 0)
        {
            Debug.Log("Spent " + amount + " Gold");
            gold -= amount;
            return true;
        }
        else
            return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        totalGold += amount;
        if (totalGold > LoadSaveManager.Instance.gameData.highScores.highestGoldAmount)
        {
            LoadSaveManager.Instance.gameData.highScores.highestGoldAmount = totalGold;
        }
        //Save inventory will also save highscore changes
        SaveInventory();
    }
}
