using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AgentManager : MonoBehaviour
{
    public AgentParameters AgentParameters;
    public Transform TopLeftCorner;
    public Transform BottomRightCorner;

    public delegate void AgentDelegate(Agent _agent);
    public event AgentDelegate OnAgentKilled;

    // Read only
    public List<Agent> agents;

    // Singleton
    public static AgentManager Get()
    {
        if (!m_instance)
        {
            m_instance = FindObjectOfType<AgentManager>();
        }
        Assert.IsNotNull(m_instance);
        return m_instance;
    }
    static AgentManager m_instance;

    public void SpawnAgents(List<Agent> _charactersPrefabs)
    {
        int id = 0;
        foreach (Agent prefab in _charactersPrefabs)
        {
            Vector3 position = GetRandomPointInGameArea();
            Agent agent = Instantiate<Agent>(prefab, position, Quaternion.identity);
            agent.id = id;

            agent.OnAgentKilled += OnSpawnedAgentKilled;

            //Say a phrase when spawned (with a start delay)
            agent.gameObject.GetComponent<SayPhrase>().SayPhraseAtBeginning();

            agents.Add(agent);
            ++id;
        }
    }

    public void ClearAllAgents()
    {
        foreach (Agent agent in agents)
        {
            Destroy(agent.gameObject);
        }
        agents.Clear();
    }

    public Vector3 GetRandomPointInGameArea()
    {
        float randomX = Random.Range(TopLeftCorner.position.x, BottomRightCorner.position.x);
        float randomY = Random.Range(BottomRightCorner.position.y, TopLeftCorner.position.y);

        return new Vector3(randomX, randomY, 0);
    }

    public float GetRandomIdleTime()
    {
        return Random.Range(AgentParameters.MinTimeBetweenWalks, AgentParameters.MaxTimeBetweenWalks);
    }

    public float GetRandomBarkTime()
    {
        return Random.Range(AgentParameters.MinTimeBetweenBarks, AgentParameters.MaxTimeBetweenBarks);
    }

    public float GetRandomWalkDistance()
    {
        return Random.Range(AgentParameters.MinWalkDistance, AgentParameters.MaxWalkDistance);
    }

    public Vector3 ClampPointInGameArea(Vector3 point)
    {
        float clampedX = Mathf.Clamp(point.x, TopLeftCorner.position.x, BottomRightCorner.position.x);
        float clampedY = Mathf.Clamp(point.y, BottomRightCorner.position.y, TopLeftCorner.position.y);

        return new Vector3(clampedX, clampedY, 0);
    }

    void Start()
    {
        agents = new List<Agent>();

        Vector3 brWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 50f, 20f, 0f));
        Vector3 tlWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(50f, Screen.height - 100f, 0f));
        brWorldPoint.z = 0f;
        tlWorldPoint.z = 0f;
        TopLeftCorner.transform.position = tlWorldPoint;
        BottomRightCorner.transform.position = brWorldPoint;
    }

    void OnSpawnedAgentKilled(Agent _agent)
    {
        bool result = agents.Remove(_agent);
        Assert.IsTrue(result);

        foreach (Agent agentTemp in agents)
        {
            //Say a phrase when spawned (with a start delay)
            agentTemp.gameObject.GetComponent<SayPhrase>().SayPhraseWhenACharaDie(_agent.id);
        }

        if (OnAgentKilled != null) OnAgentKilled(_agent);
    }

}
