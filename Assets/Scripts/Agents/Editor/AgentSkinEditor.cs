using UnityEngine;
using Spine.Unity;

using UnityEditor;

[CustomEditor(typeof(AgentSkin))]
class AgentSkinEditor : Editor
{
    AgentSkin agentSkin;

    void OnEnable()
    {
        agentSkin = target as AgentSkin;
        agentSkin.UpdateSkin(agentSkin.GetComponentInChildren<SkeletonAnimation>(true));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Apply Skin"))
        {
            agentSkin.UpdateSkin(agentSkin.GetComponentInChildren<SkeletonAnimation>(true));
        }
    }
}