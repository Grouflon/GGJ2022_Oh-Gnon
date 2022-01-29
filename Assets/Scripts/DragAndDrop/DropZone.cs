using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DropZone : MonoBehaviour
{
    [SerializeField] private Color m_defaultColor;
    [SerializeField] private Color m_overColor;

    private SpriteRenderer m_sr;
    private bool m_mouseOver = false;

    private void Awake()
    {
        m_sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check colliders under mouse
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.RaycastAll(ray.origin, Vector2.zero);
        m_mouseOver = false;
        if (hits.Length > 0)
        {
            m_mouseOver = hits.FirstOrDefault(h => h.collider.transform == transform) != default(RaycastHit2D);
        }

        // Color
        if (m_mouseOver)
        {
            m_sr.color = m_overColor;
        }
        else
        {
            m_sr.color = m_defaultColor;
        }
    }
}