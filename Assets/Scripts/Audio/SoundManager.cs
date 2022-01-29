using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum SoundType {
    BARK,
    FEAR,
    DEATH
}

public class SoundManager : MonoBehaviour
{

    public List<AudioClip> AudioBarkList;
    public List<AudioClip> AudioFearList;
    public List<AudioClip> AudioDeathList;
    public AudioClip VictoryClip;
    public AudioClip DefeatClip;


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

    public void PlayRandomSound(SoundType type, AudioSource source)
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
                currentList.AddRange(AudioFearList);
                break;
        }

        if(currentList.Count > 0)
        {
            source.clip = currentList[Random.Range(0,currentList.Count)];
            source.Play();
        }
    }
}
