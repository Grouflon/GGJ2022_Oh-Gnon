using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AgentState
{
    IDLE,
    WALK,
    DRAGGED,
    DEAD
}

public class Agent : MonoBehaviour
{

    AgentState agentState = AgentState.IDLE;
    Vector2 destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetState(AgentState state)
    {
        agentState = state;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
