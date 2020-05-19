using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAYING, WON, LOST, MENU };

public class GameManager : Singleton<GameManager>
{
    private GameState _currentGameState = GameState.PLAYING;
    GameState currentGameState {get {return _currentGameState;}}

    public delegate void GameStateAction(GameState newState);
    public static event GameStateAction OnGameStateUpdated;

    // Start is called before the first frame update
    void Start()
    {
        OnGameStateUpdated += DisplayNewState;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateGameState(GameState newState)
    {
        if (newState == _currentGameState) return;

        _currentGameState = newState;

        // Call OnGameStateUpdated event.
        OnGameStateUpdated(_currentGameState);
    }

    private void DisplayNewState(GameState newState)
    {
        string outputText = "The new state is: ";

        switch (newState)
        {
            case (GameState.LOST):
                outputText += "Lost";
                break;
            
            case (GameState.MENU):
                outputText += "Menu";
                break;
            
            case (GameState.PLAYING):
                outputText += "Playing";
                break;
            
            case (GameState.WON):
                outputText += "Won";
                break;

            default:
                outputText += "Not Found";
                break;
        }

        Debug.Log(outputText);
    }
}
