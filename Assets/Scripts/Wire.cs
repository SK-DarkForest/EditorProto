using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    private VisualElement dragSource;
    private Vector2 offset = Vector2.zero;
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
            // Calculate the offset between the mouse position and the drag source position
            Vector2 mousePositionRelativeToElement = evt.localMousePosition;
            offset = dragSource.WorldToLocal(mousePositionRelativeToElement) - dragSource.layout.center;

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

        // Update the position of the drag source based on the mouse position and offset
        Vector2 localMousePosition = evt.mousePosition;
        Vector2 newPosition = dragSource.WorldToLocal(localMousePosition) - offset;
        dragSource.style.left = newPosition.x;
        dragSource.style.top = newPosition.y;

        // Prevent event propagation and default behavior
        evt.StopPropagation();
        evt.PreventDefault();
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        // Mark dragging as false
        dragging = false;

        // Prevent event propagation and default behavior
        evt.StopPropagation();
        evt.PreventDefault();
    }
}