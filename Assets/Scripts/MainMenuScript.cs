using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadEditVehicleScene()
    {
        SceneManager.LoadScene("EditVehicleScene");
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
