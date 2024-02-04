using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    private VisualElement dragSource;
    private Vector2 offset = new Vector2(0,0);
    private bool dragging = false;

    private void Start()
    {
        // Get the drag source element
        dragSource = GetComponent<UIDocument>().rootVisualElement.Q("DragSource");

        // Register drag and drop events
        dragSource.RegisterCallback<MouseDownEvent>(OnMouseDown);
        dragSource.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        dragSource.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent evt)
{
    if (evt.button == (int)MouseButton.LeftMouse)
    {
        // Convert mouse position from global to local coordinates relative to the drag source
        Vector2 localMousePosition = dragSource.WorldToLocal(evt.mousePosition);

        // Calculate the offset between the local mouse position and the position of the drag source
        offset = localMousePosition - dragSource.layout.center;

        // Mark dragging as true
        dragging = true;

        // Prevent event propagation and default behavior
        evt.StopPropagation();
        evt.PreventDefault();
    }
}

private void OnMouseMove(MouseMoveEvent evt)
{
    if (!dragging) return;

    // Convert mouse position from global to local coordinates relative to the drag source
    Vector2 localMousePosition = dragSource.WorldToLocal(evt.mousePosition);

    // Calculate the new position of the drag source based on the offset
    Vector2 newPosition = localMousePosition - offset;

    // Update the position of the drag source
    dragSource.style.left = newPosition.x;
    dragSource.style.top = newPosition.y;

    // Prevent event propagation and default behavior
    evt.StopPropagation();
    evt.PreventDefault();
}

private void OnMouseUp(MouseUpEvent evt)
{
    // Reset the offset and mark dragging as false
    offset = Vector2.zero;
    dragging = false;

    // Prevent event propagation and default behavior
    evt.StopPropagation();
    evt.PreventDefault();
}
}