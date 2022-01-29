using System;
using System.Linq;

using UnityEngine;
using UnityEngine.Assertions;

public class DragManager : MonoBehaviour
{
    public Action<Agent> OnStartDragging;

    private DropZone m_currentDropZone = null;
    private Draggable m_currentDraggable;

    // Singleton
    public static DragManager Get()
    {
        if (!m_instance)
        {
            m_instance = FindObjectOfType<DragManager>();
        }
        Assert.IsNotNull(m_instance);
        return m_instance;
    }
    static DragManager m_instance;

    public void OnStartDrag(Draggable p_draggable)
    {
        if (m_currentDraggable == p_draggable)
            return;

        m_currentDraggable = p_draggable;
        OnStartDragging?.Invoke(m_currentDraggable.GetComponent<Agent>());
    }

    public void OnDrop(Draggable p_draggable)
    {
        if (p_draggable != m_currentDraggable ||
            m_currentDropZone == null)
            return;

        m_currentDraggable.GetComponent<Agent>().Kill();
        m_currentDraggable = null;
    }

    private void Update()
    {
        // Unselect current drop zone
        if (m_currentDropZone != null)
        {
            m_currentDropZone.Unselect();
            m_currentDropZone = null;
        }

        if (m_currentDraggable == null)
            return;

        // Check for drop zones under mouse
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics2D.RaycastAll(ray.origin, Vector2.zero);
        if (hits.Length > 0)
        {
            var hit = hits.FirstOrDefault(h => h.collider.TryGetComponent<DropZone>(out _));
            if (hit == default(RaycastHit2D))
                return;

            m_currentDropZone = hit.collider.transform.GetComponent<DropZone>();
            m_currentDropZone.Select();
        }
    }
}