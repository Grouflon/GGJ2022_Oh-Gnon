using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum SoundType {
    BARK,
    FEAR,
    DEATH
}

public enum VoiceType
{
    MARIE,
    HUGO,
    MAX
}

public class SoundManager : MonoBehaviour
{

    public List<AudioClip> AudioBarkList;
    public List<AudioClip> AudioFearList;
    public List<AudioClip> AudioDeathList;

    public List<AudioClip> MarieBarkList;
    public List<AudioClip> MarieFearList;
    public List<AudioClip> MarieDeathList;

    public List<AudioClip> HugoBarkList;
    public List<AudioClip> HugoFearList;
    public List<AudioClip> HugoDeathList;

    public List<AudioClip> MaxBarkList;
    public List<AudioClip> MaxFearList;
    public List<AudioClip> MaxDeathList;

    public AudioClip VictoryClip;
    public AudioClip DefeatClip;

    AudioSource generalSource;

    private void Awake()
    {
        generalSource = GetComponent<AudioSource>();
    }

    public static SoundManager Get()
    {
        if (!m_instance)
        {
            m_instance = FindObjectOfType<SoundManager>();
        }
        Assert.IsNotNull(m_instance);
        return m_instance;
    }
    static SoundManager m_instance;

    public void PlayRandomSound(SoundType type, AudioSource source = null)
    {
        List<AudioClip> currentList = new List<AudioClip>();

        switch (type)
        {
            case (SoundType.BARK):
                currentList.AddRange(AudioBarkList);
                break;

            case (SoundType.FEAR):
                currentList.AddRange(AudioFearList);
                break;

            case (SoundType.DEATH):
                currentList.AddRange(AudioDeathList);
                break;
        }

        if(currentList.Count > 0)
        {
            if (source)
            {
                source.clip = currentList[Random.Range(0, currentList.Count)];
                source.Play();
            }
            else
            {
                generalSource.clip = currentList[Random.Range(0, currentList.Count)];
                generalSource.Play();
            }
        }
    }

    public void PlayRandomSoundByVoiceType(SoundType type, VoiceType voiceType, AudioSource source = null)
    {
        List<AudioClip> currentList = new List<AudioClip>();

        List<AudioClip> currentBarkList = new List<AudioClip>();
        List<AudioClip> currentFearList = new List<AudioClip>();
        List<AudioClip> currentDeathList = new List<AudioClip>();

        switch (voiceType)
        {
            case (VoiceType.HUGO):
                currentBarkList.AddRange(HugoBarkList);
                currentFearList.AddRange(HugoFearList);
                currentDeathList.AddRange(HugoDeathList);
                break;

            case (VoiceType.MARIE):
                currentBarkList.AddRange(MarieBarkList);
                currentFearList.AddRange(MarieFearList);
                currentDeathList.AddRange(MarieDeathList);
                break;

            case (VoiceType.MAX):
                currentBarkList.AddRange(MaxBarkList);
                currentFearList.AddRange(MaxFearList);
                currentDeathList.AddRange(MaxDeathList);
                break;
        }

        switch (type)
        {
            case (SoundType.BARK):
                currentList.AddRange(currentBarkList);
                break;

            case (SoundType.FEAR):
                currentList.AddRange(currentFearList);
                break;

            case (SoundType.DEATH):
                currentList.AddRange(currentDeathList);
                break;
        }

        if (currentList.Count > 0)
        {
            if (source)
            {
                source.clip = currentList[Random.Range(0, currentList.Count)];
                source.Play();
            }
            else
            {
                generalSource.clip = currentList[Random.Range(0, currentList.Count)];
                generalSource.Play();
            }
        }
    }

    public void StopGeneralAudioSource()
    {
        generalSource.Stop();
    }

    public void PlayEndClip(bool hasWon)
    {
        generalSource.clip = hasWon ? VictoryClip : DefeatClip;
        generalSource.Play();
    }
}
