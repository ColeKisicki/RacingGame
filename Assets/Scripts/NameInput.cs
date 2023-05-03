using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    public void ReadNameInput(string input)
    {
        GameState.GetGameState().PlayerName = input;
    }
}