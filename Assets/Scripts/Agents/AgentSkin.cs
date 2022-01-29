using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Spine;
using Spine.Unity;

public class AgentSkin : MonoBehaviour
{
    public string Eyebrows = "Neutral_Eyebrow";
    public string Chest = "Chest_Pear";
    public string Eyes = "Neutral_Eyes";
    public string Mouth = "Mouth_Dumb";
    public string Hair = "Pear_Hairs";
    public string Head = "Pear_Head";
    public string Pants = "Pear_Neutral_Pants";
    public string Genitals = "Pear_Vulve";

    void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void UpdateSkin()
    {
        SkeletonAnimation mySkeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);

        Skin newSkin = new Skin("skin"); // 1. Create a new empty skin
        AddSkin(mySkeletonAnimation, newSkin, "Eyebrows/" + Eyebrows);
        AddSkin(mySkeletonAnimation, newSkin, "Chests/" + Chest);
        AddSkin(mySkeletonAnimation, newSkin, "Eyes/" + Eyes);
        AddSkin(mySkeletonAnimation, newSkin, "Mouths/" + Mouth);
        AddSkin(mySkeletonAnimation, newSkin, "Hairs/" + Hair);
        AddSkin(mySkeletonAnimation, newSkin, "Head/" + Head);
        AddSkin(mySkeletonAnimation, newSkin, "Pants/" + Pants);
        AddSkin(mySkeletonAnimation, newSkin, "Genitals/" + Genitals);
        
        mySkeletonAnimation.Skeleton.SetSkin(newSkin);
        mySkeletonAnimation.Skeleton.SetSlotsToSetupPose();
    }

    SkeletonAnimation skeletonAnimation;
}

 [CustomEditor(typeof(AgentSkin))]
 class AgentSkinEditor : Editor
 {
    AgentSkin agentSkin;

    void OnEnable()
    {
        agentSkin = target as AgentSkin;
        agentSkin.UpdateSkin();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Apply Skin"))
        {
            agentSkin.UpdateSkin();
        }
    }
}
