using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
