using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum GameState
{
    None,
    Menu,
    Rules,
    Setup,
    Game,
    GameOver
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

    public int otherPlayer
    {
        get { return m_localPlayer == 0 ? 1 : 0; }
    }

    public bool isWinning
    {
        get { return m_isWinning; }
    }

    public List<Agent> charactersPrefabs;
    public int[] playerObjectives;

    [Header("Debug")]
    public bool quickStart = false;

    [Header("Internal")]
    public MenuController menuController;
    public RulesController rulesController;
    public SetupScreenController setupScreenController; 
    public GameOverScreenController gameOverScreenController;
    public VignetteController vignetteController;
    public AgentManager agentManager;

    public GameObject dropZonesContainer;

    public void onAgentKilled(Agent _agent)
    {
        if (playerObjectives[m_localPlayer] == _agent.id)
        {
            setGameState(GameState.GameOver);
            SoundManager.Get().PlayEndClip(false);
        }
        else if (AgentManager.Get().agents.Count == 1)
        {
            Assert.IsTrue(AgentManager.Get().agents[0].id == playerObjectives[m_localPlayer]);
            m_isWinning = true;
            setGameState(GameState.GameOver);
            SoundManager.Get().PlayEndClip(true);
        }
    }

    public GameState getGameState() { return m_gameState; }

    public void setGameState(GameState _state)
    {
        if (m_gameState == _state)
            return;

        // EXIT STATES
        switch (m_gameState)
        {
            case GameState.Menu:
            {
                menuController.gameObject.SetActive(false);
            }
            break;

            case GameState.Rules:
            {
                rulesController.gameObject.SetActive(false);
            }
            break;

            case GameState.Setup:
            {
                setupScreenController.gameObject.SetActive(false);
            }
            break;

            case GameState.Game:
            {
                vignetteController.gameObject.SetActive(false);
                dropZonesContainer.SetActive(false);
            }
            break;

            case GameState.GameOver:
            {
                gameOverScreenController.gameObject.SetActive(false);
                vignetteController.gameObject.SetActive(false);
                dropZonesContainer.SetActive(false);
            }
            break;
        }

        m_gameState = _state;

        // ENTER STATES
        switch (m_gameState)
        {
            case GameState.Menu:
            {
                menuController.gameObject.SetActive(true);
            }
            break;

            case GameState.Rules:
            {
                rulesController.gameObject.SetActive(true);
            }
            break;

            case GameState.Setup:
            {
                AgentManager.Get().ClearAllAgents();
                m_localPlayer = -1;
                m_seed = -1;
                playerObjectives[0] = -1;
                playerObjectives[1] = -1;

                setupScreenController.gameObject.SetActive(true);
            }
            break;

            case GameState.Game:
            {
                m_isWinning = false;

                Random.InitState(m_seed);
                playerObjectives[0] = Random.Range(0, charactersPrefabs.Count);
                playerObjectives[1] = Random.Range(0, charactersPrefabs.Count);

                AgentManager.Get().SpawnAgents(charactersPrefabs);
                Debug.Log(charactersPrefabs[playerObjectives[0]].infos.Name);
                Debug.Log(charactersPrefabs[playerObjectives[1]].infos.Name);

                dropZonesContainer.SetActive(true);
                vignetteController.gameObject.SetActive(true);
            }
            break;

            case GameState.GameOver:
            {
                dropZonesContainer.SetActive(true);
                gameOverScreenController.gameObject.SetActive(true);
                vignetteController.gameObject.SetActive(true);

                foreach (var agent in agentManager.agents)
                {
                    Destroy(agent.gameObject.GetComponent<Draggable>());
                    Destroy(agent.gameObject.GetComponent<Collider2D>());
                }
            }
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerObjectives = new int[2];
        
        menuController.gameObject.SetActive(false);
        rulesController.gameObject.SetActive(false);
        setupScreenController.gameObject.SetActive(false);
        gameOverScreenController.gameObject.SetActive(false);
        vignetteController.gameObject.SetActive(false);
        dropZonesContainer.SetActive(false);

        AgentManager.Get().OnAgentKilled += onAgentKilled;

        if (quickStart)
        {
            m_localPlayer = 0;
            m_seed = 0;
            setGameState(GameState.Game);
        }
        else
        {
            setGameState(GameState.Menu);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameState m_gameState = GameState.None;
    
    int m_seed = -1;
    int m_localPlayer = -1;
    bool m_isWinning = false;

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
