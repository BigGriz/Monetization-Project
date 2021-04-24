using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class AchievementManager : MonoBehaviour
{ 
    //class start
    bool isUserAuthenticated = false;
    int gameplayCount;

    public TMPro.TextMeshProUGUI text;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        PlayGamesPlatform.Activate(); // activate playgame platform
        PlayGamesPlatform.DebugLogEnabled = true; //enable debug log

        gameplayCount = PlayerPrefs.GetInt("GameplayCount");
        gameplayCount++;
        text.SetText(gameplayCount.ToString());

        PlayerPrefs.SetInt("GameplayCount", gameplayCount);
        CheckLoginAchieves();
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

    void CheckLoginAchieves()
    {
        if (gameplayCount >= 5)
            Social.ReportProgress(GrizzyAchievements.achievement_grinding_away, 100, (bool success) => { });
        if (gameplayCount >= 4)
            Social.ReportProgress(GrizzyAchievements.achievement_still_adventuring, 100, (bool success) => { });
        if (gameplayCount >= 3)
            Social.ReportProgress(GrizzyAchievements.achievement_doin_your_thang, 100, (bool success) => { });
        if (gameplayCount >= 2)
            Social.ReportProgress(GrizzyAchievements.achievement_on_an_adventure, 100, (bool success) => { });
        if (gameplayCount >= 1)
            Social.ReportProgress(GrizzyAchievements.achievement_new_beginnings, 100, (bool success) => { });
        if (gameplayCount > 0)
            OpenAchievements();
    }

    public void GetAchieve()
    {
        //Social.ReportProgress(GrizzyAchievements.achievement_on_an_adventure, 100, (bool success) => { });
        OpenAchievements();
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