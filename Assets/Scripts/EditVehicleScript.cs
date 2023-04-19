using UnityEngine;
using UnityEngine.SceneManagement;

public class EditVehicleScript : MonoBehaviour
{

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}