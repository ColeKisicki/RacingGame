using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}