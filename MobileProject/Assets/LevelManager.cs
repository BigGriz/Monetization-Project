using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area
{
    CAVERNS = 0,
    FOREST,
    MOUNTAINS
}


public class LevelManager : MonoBehaviour
{
    public GameSettings settings;

    private void Start()
    {
        settings = CallbackHandler.instance.settings;
        TempReset();

        CallbackHandler.instance.nextLevel += NextLevel;
        Invoke("Setup", 0.1f);
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.nextLevel -= NextLevel;
    }

    void Setup()
    {
        CallbackHandler.instance.UpdateLevel(settings.area.ToString(), settings.stage);
    }

    void TempReset()
    {
        settings.area = Area.CAVERNS;
        settings.stage = 1;
    }

    public void NextLevel()
    {
        settings.stage++;
        if (settings.stage > 5)
        {
            settings.stage = 0;
            settings.area++;
        }
        Setup();

        CallbackHandler.instance.Fade(false);
    }
}
