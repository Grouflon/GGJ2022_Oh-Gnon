using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentParameters", menuName = "ScriptableObjects/AgentParameters", order = 1)]
public class AgentParameters : ScriptableObject
{
    public float Speed = 5;
    public float MinWalkDistance = 3;
    public float MaxWalkDistance = 3;
    public float MinTimeBetweenWalks = 1;
    public float MaxTimeBetweenWalks = 5;

}

