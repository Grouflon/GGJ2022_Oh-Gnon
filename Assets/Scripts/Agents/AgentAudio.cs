using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudio : MonoBehaviour
{
    Agent agent;

    AudioSource audioSource;
    float barkCurrentTimer;
    float barkTime;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        agent.OnAgentStateChanged += OnAgentStateChanged;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Random.Range(0.2f, 0.5f);
        barkTime = Random.Range(0f, 15f);
    }

    void OnAgentStateChanged(Agent agent, AgentState state)
    {
        if(state == AgentState.DRAGGED)
        {
            audioSource.volume = 1f;
            SoundManager.Get().PlayRandomSound(SoundType.FEAR, audioSource);
        }

        if (state == AgentState.DEAD)
        {
            audioSource.volume = 1f;
            SoundManager.Get().PlayRandomSound(SoundType.DEATH, audioSource);
        }

        if (agent.GetState() == AgentState.WALK || agent.GetState() == AgentState.IDLE)
        {
            audioSource.volume = Random.Range(0.2f, 1f);
        }
    }

    void Update()
    {
        if(agent.GetState() == AgentState.WALK || agent.GetState() == AgentState.IDLE)
            Bark();
    }

    void Bark()
    {
        barkCurrentTimer += Time.deltaTime;
        if (barkCurrentTimer > barkTime)
        {
            if (audioSource != null && SoundManager.Get() != null)
            {
                SoundManager.Get().PlayRandomSound(SoundType.BARK, audioSource);
            }
            barkCurrentTimer = 0f;
            barkTime = AgentManager.Get().GetRandomBarkTime();
        }
    }

}
