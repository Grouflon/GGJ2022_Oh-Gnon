using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character", order = 0)]
public class CharacterInfos : ScriptableObject
{
    // Rendering
    public string Name;

    public S_Relation_Params[] Relations;
}