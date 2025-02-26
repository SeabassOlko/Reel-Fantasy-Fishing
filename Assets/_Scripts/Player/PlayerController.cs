using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 firstFingerPos;
    bool firstReel = false;

    [SerializeField]
    float reelSpeed = 0.5f;
    [SerializeField]
    float tiltSpeed = 1f;
    [SerializeField]
    float tensionLossSpeed = 0.5f;
    [SerializeField]
    GameObject bobberPrefab;

    //Menus
    [SerializeField]
    BlankMenu blankMenu;
    [SerializeField]
    FishingMenu fishingMenu;
    [SerializeField]
    CatchMenu catchMenu;
    [SerializeField]
    ReelingMenu reelingMenu;
    [SerializeField]
    LoseMenu loseMenu;

    [SerializeField]
    GameObject RodIdle, RodReeling;
    [SerializeField]
    Transform RodIdleTip, RodReelingTip;
    [SerializeField]
    LineRenderer fishingLine;

    // Player Input Variety Trackers
    bool isPlayerReeling = false;
    public bool currentlyReeling = false;
    bool isShopping = false;
    bool isCasting = true;
    bool isSetting = false;
    bool isHandlingFish = false;

    [SerializeField]
    BobberSliderController bobberSliderController;

    PlayerInventory inventory;

    BobberMechanics bobber;

    AudioSource playerAudioSource;

    [SerializeField]
    AudioClip reelClip;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        playerAudioSource = GetComponent<AudioSource>();
        RodIdle.SetActive(true);
        fishingLine.positionCount = 2;
    }

    void Update()
    {
        if (isPlayerReeling && !currentlyReeling)
        {
            bobberSliderController.RemoveTension(tensionLossSpeed * Time.deltaTime);
        }
        if (isPlayerReeling)
        {
            fishingLine.SetPosition(0, RodReelingTip.position);
            fishingLine.SetPosition(1, bobber.GetComponent<Transform>().position);
        }
        if (isSetting)
        {
            fishingLine.SetPosition(0, RodIdleTip.position);
            fishingLine.SetPosition(1, bobber.GetComponent<Transform>().position);
        }
        if (!isSetting && !isPlayerReeling)
            fishingLine.gameObject.SetActive(false);
    }

    public int GetBait()
    {
        return inventory.GetCurrentBaitType();
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
                    Debug.Log("Top right quad");
                    bobberSliderController.AddProgress(reelSpeed * Time.deltaTime);
                }
            }
            // check top left quadrant
            if (firstFingerPos.y > -3 && firstFingerPos.x < 0)
            {
                if (touchPos.x > firstFingerPos.x || touchPos.y > firstFingerPos.y)
                {
                    Debug.Log("top left quad");
                    bobberSliderController.AddProgress(reelSpeed * Time.deltaTime);
                }
            }
            // check bottom left quadrant
            if (firstFingerPos.y < -3 && firstFingerPos.x < 0)
            {
                if (touchPos.x < firstFingerPos.x || touchPos.y > firstFingerPos.y)
                {
                    Debug.Log("bottom left quad");
                    bobberSliderController.AddProgress(reelSpeed * Time.deltaTime);
                }
            }
            // check bottom right quadrant
            if (firstFingerPos.y < -3 && firstFingerPos.x > 0)
            {
                if (touchPos.x < firstFingerPos.x || touchPos.y < firstFingerPos.y)
                {
                    Debug.Log("bottom right quad");
                    bobberSliderController.AddProgress(reelSpeed * Time.deltaTime);
                }
            }
            firstFingerPos = touchPos;
            if (!playerAudioSource.isPlaying)
                playerAudioSource.PlayOneShot(reelClip);
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
        if (!isCasting || inventory.GetCurrentBaitAmount() == 0)
            return;
        RodIdle.GetComponent<Animator>().SetTrigger("Cast");
        Debug.Log("Casting hook out");
        isCasting = false;
        inventory.UseCurrentBait();
        fishingMenu.Casting();
        inventory.SaveInventory();
    }

    public void SpawnBobber()
    {
        fishingLine.gameObject.SetActive(true);
        bobber = Instantiate(bobberPrefab, new Vector3(0, 0, 0), transform.rotation).GetComponent<BobberMechanics>();
        isSetting = true;
    }

    public void setHook()
    {
        if (!isSetting)
            return;

        if (!bobber.FishBiting())
        {
            bobber.FishEscaped();
            return;
        }
        isPlayerReeling = true;
        isSetting = false;
        RodIdle.SetActive(false);
        RodReeling.SetActive(true);
        blankMenu.SetHook();
    }

    public void MissedFish()
    {
        reelingMenu.FishEscaped();
        fishingMenu.UpdateBait();
        fishingMenu.UpdateCoins();
        RodIdle.SetActive(true);
        RodReeling.SetActive(false);
        isPlayerReeling = false;
        isSetting = false;
        isCasting = true;
        if (CheckGameOver())
            fishingMenu.EnterLoseMenu();
    }

    public void CatchFish(Sprite fishSprite, string fishName, float weight, int value)
    {
        reelingMenu.FishCaught();
        float roundedWeight = Mathf.Round(weight * 100) / 100;
        catchMenu.SetUpFishInfo(fishSprite, fishName, roundedWeight, value);
        RodIdle.SetActive(true);
        RodReeling.SetActive(false);
        isHandlingFish = true;
        isPlayerReeling = false;
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

    public void ShowTilt(float tilt)
    {
        // apply visual tilt to rod
        Quaternion rodRotation = new Quaternion();
        rodRotation.eulerAngles = new Vector3(0, 0, -1 * Mathf.Clamp(tilt * 27, -13.0f, 13.0f));
        RodReeling.transform.rotation = rodRotation;

        if (tilt < -0.1 || tilt > 0.1)
        {
            float tiltToApply = Mathf.Clamp(tilt, -0.6f, 0.6f);
            bobber.moveBobber(tiltToApply * tiltSpeed * Time.deltaTime);
        }
    }

    bool CheckGameOver()
    {
        if (inventory.GetTotalBait() <= 0 && inventory.GetGold() < 15)
        {
            LoadSaveManager.Instance.gameData.playerStats.gameReset = true;
            LoadSaveManager.Instance.Save();

            return true;
        }
        return false;
    }
}
