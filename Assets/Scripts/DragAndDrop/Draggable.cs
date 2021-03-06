using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Vector3 m_dragOffset;
    private Camera m_camera;
    private Agent m_agent;

    void Awake()
    {
        m_camera = Camera.main;
        m_agent = GetComponent<Agent>();
    }

    void OnMouseDown()
    {
        m_dragOffset = transform.position - GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePosition() + m_dragOffset;
        DragManager.Get().OnStartDrag(this);
    }

    private void OnMouseUp()
    {
        m_agent.SetState(AgentState.IDLE);
        DragManager.Get().OnDrop(this);
    }

    Vector3 GetMousePosition()
    {
        var mousePosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}