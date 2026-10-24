using UnityEngine;
using UnityEngine.EventSystems;


public class MixArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private ColorMixManager colorMixManager;


    public void OnDrop(PointerEventData eventData)
{
    DragAndDropColor draggedColor =
        eventData.pointerDrag.GetComponent<DragAndDropColor>();


    if (draggedColor == null)
    {
        return;
    }


    ColorType color = draggedColor.GetColorType();


    if (colorMixManager != null)
    {
        colorMixManager.PutColor(color);
    }


    Debug.Log("MixAreaに " + color + " を置きました");
}
}
