using UnityEngine;
using UnityEngine.UI;

public class CanvasScaler : MonoBehaviour
{
    public Canvas targetCanvas;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float scaleSpeed = 0.1f;

    private RectTransform canvasRect;
    private Vector2 prevMousePos;
    private float currentScale = 1f;

    void Start()
    {
        if (targetCanvas == null)
        {
            targetCanvas = GetComponent<Canvas>();
        }
        canvasRect = targetCanvas.GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, targetCanvas.worldCamera, out mousePos);

        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            // Calculate the scale change
            float newScale = Mathf.Clamp(currentScale + scrollDelta * scaleSpeed, minScale, maxScale);
            float scaleFactor = newScale / currentScale;

            // Calculate the pivot point in normalized coordinates
            Vector2 pivotPoint = new Vector2(
                (mousePos.x / canvasRect.rect.width) + 0.5f,
                (mousePos.y / canvasRect.rect.height) + 0.5f
            );

            // Apply the scale
            canvasRect.localScale = Vector3.one * newScale;

            // Adjust the position to keep the mouse over the same point
            Vector2 scaledMousePos = mousePos * scaleFactor;
            Vector2 offset = scaledMousePos - mousePos;
            canvasRect.anchoredPosition -= offset;

            currentScale = newScale;
        }

        // Pan the canvas when middle mouse button is held down
        if (Input.GetMouseButton(2)) // 2 is middle mouse button
        {
            Vector2 mouseDelta = (Vector2)Input.mousePosition - prevMousePos;
            canvasRect.anchoredPosition += mouseDelta;
        }

        prevMousePos = Input.mousePosition;
    }
}