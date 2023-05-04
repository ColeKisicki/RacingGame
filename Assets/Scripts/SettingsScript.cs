using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    [SerializeField] private Slider DifficultySlider;

    private void Start()
    {
        //updates the difficulty strategy in the game state
        DifficultySlider.onValueChanged.AddListener((float v) => {
            GameState.GetGameState().updateDifficulty((int)v);
        });
    }
}