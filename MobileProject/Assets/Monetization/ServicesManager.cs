using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServicesManager : MonoBehaviour
{
    #region Singleton & Setup
    public static ServicesManager instance;

    AdManager adManager;
    AchievementManager achievementManager;
    IAPManager iapManager;
    TwitterBlade twitterBlade;
    LeaderBoardManager leaderBoardManager;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one ServicesManager exists!");
            Application.Quit();
            return;
        }

        adManager = GetComponentInChildren<AdManager>();
        achievementManager = GetComponentInChildren<AchievementManager>();
        iapManager = GetComponentInChildren<IAPManager>();
        twitterBlade = GetComponentInChildren<TwitterBlade>();
        leaderBoardManager = GetComponentInChildren<LeaderBoardManager>();

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion Singleton & Setup

    #region Callbacks
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion Callbacks


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.buildIndex)
        {
            // Main Menu
            case 0:
            {

                break;
            }
            // Game Scene
            case 1:
            {
                adManager.ShowInterstitialAd();
                break;
            }
        }


        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }
}
