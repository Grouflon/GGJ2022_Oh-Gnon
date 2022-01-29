using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character", order = 0)]
public class CharacterInfos : ScriptableObject
{
    // Rendering
    public string Name;
    [SpineSkin] public string skinProperty = "base";

    // Data
    public EFruit Fruit;
    public ECloth Cloth;
    public EGenital Genital;
    public EExpression Expression;
    public EHat Hat;
    public EFacialHair FacialHair;
    public EVoice Voice;
    public S_Relation_Params[] Relations;
}