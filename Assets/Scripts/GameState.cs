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

    public string PlayerName = "";
    
    public VehicleBody selectedBody;
    public VehicleWheel selectedWheel;
    public VehicleEngine selectedEngine;
    public IDifficultyStrategy Difficulty = new MediumDifficulty();

    private GameState()
    {
        
    }

    public void updateDifficulty(int strategyIndex)
    {
        if (strategyIndex == 0)
            Difficulty = new EasyDifficulty();
        if (strategyIndex == 1)
            Difficulty = new MediumDifficulty();
        if (strategyIndex == 2)
            Difficulty = new HardDifficulty();

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
