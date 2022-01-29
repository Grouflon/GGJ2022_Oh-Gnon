using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AgentManager : MonoBehaviour
{
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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
