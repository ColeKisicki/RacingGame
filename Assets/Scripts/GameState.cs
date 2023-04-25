using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{

    private static GameState _gameStateRef = null;
    public Vehicle _playerVehicleRef = null;
    private GameState()
    {
        
    }

    public static GameState GetGameState()
    {
        if (_gameStateRef == null)
        {
            _gameStateRef = new GameState();
        }

        return _gameStateRef;
    }
}
