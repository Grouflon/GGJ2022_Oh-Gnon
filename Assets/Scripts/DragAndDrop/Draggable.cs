using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Vector3 m_dragOffset;
    private Camera m_camera;

    void Awake()
    {
        m_camera = Camera.main;
    }

    void OnMouseDown()
    {
        m_dragOffset = transform.position - GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePosition() + m_dragOffset;
    }

    Vector3 GetMousePosition()
    {
        var mousePosition = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}