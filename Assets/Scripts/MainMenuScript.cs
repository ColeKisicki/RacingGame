using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
