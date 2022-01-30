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
    FEAR,
    DEAD
}

public enum EFatality
{
    MOUTH,
    PENTACLE
}

[System.Serializable]
public struct IdleAnim
{
    public string Anim;
    public float Duration;
}

public class Agent : MonoBehaviour
{
    public int id = -1;
    public CharacterInfos infos;
    public GameObject deathFX;

    public List<IdleAnim> idles;

    AgentState agentState = AgentState.IDLE;
    Vector3 destination;
    Rigidbody agentRigidbody;
    float agentSpeed;
    float idleCurrentTimer;
    float idleTime;
    Vector3 fearSource;

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

        audioSource = GetComponent<AudioSource>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);

        SetState(AgentState.IDLE);
    }

    private void OnDestroy()
    {
        if (DragManager.Get() == null)
            return;
    }

    public AgentState GetState()
    {
        return agentState;
    }

    public void Kill(EFatality p_fatality)
    {
        if (OnAgentKilled != null) OnAgentKilled(this);
        StartCoroutine(_Kill(p_fatality));
    }

    private IEnumerator _Kill(EFatality p_fatality)
    {
        GetComponent<Collider2D>().enabled = false;

        switch (p_fatality)
        {
            case EFatality.MOUTH:
                skeletonAnimation.AnimationName = "Disappear";
                yield return new WaitForSeconds(0.3f);
                break;

            case EFatality.PENTACLE:
                skeletonAnimation.AnimationName = "Explosion";
                yield return new WaitForSeconds(0.167f);
                if (deathFX != null)
                    Instantiate(deathFX, transform.position, transform.rotation);
                break;
        }

        SetState(AgentState.DEAD);
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
            destination = transform.position + (Vector3) Random.insideUnitCircle * AgentManager.Get().GetRandomWalkDistance();
            destination = AgentManager.Get().ClampPointInGameArea(destination);
            skeletonAnimation.AnimationName = "Walk";
        }

        if (agentState == AgentState.FEAR)
        {
            Vector3 fearSourceToAgent = transform.position - fearSource;
            fearSourceToAgent.z = 0f;
            float distanceToFearSource = fearSourceToAgent.magnitude;
            float fearedDistance = Mathf.Max(Random.Range(3f, 5f), distanceToFearSource);
            destination = fearSource + fearSourceToAgent.normalized * fearedDistance;
            destination = AgentManager.Get().ClampPointInGameArea(destination);
            skeletonAnimation.AnimationName = "Panic";
        }

        if (agentState == AgentState.IDLE)
        {

            idleCurrentTimer = 0f;
            idleTime = AgentManager.Get().GetRandomIdleTime();
            skeletonAnimation.AnimationName = "Idle";

            if (Random.Range(0f, 1f) > 0.9f)
            {
                var randIdle = idles[Random.Range(0, idles.Count)];
                skeletonAnimation.AnimationName = randIdle.Anim;
                idleTime += randIdle.Duration;
            }
        }

        if (agentState == AgentState.DRAGGED)
        {
            skeletonAnimation.AnimationName = "Panic";

            foreach (Agent otherAgent in AgentManager.Get().agents)
            {
                if (otherAgent == this)
                    continue;

                otherAgent.OnOtherAgentGrabbed(this);
            }
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

    void Walk(float _speed)
    {
        Vector3 move = (destination - transform.position).normalized * _speed;
        transform.position = transform.position + move;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(destination.x, destination.y)) < 0.1f)
        {
            SetState(AgentState.IDLE);
        }
    }

    void Fear()
    {
        Walk(agentSpeed * 1.5f);
    }

    void Update()
    {
        switch (agentState)
        {
            case (AgentState.WALK):
                Walk(agentSpeed);
                break;

            case (AgentState.IDLE):
                Idle();
                break;

            case (AgentState.FEAR):
                Fear();
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

    private void OnOtherAgentGrabbed(Agent p_agent)
    {
        fearSource = p_agent.transform.position;
        SetState(AgentState.FEAR);
    }

    public void OnGrabbed()
    {
        Debug.Log($"Agent got grabbed ({gameObject.name})");
        SetState(AgentState.DRAGGED);
    }
}