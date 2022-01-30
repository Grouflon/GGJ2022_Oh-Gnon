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
    public string Hairs = "Pear_Hairs";
    public string Head = "Pear_Head";
    public string Pants = "Pear_Neutral_Pants";
    public string Genitals = "Pear_Vulve";
    public string Hats = "";
    public string Mustaches = "";

    void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSkin(skeletonAnimation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    static void AddSkin(SkeletonAnimation _skeletonAnimation, Skin _parentSkin, string _skinName)
    {
        if (_skinName.Length == 0)
            return;

        if (_skinName[_skinName.Length - 1] == '/')
            return;

        Skin skin = _skeletonAnimation.Skeleton.Data.FindSkin(_skinName);
        if (skin != null)
        {
            _parentSkin.AddSkin(skin);
        }
        else
        {
            Debug.LogError("Unknown skin " + _skinName + " in " + _skeletonAnimation.transform.parent.gameObject.name);
        }
    }

    public void UpdateSkin(SkeletonAnimation _skeletonAnimaton)
    {
        Skin newSkin = new Skin("skin"); // 1. Create a new empty skin
        AddSkin(_skeletonAnimaton, newSkin, "Eyebrows/" + Eyebrows);
        AddSkin(_skeletonAnimaton, newSkin, "Chests/" + Chest);
        AddSkin(_skeletonAnimaton, newSkin, "Eyes/" + Eyes);
        AddSkin(_skeletonAnimaton, newSkin, "Mouths/" + Mouth);
        AddSkin(_skeletonAnimaton, newSkin, "Hairs/" + Hairs);
        AddSkin(_skeletonAnimaton, newSkin, "Head/" + Head);
        AddSkin(_skeletonAnimaton, newSkin, "Pants/" + Pants);
        AddSkin(_skeletonAnimaton, newSkin, "Genitals/" + Genitals);
        AddSkin(_skeletonAnimaton, newSkin, "Hats/" + Hats);
        AddSkin(_skeletonAnimaton, newSkin, "Mustaches/" + Mustaches);

        _skeletonAnimaton.Skeleton.SetSkin(newSkin);
        _skeletonAnimaton.Skeleton.SetSlotsToSetupPose();
    }

    SkeletonAnimation skeletonAnimation;
}