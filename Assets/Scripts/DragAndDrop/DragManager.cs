using System.Linq;

using UnityEngine;

public class DragManager : MonoBehaviour
{
    private DropZone m_currentDropZone = null;
    private Draggable m_currentDraggable;

    public void OnStartDrag(Draggable p_draggable)
    {
        m_currentDraggable = p_draggable;
    }

    public void OnDrop(Draggable p_draggable)
    {
        if (p_draggable != m_currentDraggable ||
            m_currentDropZone == null)
            return;

        m_currentDraggable.GetComponent<Agent>().Kill();
    }

    private void Update()
    {
        // Unselect current drop zone
        if (m_currentDropZone != null)
        {
            m_currentDropZone.Unselect();
            m_currentDropZone = null;
        }

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