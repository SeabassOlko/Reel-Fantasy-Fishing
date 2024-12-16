using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class InputConsumer : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;
    //public UnityEvent m_TouchEvent;
    //public UnityEvent m_SwipeEvent;
    //public UnityEvent m_HoldSwipeEvent;


    //Variables to control tapping and swiping
    float distanceThreshold = 0.8f;
    float swipeThreshold = 0.9f;
    float tapTimeout = 0.1f;
    float swipeTimeout = 0.5f;
    float holdTimeToStart = 0.5f;

    bool pressed = false;

    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        inputManager = GetComponent<InputManager>();
        inputManager.OnTouchBegin += OnTouchStarted;
        inputManager.OnTouchEnd += OnTouchEnded;
    }

    void OnTouchStarted()
    {
        startPos = inputManager.PrimaryPosition();
        pressed = true;
        startTime = Time.time;
    }

    void OnTouchEnded()
    {
        startPos = inputManager.PrimaryPosition();
        pressed = false;
        endTime = Time.time;
        DetectInput();
        playerController.currentlyReeling = false;
    }

    void DetectInput()
    {
        if (endTime - startTime > swipeTimeout)
        {
            Debug.Log("not a tap or swipe");
            return;
        }
        if (endTime - startTime < tapTimeout)
        {
            Tap();
            return;
        }

        //If we reached down here we did not detect a tap, and we are within our swipe threshold
        CheckSwipe();
    }

    void Tap()
    {
        Debug.Log("Tap Happened");
    }

    void Update()
    {
        if (pressed && Time.time - startTime > holdTimeToStart && playerController.IsReeling())
        {
            playerController.currentlyReeling = true;
            playerController.ReelCheck(inputManager.PrimaryPosition());
        }
        if (playerController.IsReeling())
        {
            playerController.ShowTilt(inputManager.LeftRightTilt().x);
        }
    }

    void CheckSwipe()
    {
        float distance = Vector2.Distance(startPos, endPos);

        //no valid swipe detected - pos diference was too low
        if (distance < distanceThreshold) return;

        Vector2 direction = (endPos - startPos).normalized;
        float checkUp = Vector2.Dot(Vector2.up, direction);
        float checkLeft = Vector2.Dot(Vector2.left, direction);
        //Debug.Log("Up: " + checkUp + " Left: " + checkLeft);

        if (checkUp <= -swipeThreshold && playerController.IsCasting())
        {
            Debug.Log("Swipe up");
            playerController.CastHook();
            return;
        }

        if (checkUp >= swipeThreshold && playerController.IsSetting())
        {
            Debug.Log("Swipe Down");
            playerController.setHook();
            return;
        }

        if (checkLeft <= -swipeThreshold)
        {
            Debug.Log("Swipe Left");
            return;
        }

        if (checkLeft >= swipeThreshold)
        {
            Debug.Log("Swipe Right");
            return;
        }
    }
}
