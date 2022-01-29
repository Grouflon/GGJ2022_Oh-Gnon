using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum GameState
{
    None,
    Setup,
    Game
}

public class GameManager : MonoBehaviour
{
    public int seed
    {
        get { return m_seed; }
        set { Assert.IsTrue(m_gameState == GameState.Setup); m_seed = value; }
    }

    public int localPlayer
    {
        get { return m_localPlayer; }
        set { Assert.IsTrue(m_gameState == GameState.Setup); m_localPlayer = value; }
    }

    [Header("Internal")]
    public SetupScreenController setupScreenController; 

    public void setGameState(GameState _state)
    {
        if (m_gameState == _state)
            return;

        // EXIT STATES
        switch (m_gameState)
        {
            case GameState.Setup:
            {
                setupScreenController.gameObject.SetActive(false);
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
                setupScreenController.gameObject.SetActive(true);
            }
            break;

            case GameState.Game:
            {
                
            }
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        setupScreenController.gameObject.SetActive(false);

        setGameState(GameState.Setup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameState m_gameState = GameState.None;
    
    int m_seed = -1;
    int m_localPlayer = -1;

    // Singleton
    public static GameManager Get()
    {
        if (!m_instance)
        {
            m_instance = FindObjectOfType<GameManager>();
        }
        Assert.IsNotNull(m_instance);
        return m_instance;
    }
    static GameManager m_instance; 
}
