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
    public int id = -1;
    public CharacterInfos infos;

    AgentState agentState = AgentState.IDLE;
    Vector3 destination;
    Rigidbody agentRigidbody;
    float agentSpeed;
    float idleCurrentTimer;
    float idleTime;

    public delegate void AgentDelegate(Agent _agent);
    public event AgentDelegate OnAgentKilled;
    AudioSource audioSource;

    void Start()
    {
        agentRigidbody = GetComponent<Rigidbody>();
        agentSpeed = AgentManager.Get().AgentParameters.Speed;
        audioSource = GetComponent<AudioSource>();
    }

    public void Kill()
    {
        if (OnAgentKilled != null) OnAgentKilled(this);
        Destroy(gameObject);
    }

    public void SetState(AgentState state)
    {
        agentState = state;

        if(agentState == AgentState.WALK)
        {
            //destination = AgentManager.Get().GetRandomPointInGameArea();
            destination = transform.position + (Vector3)Random.insideUnitCircle * AgentManager.Get().GetRandomWalkDistance();
            destination = AgentManager.Get().ClampPointInGameArea(destination);
        }

        if (agentState == AgentState.IDLE)
        {
            idleCurrentTimer = 0f;
            idleTime = AgentManager.Get().GetRandomIdleTime();
            if(audioSource != null && SoundManager.Get() != null)
            {
                SoundManager.Get().PlayRandomSound(SoundType.BARK, audioSource);
            }
        }
    }

    void Idle()
    {
        idleCurrentTimer += Time.deltaTime;
        if(idleCurrentTimer > idleTime)
        {
            SetState(AgentState.WALK);
        }
    }

    void Walk()
    {
        Vector3 move = (destination - transform.position).normalized * agentSpeed;
        transform.position = transform.position + move;
        if(Vector3.Distance(transform.position, destination) < 0.1f)
        {
           SetState(AgentState.IDLE);
        }
    }

    void Update()
    {
        switch (agentState)
        {
            case (AgentState.WALK):
                Walk();
                break;

            case (AgentState.IDLE):
                Idle();
                break;
        }
    }

    // Temporary, to be replaced by drop
    private void OnMouseUp()
    {
        Kill();
    }
}
