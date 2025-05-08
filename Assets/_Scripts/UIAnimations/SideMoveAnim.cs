using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMoveAnim : MonoBehaviour
{
    public enum MoveDirection
    {
        Left, Right, Up, Down
    }

    float animTime = 0f;
    float animTimeElapsed = 0f;
    MoveDirection animDirection;

    float horizontalMoveAmount = 800f, verticalMoveAmount = 1720f;

    RectTransform uiTransform;
    Vector3 startTransform;

    bool playAnim = false;
    bool grabTransform = false;

    // Start is called before the first frame update
    void Start()
    {
        uiTransform = GetComponent<RectTransform>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnim)
        {
            if (!grabTransform)
            {
                startTransform = uiTransform.anchoredPosition;
                grabTransform = true;
            }
            if (animDirection == MoveDirection.Left)
            {
                animTimeElapsed += Time.deltaTime;
                uiTransform.anchoredPosition = startTransform - new Vector3(horizontalMoveAmount * (animTimeElapsed / animTime), 0, 0);
                if (animTimeElapsed >= animTime)
                {
                    uiTransform.anchoredPosition = startTransform - new Vector3(horizontalMoveAmount, 0, 0);
                    playAnim = false;
                    grabTransform = false;
                    animTimeElapsed = 0;
                }
            }
            else if (animDirection == MoveDirection.Right)
            {
                animTimeElapsed += Time.deltaTime;
                uiTransform.anchoredPosition = startTransform + new Vector3(horizontalMoveAmount * (animTimeElapsed / animTime), 0, 0);
                if (animTimeElapsed >= animTime)
                {
                    uiTransform.anchoredPosition = startTransform + new Vector3(horizontalMoveAmount, 0, 0);
                    playAnim = false;
                    grabTransform = false;
                    animTimeElapsed = 0;
                }
            }
            else if (animDirection == MoveDirection.Up)
            {
                animTimeElapsed += Time.deltaTime;
                uiTransform.anchoredPosition = startTransform + new Vector3(0, verticalMoveAmount * (animTimeElapsed / animTime), 0);
                if (animTimeElapsed >= animTime)
                {
                    uiTransform.anchoredPosition = startTransform + new Vector3(0, verticalMoveAmount * (animTimeElapsed / animTime), 0);
                    playAnim = false;
                    grabTransform = false;
                    animTimeElapsed = 0;
                }
            }
            else if (animDirection == MoveDirection.Down)
            {
                animTimeElapsed += Time.deltaTime;
                uiTransform.anchoredPosition = startTransform - new Vector3(0, verticalMoveAmount * (animTimeElapsed / animTime), 0);
                if (animTimeElapsed >= animTime)
                {
                    uiTransform.anchoredPosition = startTransform - new Vector3(0, verticalMoveAmount * (animTimeElapsed / animTime), 0);
                    playAnim = false;
                    grabTransform = false;
                    animTimeElapsed = 0;
                }
            }
        }
    }

    public void MoveIn(MoveDirection direction, float playTime)
    {
        animTime = playTime;
        animDirection = direction;

        Vector3 start;
        if (direction == MoveDirection.Left)
            start = new Vector3(horizontalMoveAmount, 0f, 0f);
        else if (direction == MoveDirection.Right)
            start = new Vector3(-horizontalMoveAmount, 0f, 0f);
        else if (direction == MoveDirection.Up)
            start = new Vector3(0, -verticalMoveAmount, 0);
        else
            start = new Vector3(0, verticalMoveAmount, 0);

        uiTransform.anchoredPosition = start;

        playAnim = true;
    }

    public void MoveOut(MoveDirection direction, float playTime)
    {
        animTime = playTime;
        animDirection = direction;

        uiTransform.anchoredPosition = Vector3.zero;

        playAnim = true;
    }
}
