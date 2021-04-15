using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GiveMeMoney : MonoBehaviour, IUnityAdsListener
{
    string appleID = "4078366";
    string googleID = "4078367";
    string storeID;

    string interstitialAD;
    string rewardedAD;

    bool testMode = true;
    bool android = true;

    // Initialize the Ads listener and service:
    void Start()
    {
        storeID = android ? googleID : appleID;
        interstitialAD = android ? "Interstitial_Android" : "Interstitial_iOS";
        rewardedAD = android ? "Rewarded_Android" : "Rewarded_iOS";

        Advertisement.AddListener(this);

        InitialiseAdvertisements();
    }
    void InitialiseAdvertisements()
    {
        Advertisement.Initialize(storeID, testMode);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ShowInterstitialAd();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ShowVideoAd();
        }
    }

    void ShowVideoAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(rewardedAD))
        {
            Advertisement.Show(rewardedAD);
            return;
        }

        Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
    }

    void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(interstitialAD))
        {
            Advertisement.Show(interstitialAD);
            return;
        }

        Debug.Log("Rewarded interstital is not ready at the moment! Please try again later!");
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
            {
                Debug.LogWarning("Finished.");
                break;
            }
            case ShowResult.Skipped:
            {
                Debug.LogWarning("Skipped.");
                break;
            }
            case ShowResult.Failed:
            {

                Debug.LogWarning("The ad did not finish due to an error.");
                break;
            }
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == rewardedAD)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }

        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == interstitialAD)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
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

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
