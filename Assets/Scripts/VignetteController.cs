using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;

public class VignetteController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public TMP_Text nameText;
    public Button forfeitButton;

    void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);
    }

    void OnEnable()
    {
        GameManager gm = GameManager.Get();
        if (gm.getGameState() != GameState.Game)
            return;

        if (gm.playerObjectives.Length == 0)
            return;

        Agent agentPrefab = gm.charactersPrefabs[gm.playerObjectives[gm.otherPlayer]];
        
        nameText.text = agentPrefab.infos.Name;
        AgentSkin agentSkin = agentPrefab.GetComponentInChildren<AgentSkin>(true);
        agentSkin.UpdateSkin(skeletonAnimation);
        skeletonAnimation.AnimationName = "Idle";
    }

    void Start()
    {
        forfeitButton.onClick.AddListener(
        () => {
            if (GameManager.Get().getGameState() == GameState.Game)
            {
            GameManager.Get().setGameState(GameState.GameOver);
            }
        });
    }
}
