using UnityEngine;

using Spine;
using Spine.Unity;

public class Character : MonoBehaviour
{
    public void SetCharacter(CharacterInfos p_characterInfos)
    {
        var skeletonGraphic = GetComponent<SkeletonGraphic>();
        var skeleton = skeletonGraphic.Skeleton;
        skeleton.SetSkin(p_characterInfos.skinProperty);
    }
}