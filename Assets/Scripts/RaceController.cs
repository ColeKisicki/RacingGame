using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    [SerializeField] public GameObject vehicleBody;
    [SerializeField] private int totalLaps = 3;

    private int currentCheckpointIndex = 0;
    private int currentLap = 1;
    private float raceStartTime;
    private float elapsedTime;

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
        Debug.Log(currentCheckpointIndex);
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
                    Debug.Log("Race finished!");
                    enabled = false;
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
}