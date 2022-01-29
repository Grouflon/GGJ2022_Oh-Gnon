using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    Setup,
    Game
}

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setGameState(GameState.Setup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setGameState(GameState _state)
    {
        if (m_gameState == _state)
            return;

        // EXIT STATES
        switch (m_gameState)
        {
            case GameState.Setup:
            {

            }
            break;

            case GameState.Game:
            {

            }
            break;
        }

        m_gameState = _state;

        // ENTER STATES
        switch (m_gameState)
        {
            case GameState.Setup:
            {

            }
            break;

            case GameState.Game:
            {
                
            }
            break;
        }
    }

    GameState m_gameState = GameState.None;
    int m_seed = -1;
    int m_localPlayer = -1;
}
