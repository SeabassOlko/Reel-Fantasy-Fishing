using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberMechanics : MonoBehaviour
{
    PlayerController playerController;
    BobberSliderController sliderController;
    GameObject fish;

    Animator bobberAnim;
    AudioSource bobberAudio;

    [SerializeField]
    AudioClip biteChimeClip;

    [SerializeField]
    GameObject exclamationImage;
    [SerializeField]
    float bobberMaxLeftPos = -1.6f, bobberMaxRightPos = 1.6f;
    [SerializeField]
    float tensionScale = 0.5f;
    float timeTillBite = 5f;
    float currentTime = 0f;
    [SerializeField]
    float setBiteTime = 5f;
    float currentBiteTime = 0f;
    bool biting = false;
    bool setHook = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        bobberAnim = GetComponent<Animator>();
        bobberAudio = GetComponent<AudioSource>();
        bobberAnim.Play("BobberIdle");
        SpawnFish();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= timeTillBite)
            currentTime += Time.deltaTime;
        else if (currentTime >= timeTillBite && !biting)
        {
            biting = true;
            bobberAnim.Play("BobberFishOn");
            bobberAudio.PlayOneShot(biteChimeClip);
            exclamationImage.SetActive(true);
        }

        if (currentBiteTime <= setBiteTime && biting)
            currentBiteTime += Time.deltaTime;
        else if (currentBiteTime >= setBiteTime && biting && !playerController.IsReeling())
        {
            FishEscaped();
        }

        if (playerController.IsReeling() && !setHook && biting)
        {
            setHook = true;
            exclamationImage.SetActive(false);
            fish.GetComponent<FishBehavior>().MakeActive();
            sliderController = FindAnyObjectByType<BobberSliderController>();
            sliderController.SetUpSliders();
        }

        if (setHook && playerController.currentlyReeling)
        {
            if (transform.position.x < bobberMaxLeftPos / 3 || transform.position.x > bobberMaxRightPos / 3)
            {
                // if in the last third of bobber space apply full tension
                if (transform.position.x < (bobberMaxLeftPos / 3) * 2 || transform.position.x > (bobberMaxRightPos / 3) * 2)
                    sliderController.AddTension((fish.GetComponent<FishStats>().GetWeight() * tensionScale) * Time.deltaTime);
                // else in the second third apply half tension
                else
                    sliderController.AddTension((fish.GetComponent<FishStats>().GetWeight() * tensionScale) / 2 * Time.deltaTime);
            }
        }
        else if (setHook && !playerController.currentlyReeling)
        {
            sliderController.RemoveProgress((fish.GetComponent<FishStats>().GetWeight() * tensionScale) / 2 * Time.deltaTime);
        }

        if (setHook)
        {
            if (sliderController.GetProgress() >= .99)
            {
                FishStats fishInfo = fish.GetComponent<FishStats>();
                playerController.CatchFish(fishInfo.GetFishSprite(), fishInfo.GetName(), fishInfo.GetWeight(), fishInfo.GetValue());
            }
            if (sliderController.GetTension() >= .99 || sliderController.GetProgress() <= 0.01)
                FishEscaped();
        }
    }

    public void moveBobber(bool direction, float amount)
    {
        float moveAmount;
        if (direction)
            moveAmount = Mathf.Clamp(transform.position.x + (amount * -1), bobberMaxLeftPos, bobberMaxRightPos);
        else
            moveAmount = Mathf.Clamp(transform.position.x + amount, bobberMaxLeftPos, bobberMaxRightPos);

        transform.position = new Vector3(moveAmount, transform.position.y, transform.position.z);
    }

    public void moveBobber(float amount)
    {
        float moveAmount;
        moveAmount = Mathf.Clamp(transform.position.x + amount, bobberMaxLeftPos, bobberMaxRightPos);

        transform.position = new Vector3(moveAmount, transform.position.y, transform.position.z);
    }

    void SpawnFish()
    {
        fish = Instantiate(FindAnyObjectByType<FishLibrary>().GetRandomFish(playerController.GetBait()), transform.position, transform.rotation);
        timeTillBite = Random.Range(3f, 8f);
        setBiteTime = setBiteTime / (1 + fish.GetComponent<FishStats>().GetRarity());
    }

    public void FishEscaped()
    {
        playerController.MissedFish();
        Destroy(fish);
        Destroy(gameObject);
    }

    public bool FishBiting()
    {
        return biting;
    }
}
