using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterBlade : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OpenTwitter()
    {
        string twitterAddress = "http://twitter.com/intent/tweet";
        string message = "GET THIS AWESOME GAME";//text string
        string descriptionParameter = "GRIZZY";
        string appStoreLink = "https://play.google.com/store/apps/details?id = com.biggrizgames.grizzy";
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(message + "n" + descriptionParameter + "n" + appStoreLink));
    }
}
