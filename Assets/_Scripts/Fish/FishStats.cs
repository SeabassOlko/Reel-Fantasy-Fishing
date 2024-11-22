using UnityEngine;

public class FishStats : MonoBehaviour
{
    enum rarity
    {
        common, uncommon, rare, legendary, mythical
    }

    [SerializeField]
    string name;
    [SerializeField]
    float minWeight, maxWeight, weight, speed, agression;
    [SerializeField]
    int baseValue, value;
    [SerializeField]
    rarity fishRarity;

    void Start()
    {
        weight = Random.Range(minWeight, maxWeight);
        value = baseValue + (baseValue * (int)weight / (int)minWeight);
    }

    public int GetRarity()
    {
        return (int)fishRarity;
    }

    public float GetWeight()
    {
        return weight;
    }

    public float GetSpeed() 
    {
        return speed;
    }

    public float GetAgression() 
    { 
        return agression;
    }

    public string GetName() 
    {
        return name;
    }

    public int GetValue()
    {
        return value;
    }

    public void DestroyFish()
    {
        Destroy(gameObject);
    }
}
