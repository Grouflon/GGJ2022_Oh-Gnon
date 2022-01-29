using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

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
    public delegate void AgentStateDelegate(Agent _agent, AgentState _state);
    public event AgentStateDelegate OnAgentStateChanged;

    AudioSource audioSource;
    SkeletonAnimation skeletonAnimation;

    void Start()
    {
        agentRigidbody = GetComponent<Rigidbody>();
        agentSpeed = AgentManager.Get().AgentParameters.Speed;

        audioSource = GetComponent<AudioSource>();
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);

        UpdateSkin(infos);

        SetState(AgentState.IDLE);
    }

    static void AddSkin(SkeletonAnimation _skeletonAnimation, Skin _parentSkin, string _skinName)
    {
        Skin skin = _skeletonAnimation.Skeleton.Data.FindSkin(_skinName);
        if (skin != null)
        {
            _parentSkin.AddSkin(skin);
        }
        else
        {
            Debug.LogError("Unknown skin " + _skinName);
        }
    }

    void UpdateSkin(CharacterInfos _infos)
    {
        Skin newSkin = new Skin("skin"); // 1. Create a new empty skin
        AddSkin(skeletonAnimation, newSkin, "Eyebrows/" + _infos.Eyebrows);
        AddSkin(skeletonAnimation, newSkin, "Chests/" + _infos.Chest);
        AddSkin(skeletonAnimation, newSkin, "Eyes/" + _infos.Eyes);
        AddSkin(skeletonAnimation, newSkin, "Mouths/" + _infos.Mouth);
        AddSkin(skeletonAnimation, newSkin, "Hairs/" + _infos.Hair);
        AddSkin(skeletonAnimation, newSkin, "Head/" + _infos.Head);
        AddSkin(skeletonAnimation, newSkin, "Pants/" + _infos.Pants);
        AddSkin(skeletonAnimation, newSkin, "Genitals/" + _infos.Genitals);
        
        skeletonAnimation.Skeleton.SetSkin(newSkin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
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
        agentState = state;

        if (OnAgentStateChanged != null) OnAgentStateChanged(this, agentState);

        if (agentState == AgentState.WALK)
        {
            //destination = AgentManager.Get().GetRandomPointInGameArea();
            destination = transform.position + (Vector3)Random.insideUnitCircle * AgentManager.Get().GetRandomWalkDistance();
            destination = AgentManager.Get().ClampPointInGameArea(destination);
            skeletonAnimation.AnimationName = "Walk";
        }

        if (agentState == AgentState.IDLE)
        {
            idleCurrentTimer = 0f;
            idleTime = AgentManager.Get().GetRandomIdleTime();
            skeletonAnimation.AnimationName = "Idle";
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
