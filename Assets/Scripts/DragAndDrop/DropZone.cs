using System;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    [SerializeField] private GameObject m_explosionFX;
    [SerializeField] private Color m_defaultColor;
    [SerializeField] private Color m_overColor;

    private SpriteRenderer m_sr;
    private bool m_mouseOver = false;

    private void Awake()
    {
        m_sr = GetComponent<SpriteRenderer>();
    }

    public void Unselect()
    {
        m_sr.color = m_defaultColor;
    }

    public void Select()
    {
        m_sr.color = m_overColor;
    }

    public void Use()
    {
        Instantiate(m_explosionFX, transform.position, transform.rotation);
    }
}