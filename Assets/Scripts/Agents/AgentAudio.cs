using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAudio : MonoBehaviour
{
    public VoiceType voiceType;

    Agent agent;

    AudioSource audioSource;
    float barkCurrentTimer;
    float barkTime;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        agent.OnAgentStateChanged += OnAgentStateChanged;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Random.Range(0.03f, 0.05f);
        barkTime = Random.Range(2f, 15f);
        //voiceType = (VoiceType)Random.Range(0, 3);
    }

    void OnAgentStateChanged(Agent agent, AgentState previousState, AgentState currentState)
    {
        if(previousState != AgentState.DRAGGED && currentState == AgentState.DRAGGED)
        {
            Debug.Log("AUDIO : Play FEAR");
            SoundManager.Get().PlayRandomSoundByVoiceType(SoundType.FEAR, voiceType);
        }

        if(previousState == AgentState.DRAGGED)
        {
            //STOP drag sound
            SoundManager.Get().StopGeneralAudioSource();
        }

        if (currentState == AgentState.DEAD)
        {
            SoundManager.Get().PlayRandomSoundByVoiceType(SoundType.DEATH, voiceType);
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
                SoundManager.Get().PlayRandomSoundByVoiceType(SoundType.BARK, voiceType, audioSource);
            }
            barkCurrentTimer = 0f;
            barkTime = AgentManager.Get().GetRandomBarkTime();
        }
    }

}
