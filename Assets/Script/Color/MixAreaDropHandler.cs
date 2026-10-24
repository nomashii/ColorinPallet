using UnityEngine;
using UnityEngine.EventSystems;

public class MixAreaDropHandler :
    MonoBehaviour,
    IDropHandler
{
    [SerializeField] private ColorMixManager colorMixManager;

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDropColor drag =
            eventData.pointerDrag.GetComponent<DragAndDropColor>();

        if (drag == null)
            return;

        Debug.Log("Drop成功：" + drag.GetColorType());

        colorMixManager.PutColor(drag.GetColorType());
    }
}