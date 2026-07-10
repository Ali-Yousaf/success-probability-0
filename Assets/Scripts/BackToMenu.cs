using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public string sceneName = "Menu";
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
