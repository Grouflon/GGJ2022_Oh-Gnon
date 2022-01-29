using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AgentManager : MonoBehaviour
{
    public AgentParameters AgentParameters;
    public Transform TopLeftCorner;
    public Transform BottomRightCorner;

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
}
