using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    [SerializeField] public GameObject vehicleBody;
    [SerializeField] private int totalLaps = 1;

    private int currentCheckpointIndex = 0;
    private int currentLap = 1;
    private float raceStartTime;
    private float elapsedTime;

    //public event Action<string, float> OnRaceOver;

    private void Start()
    {
        raceStartTime = Time.time;
        GameState.GetGameState().setRaceController(this);
    }

    private void Update()
    {
        if (vehicleBody == null)
        {
            vehicleBody = GameState.GetGameState()._playerVehicleRef.bodyInstance;
        }
        elapsedTime = Time.time - raceStartTime;
    }

    public void CheckpointReached(int checkpointIndex)
    {
        Debug.Log("currentCheckpointIndex: " + currentCheckpointIndex);
        if (checkpointIndex == currentCheckpointIndex)
        {
            currentCheckpointIndex++;
            if (currentCheckpointIndex >= GetComponentInChildren<Transform>().childCount)
            {
                currentCheckpointIndex = 0;
                currentLap++;
                Debug.Log("LAP");
                if (currentLap > totalLaps)
                {
                    Debug.Log("Race finished! Time: " + elapsedTime);
                    enabled = false;
                    Invoke("NavigateToMainMenu", 10f);
                    //OnRaceOver.Invoke("SAM", GetElapsedTime());
                    GameState.GetGameState().scoresToAddToLeaderboard.Add(new GameState.LeaderboardData { name = "SAM", time = (float)Math.Round(GetElapsedTime(), 2) });
                }
            }
        }
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}