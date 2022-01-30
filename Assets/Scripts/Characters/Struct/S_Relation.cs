[System.Serializable]
public enum E_relationType
{
    Jumeaux,
    FanDe,
    Pote,
    Amoureux,
    Epoux,
    Deteste,
    Business,
    Obeis
}

[System.Serializable]
public struct S_Relation_Params
{
    public E_relationType RelationType;
    public int[] ID_Targets;
    public string[] Unique_Phrases;
}
