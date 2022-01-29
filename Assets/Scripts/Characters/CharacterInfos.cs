using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character", order = 0)]
public class CharacterInfos : ScriptableObject
{
    // Rendering
    public string Name;
    [SpineSkin] public string skinProperty = "base";

    // Data
    [Header("Skin")]
    public string Eyebrows = "Neutral_Eyebrow";
    public string Chest = "Chest_Pear";
    public string Eyes = "Neutral_Eyes";
    public string Mouth = "Mouth Dumb";
    public string Hair = "Pear_Hairs";
    public string Head = "Pear_Head";
    public string Pants = "Pear_Neutral_Pants";
    public string Genitals = "Pear_Vulve";

    public EFruit Fruit;
    public ECloth Cloth;
    public EGenital Genital;
    public EExpression Expression;
    public EHat Hat;
    public EFacialHair FacialHair;
    public EVoice Voice;
    public S_Relation_Params[] Relations;
}