using UnityEngine;

public class FishLibrary : MonoBehaviour 
{
    [SerializeField]
    GameObject[] commonFishLibrary, unCommonFishLibrary, rareFishLibrary, legendaryFishLibrary, mythicalFishLibrary;

    enum BaitTypes
    {
        CommonBait, CastersBait, AnglersBait, EnchantedBait
    }

    public GameObject GetFish(int fish)
    {
        return commonFishLibrary[fish];
    }

    public GameObject GetRandomFish(int bait)
    {
        int randomNumber = Random.Range(0, 101);
        if (bait == (int)BaitTypes.CommonBait)
        {
            if (randomNumber < 75)
                return commonFishLibrary[Random.Range(0, commonFishLibrary.Length)];
            else
                return unCommonFishLibrary[Random.Range(0, unCommonFishLibrary.Length)];
        }
        else if (bait == (int)BaitTypes.CastersBait)
        {
            if (randomNumber < 60)
                return unCommonFishLibrary[Random.Range(0, unCommonFishLibrary.Length)];
            else
                return rareFishLibrary[Random.Range(0, rareFishLibrary.Length)];
        }
        else if (bait == (int)BaitTypes.AnglersBait)
        {
            if (randomNumber < 50)
                return rareFishLibrary[Random.Range(0, rareFishLibrary.Length)];
            else
                return legendaryFishLibrary[Random.Range(0, legendaryFishLibrary.Length)];
        }
        else if (bait == (int)BaitTypes.EnchantedBait)
        {
            if (randomNumber < 60)
                return legendaryFishLibrary[Random.Range(0, legendaryFishLibrary.Length)];
            else
                return mythicalFishLibrary[Random.Range(0, mythicalFishLibrary.Length)];
        }

        return commonFishLibrary[0];
    }

    public string[] GetAllFish()
    {
        string[] fishNames = new string[commonFishLibrary.Length + unCommonFishLibrary.Length + rareFishLibrary.Length + legendaryFishLibrary.Length + mythicalFishLibrary.Length];
        int count = 0;

        foreach (GameObject fish in commonFishLibrary)
        {
            fishNames[count] = fish.name;
            count++;
        }
        foreach (GameObject fish in unCommonFishLibrary)
        {
            fishNames[count] = fish.name;
            count++;
        }
        foreach (GameObject fish in rareFishLibrary)
        {
            fishNames[count] = fish.name;
            count++;
        }
        foreach (GameObject fish in legendaryFishLibrary)
        {
            fishNames[count] = fish.name;
            count++;
        }
        foreach (GameObject fish in mythicalFishLibrary)
        {
            fishNames[count] = fish.name;
            count++;
        }

        return fishNames;
    }
}
