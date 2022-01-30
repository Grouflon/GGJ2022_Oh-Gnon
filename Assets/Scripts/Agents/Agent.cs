using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Spine;
using Spine.Unity;

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
    public delegate void AgentStateDelegate(Agent _agent, AgentState _previousState, AgentState _currentState);
    public event AgentStateDelegate OnAgentStateChanged;

    AudioSource audioSource;
    SkeletonAnimation skeletonAnimation;

    void Start()
    {
        agentRigidbody = GetComponent<Rigidbody>();
        agentSpeed = AgentManager.Get().AgentParameters.Speed;

        DragManager.Get().OnStartDragging += OnAgentGrabbed;

        audioSource = GetComponent<AudioSource>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);

        SetState(AgentState.IDLE);
    }

    private void OnDestroy()
    {
        if (DragManager.Get() == null)
            return; 

        DragManager.Get().OnStartDragging -= OnAgentGrabbed;
    }

    public AgentState GetState()
    {
        return agentState;
    }

    public void Kill()
    {
        SetState(AgentState.DEAD);
        if (OnAgentKilled != null) OnAgentKilled(this);
        Destroy(gameObject);
    }

    public void SetState(AgentState state)
    {

        if (agentState == state)
            return;

        if (OnAgentStateChanged != null) OnAgentStateChanged(this, agentState, state);

        agentState = state;

        if (agentState == AgentState.WALK)
        {
            //destination = AgentManager.Get().GetRandomPointInGameArea();
            destination = transform.position + (Vector3) Random.insideUnitCircle * AgentManager.Get().GetRandomWalkDistance();
            destination = AgentManager.Get().ClampPointInGameArea(destination);
            skeletonAnimation.AnimationName = "Walk";
        }

        if (agentState == AgentState.IDLE)
        {
            idleCurrentTimer = 0f;
            idleTime = AgentManager.Get().GetRandomIdleTime();
            skeletonAnimation.AnimationName = "Idle";
        }

        if (agentState == AgentState.DRAGGED)
        {
            skeletonAnimation.AnimationName = "Panic";
        }
    }

    void Idle()
    {
        idleCurrentTimer += Time.deltaTime;
        if (idleCurrentTimer > idleTime)
        {
            SetState(AgentState.WALK);
        }
    }

    void Walk()
    {
        Vector3 move = (destination - transform.position).normalized * agentSpeed;
        transform.position = transform.position + move;
        if (Vector3.Distance(transform.position, destination) < 0.1f)
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

        // Z ordering
        Vector3 position = transform.position;
        if (agentState != AgentState.DRAGGED)
        {
            position.z = position.y * 0.1f;
        }
        else
        {
            position.z = -5.0f;
        }
        transform.position = position;
    }

    private void OnAgentGrabbed(Agent p_agent)
    {
        Debug.Log($"Agent got grabbed ({p_agent.gameObject.name})");
    }
}