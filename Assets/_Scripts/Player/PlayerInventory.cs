using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    int commonBait = 3, castersBait = 0, anglersBait = 0, enchantedBait = 0;

    enum CurrentBait
    {
        CommonBait, CastersBait, AnglersBait, EnchantedBait
    }

    CurrentBait currentBait;

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
}
