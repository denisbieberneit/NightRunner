using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    private string gameId = "4082869";

    [SerializeField] public GameObject deathScreen;
    [SerializeField] public GameObject adScreen;
    public string mySurfacingId = "rewardedAds";

    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);

    }

    // Implement a function for showing a rewarded video ad:
    public void ShowRewardedVideo()
    {
        Advertisement.Show();
        deathScreen.SetActive(true);
        adScreen.SetActive(false);
    }

    public void OnUnityAdsReady(string mySurfacingId)
    {

    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + PlayerPrefs.GetInt("PlayerCoinsRun"));
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("SkippedAd");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
