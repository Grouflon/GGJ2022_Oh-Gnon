using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void UpdateSkin()
    {
        Skin newSkin = new Skin("skin"); // 1. Create a new empty skin
        AddSkin(skeletonAnimation, newSkin, "Eyebrows/" + Eyebrows);
        AddSkin(skeletonAnimation, newSkin, "Chests/" + Chest);
        AddSkin(skeletonAnimation, newSkin, "Eyes/" + Eyes);
        AddSkin(skeletonAnimation, newSkin, "Mouths/" + Mouth);
        AddSkin(skeletonAnimation, newSkin, "Hairs/" + Hair);
        AddSkin(skeletonAnimation, newSkin, "Head/" + Head);
        AddSkin(skeletonAnimation, newSkin, "Pants/" + Pants);
        AddSkin(skeletonAnimation, newSkin, "Genitals/" + Genitals);
        
        skeletonAnimation.Skeleton.SetSkin(newSkin);
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    }

    SkeletonAnimation skeletonAnimation;
}
