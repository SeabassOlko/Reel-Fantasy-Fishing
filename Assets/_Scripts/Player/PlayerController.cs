using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 firstFingerPos;
    bool firstReel = false;

    [SerializeField]
    GameObject bobberPrefab;

    public int level;

    // Player Input Variety Trackers
    bool isPlayerReeling = false;
    bool isShopping = false;
    bool isCasting = true;
    bool isSetting = false;
    bool isHandlingFish = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReelCheck(Vector3 touchPos)
    {
        Debug.Log("ReelCheckCalled");
        if (!firstReel && ReelAreaCheck(touchPos))
        {
            firstReel = true;
            firstFingerPos = touchPos;
        }
        else if (ReelAreaCheck(touchPos))
        {
            // check top right quadrant
            if (firstFingerPos.y > -3 && firstFingerPos.x > 0)
            {
                if (touchPos.x > firstFingerPos.x || touchPos.y < firstFingerPos.y)
                {
                    Debug.Log("top right quad");
                    // add to the reel amount Vector3.Distance(firstFingerPos, touchPos);
                }
            }
            // check top left quadrant
            if (firstFingerPos.y > -3 && firstFingerPos.x < 0)
            {
                if (touchPos.x > firstFingerPos.x || touchPos.y > firstFingerPos.y)
                {
                    Debug.Log("top left quad");
                    // add to the reel amount Vector3.Distance(firstFingerPos, touchPos);
                }
            }
            // check bottom left quadrant
            if (firstFingerPos.y < -3 && firstFingerPos.x < 0)
            {
                if (touchPos.x < firstFingerPos.x || touchPos.y > firstFingerPos.y)
                {
                    Debug.Log("bottom left quad");
                    // add to the reel amount Vector3.Distance(firstFingerPos, touchPos);
                }
            }
            // check bottom right quadrant
            if (firstFingerPos.y < -3 && firstFingerPos.x > 0)
            {
                if (touchPos.x < firstFingerPos.x || touchPos.y < firstFingerPos.y)
                {
                    Debug.Log("bottom right quad");
                    // add to the reel amount Vector3.Distance(firstFingerPos, touchPos);
                }
            }
            firstFingerPos = touchPos;
        }
    }

    bool ReelAreaCheck(Vector3 touchPos)
    {
        if (touchPos.x > -1.85 && touchPos.x < 1.85 && touchPos.y < -1.2 && touchPos.y > -4.8)
            return true;
        else
            return false;
    }

    public void CastHook()
    {
        Debug.Log("Casting hook out");
        isCasting = false;
        isSetting = true;
        Instantiate(bobberPrefab, new Vector3(0, 0, 0), transform.rotation);
    }

    public void setHook()
    {
        isPlayerReeling = true;
        isSetting = false;
    }

    public void MissedFish()
    {
        Debug.Log("Did not hook fish in time");
    }

    public bool IsReeling()
    {
        return isPlayerReeling;
    }

    public bool IsShopping()
    {
        return isShopping;
    }

    public bool IsCasting()
    {
        return isCasting;
    }

    public bool IsSetting() 
    {
        return isSetting;
    }

    public bool IsHandlingFish()
    {
        return isHandlingFish;
    }

    public void SetIsReeling(bool reeling)
    {
        isPlayerReeling = reeling;
    }

    public void SetIsShopping(bool shopping)
    {
        isShopping = shopping;
    }
    public void SetIsCasting(bool casting)
    {
        isCasting = casting;
    }

    public void SetIsHandlingFish(bool handling)
    {
        isHandlingFish = handling;
    }
}
