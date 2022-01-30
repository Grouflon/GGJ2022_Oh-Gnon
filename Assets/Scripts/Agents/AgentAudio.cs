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
        audioSource.volume = Random.Range(0.01f, 0.02f);
        barkTime = Random.Range(2f, 15f);
    }

    void OnAgentStateChanged(Agent agent, AgentState previousState, AgentState currentState)
    {
        if(previousState != AgentState.DRAGGED && currentState == AgentState.DRAGGED)
        {
            Debug.Log("AUDIO : Play FEAR");
            SoundManager.Get().PlayRandomSound(SoundType.FEAR);
        }

        if(previousState == AgentState.DRAGGED)
        {
            //STOP drag sound
            SoundManager.Get().StopGeneralAudioSource();
        }

        if (currentState == AgentState.DEAD)
        {
            SoundManager.Get().PlayRandomSound(SoundType.DEATH);
        }

        /*if (agent.GetState() == AgentState.WALK || agent.GetState() == AgentState.IDLE)
        {
            audioSource.volume = Random.Range(0.1f, 0.4f);
        }*/
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
