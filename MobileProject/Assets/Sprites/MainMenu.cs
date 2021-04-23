using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        AudioManager.instance.PlayAudio("UI");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        AudioManager.instance.PlayAudio("UI");
        Application.Quit();
    }
}
