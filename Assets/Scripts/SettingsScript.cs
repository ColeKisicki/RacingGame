using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{

    [SerializeField] private Slider DifficultySlider;

    private void Start()
    {
        DifficultySlider.onValueChanged.AddListener((float v) => {
            Debug.Log("v: " + v);
        });
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}