using System;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

using Spine.Unity;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    public EFatality Fatality;

    [SerializeField] private bool m_playExplosion = false;
    [SerializeField] private GameObject m_explosionFX;

    [Header("Animations")]
    [SerializeField] private string m_idleAnimation;
    [SerializeField] private string m_inUseAnimation;

    SkeletonAnimation skeletonAnimation;

    private bool m_mouseOver = false;

    private void Start()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>(true);
        Unselect();
    }

    public void Unselect()
    {
        skeletonAnimation.AnimationName = m_idleAnimation;
    }

    public void Select()
    {
        skeletonAnimation.AnimationName = m_inUseAnimation;
    }

    public void Use()
    {
        if (m_playExplosion)
        {
            Invoke("Explosion", 0.5f);
        }
    }

    private void Explosion()
    {
        Instantiate(m_explosionFX, transform.position, transform.rotation);
    }
}