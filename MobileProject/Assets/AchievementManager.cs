using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AchievementManager : MonoBehaviour
{ 
    //class start
    bool isUserAuthenticated = false;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        PlayGamesPlatform.Activate(); // activate playgame platform
        PlayGamesPlatform.DebugLogEnabled = true; //enable debug log
    }
    // Update is called once per frame
    void Update()
    {
        if (!isUserAuthenticated)
        {
            Social.localUser.Authenticate((bool success) => {
                if (success)
                {
                    Debug.Log("You've successfully logged in");
                    isUserAuthenticated = true; // set value to true
                }
                else
                {
                    Debug.Log("Login failed for some reason");
                }
            });
        }
    }

    public void GetAchieve()
    {
        Social.ReportProgress(GrizzyAchievements.achievement_on_an_adventure, 100, (bool success) => { });
        OpenAchievements();
        Debug.Log("Achieve Pressed");
    }

    public void OpenAchievements()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("You've successfully logged in");
                Social.ShowAchievementsUI();
            }
            else
            {
                Debug.Log("Login failed for some reason");
            }
        });
    }

} // end class