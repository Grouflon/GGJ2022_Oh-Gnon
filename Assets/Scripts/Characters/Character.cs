using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character", order = 0)]
public class Character : ScriptableObject
{
    public string Name;
    public EFruit Fruit;
    public ECloth Cloth;
    public EGenital Genital;
    public EExpression Expression;
    public EHat Hat;
    public EFacialHair FacialHair;
    public EVoice Voice;
}