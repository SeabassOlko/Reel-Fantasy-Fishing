using UnityEngine;

public class RodTriggerScript : MonoBehaviour
{
    void FinishAnim()
    {
        FindAnyObjectByType<PlayerController>().SpawnBobber();
    }
}
