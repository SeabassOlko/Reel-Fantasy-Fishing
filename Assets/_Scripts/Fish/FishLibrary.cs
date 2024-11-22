using UnityEngine;

public class FishLibrary : MonoBehaviour 
{
    [SerializeField]
    GameObject[] fishLibrary;

    public GameObject GetFish(int fish)
    {
        return fishLibrary[fish];
    }
}
