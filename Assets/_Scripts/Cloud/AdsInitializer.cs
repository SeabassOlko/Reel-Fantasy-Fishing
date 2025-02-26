using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    float internetCheckTime = 15.0f;
    float currentTime = 0.0f;
    bool checkingInternet = true;

    void Start()
    {
        int i = 0;
        while (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            if (i > 500000) break;
            Debug.Log("Still checking internet");
            i++;
        }
        Debug.Log($"{Application.internetReachability} // Unity App internet Connection");
    }

    public void InitializeAds()
    {
        _gameId = _androidGameId;

        Advertisement.Initialize(_gameId, _testMode, this);
    }

    private void Update()
    {
        while (checkingInternet)
        {
            UnityWebRequest request = new UnityWebRequest("http://google.com");

            currentTime += Time.deltaTime;

            Debug.Log("Checking for internet");

            if (request.error == null)
            {
                Debug.Log("Internet Found");
                checkingInternet = false;
                InitializeAds();
            }
            else if (currentTime >= internetCheckTime)
                checkingInternet = false;
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

