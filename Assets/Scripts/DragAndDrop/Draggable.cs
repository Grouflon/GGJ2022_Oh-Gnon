using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Vector3 m_dragOffset;
    private Camera m_camera;
    private DragManager m_dragManager;
    private Agent m_agent;

    void Awake()
    {
        m_camera = Camera.main;
        m_dragManager = FindObjectOfType<DragManager>();
        m_agent = GetComponent<Agent>();
    }

    void OnMouseDown()
    {
        m_dragOffset = transform.position - GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePosition() + m_dragOffset;
        m_dragManager.OnStartDrag(this);

        m_agent.SetState(AgentState.DRAGGED);
    }

    private void OnMouseUp()
    {
        m_dragManager.OnDrop(this);
    }

    Vector3 GetMousePosition()
    {
        var mousePosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}