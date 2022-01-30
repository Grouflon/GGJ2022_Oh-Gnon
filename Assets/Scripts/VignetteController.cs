using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;

public class VignetteController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public TMP_Text nameText;

    void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);
    }

    void OnEnable()
    {
        GameManager gm = GameManager.Get();

        Agent agentPrefab = gm.charactersPrefabs[gm.playerObjectives[gm.otherPlayer]];
        
        nameText.text = agentPrefab.infos.Name;
        AgentSkin agentSkin = agentPrefab.GetComponentInChildren<AgentSkin>(true);
        agentSkin.UpdateSkin(skeletonAnimation);
        skeletonAnimation.AnimationName = "Idle";
    }

    void Start()
    {
    }
}
