using UnityEngine;
using Spine.Unity;

[CreateAssetMenu(fileName = "PhraseList", menuName = "Data/Phrase", order = 0)]
public class PhraseArrays : ScriptableObject
{
    //Generique phrase
    public string[] PhraseBonjour;
    public string[] PhrasePanic;

    //Quiche phrase
    public string[] PhraseJumeaux;

    //Quiche phrase
    public string[] PhraseDualipoire;

    //Quiche phrase
    public string[] PhrasePote;

    //Quiche phrase
    public string[] PhraseQuiche;
}
