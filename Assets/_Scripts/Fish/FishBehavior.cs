using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FishStats))]

public class FishBehavior : MonoBehaviour
{
    FishStats fishInfo;
    BobberMechanics bobber;

    public bool isActive = false;

    bool moveDirection;
    float baseMoveTime = 50f;
    float moveTime, currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bobber = FindAnyObjectByType<BobberMechanics>();
        fishInfo = GetComponent<FishStats>();
        if (Random.Range(0, 2) == 1)
            moveDirection = true;
        else
            moveDirection = false;
        SetMoveTime();
        Debug.Log("Move Time: " + moveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        if (currentTime <= moveTime)
        {
            currentTime += Time.deltaTime;
            bobber.moveBobber(moveDirection, fishInfo.GetSpeed() * Time.deltaTime);
        }
        else
        {
            currentTime = 0f;
            SetMoveTime();
            moveDirection = !moveDirection;
        }
    }

    void SetMoveTime()
    {
        moveTime = Random.Range(0.1f, baseMoveTime / fishInfo.GetAgression());
    }

    public void MakeActive()
    {
        isActive = true;
    }
}
