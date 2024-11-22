using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobberMechanics : MonoBehaviour
{
    PlayerController playerController;
    FishStats fish;

    [SerializeField]
    GameObject exclamationImage;
    float timeTillBite = 5f;
    float currentTime = 0f;
    [SerializeField]
    float setBiteTime = 5f;
    float currentBiteTime = 0f;
    bool biting = false;

    Slider TensionSlider;
    Slider ProgressSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
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
            StartCoroutine(FishOn());
        }

        if (currentBiteTime <= setBiteTime && biting)
            currentBiteTime += Time.deltaTime;
        else if (currentBiteTime >= setBiteTime && biting && !playerController.IsReeling())
        {
            playerController.MissedFish();
            fish.DestroyFish();
            Destroy(gameObject);
        }
    }

    IEnumerator FishOn()
    {
        exclamationImage.SetActive(true);
        yield return new WaitForSeconds(setBiteTime);
        if (!playerController.IsReeling())
            playerController.MissedFish();
        exclamationImage.SetActive(false);
    }

    public void moveBobber(bool direction, float amount)
    {
        float moveAmount;
        if (direction)
            moveAmount = Mathf.Clamp(transform.position.x + (amount * -1), -1.6f, 1.6f);
        else
            moveAmount = Mathf.Clamp(transform.position.x + amount, -1.6f, 1.6f);

        transform.position = new Vector3(moveAmount, transform.position.y, transform.position.z);
    }

    public void moveBobber(float amount)
    {
        float moveAmount;
        moveAmount = Mathf.Clamp(transform.position.x + amount, -1.6f, 1.6f);

        transform.position = new Vector3(moveAmount, transform.position.y, transform.position.z);
    }

    void SpawnFish()
    {
        fish = Instantiate(FindAnyObjectByType<FishLibrary>().GetFish(0), transform.position, transform.rotation).GetComponent<FishStats>();
        timeTillBite = Random.Range(3f, 8f);
        setBiteTime = setBiteTime / (1 + fish.GetRarity());
    }
}
