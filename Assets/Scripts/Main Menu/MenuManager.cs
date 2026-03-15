using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayHeadsTails()
    {
        SceneManager.LoadScene("HeadsTails");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
