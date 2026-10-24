using UnityEngine;
using UnityEngine.EventSystems;

public class PaintController : MonoBehaviour
{
    [SerializeField] private PaletteManager paletteManager;

    void Update()
    {
        if (paletteManager != null && paletteManager.IsPaletteOpen)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current != null &&
                    EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                TryPaintAtScreenPosition(touch.position);
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            TryPaintAtScreenPosition(Input.mousePosition);
        }
    }

    private void TryPaintAtScreenPosition(Vector2 screenPosition)
{
    Vector2 worldPoint =
        Camera.main.ScreenToWorldPoint(screenPosition);

    RaycastHit2D[] hits =
        Physics2D.RaycastAll(worldPoint, Vector2.zero);

    foreach (RaycastHit2D hit in hits)
    {
        PaintableObject paintableObject =
            hit.collider.GetComponent<PaintableObject>();

        if (paintableObject != null)
        {
            paintableObject.TryPaint();
            return;
        }
    }
}
}