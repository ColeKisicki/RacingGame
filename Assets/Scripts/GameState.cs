using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameState
{

    private static GameState _gameStateRef = null;
    public Vehicle _playerVehicleRef = null;
    private RaceController raceController;
    // Store this value for knowing if we need to take user to leaderboard upon completing race
    private float worstLeaderboardScore;
    public List<LeaderboardData> scoresToAddToLeaderboard = new List<LeaderboardData>();

    private GameState()
    {
        
    }

    public void setRaceController(RaceController rc)
    {
        raceController = rc;
        return;
    }

    public int GetCurrentLapNumber()
    {
        return raceController.GetCurrentLap();
    }
    
    public float GetElapsedTime()
    {
        return raceController.GetElapsedTime();
    }
    
    public static GameState GetGameState()
    {
        if (_gameStateRef == null)
        {
            _gameStateRef = new GameState();
        }

        return _gameStateRef;
    }

    public class LeaderboardData
    {
        public string name;
        public float time;
    }
}
