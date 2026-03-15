using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayHeadsTails()
    {
        SceneManager.LoadScene("HeadsTails");
    }

    public void PlayNumberGuess()
    {
        SceneManager.LoadScene("NumberGuess");
    }

    public void PlayerTargetHit()
    {
        SceneManager.LoadScene("Hit The Target");
    }

    public void PlayLuckyDraw()
    {
        SceneManager.LoadScene("LuckyDraw");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
