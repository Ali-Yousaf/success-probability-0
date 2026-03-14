using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayDiceRoll()
    {
        SceneManager.LoadScene("Dice Roll");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
