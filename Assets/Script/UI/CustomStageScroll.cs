using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomStageScroll :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    [Header("ステージカード")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform content;

    [Header("スクロールバー")]
    [SerializeField] private Scrollbar scrollbar;

    [Header("スクロール設定")]
    [SerializeField] private float dragSensitivity = 1f;
    [SerializeField] private float deceleration = 8f;
    [SerializeField] private bool useInertia = true;

    [Header("右端の余白")]
    [SerializeField] private float rightEndPadding = 0f;

    private Vector2 previousPointerPosition;
    private float velocity;
    private bool isDragging;

    private float startContentX;
    private float endContentX;
    private float scrollDistance;

    private bool CanScroll
    {
        get
        {
            return scrollDistance > 0.01f;
        }
    }

    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            content
        );

        Canvas.ForceUpdateCanvases();

        // ステージ1-1が表示されている初期位置
        startContentX = content.anchoredPosition.x;

        RefreshScrollRange();

        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(
                OnScrollbarValueChanged
            );
        }
    }

    private void OnDestroy()
    {
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.RemoveListener(
                OnScrollbarValueChanged
            );
        }
    }

    private void Update()
    {
        if (!CanScroll)
        {
            velocity = 0f;

            Vector2 fixedPosition =
                content.anchoredPosition;

            fixedPosition.x = startContentX;

            content.anchoredPosition =
                fixedPosition;

            return;
        }

        if (isDragging || !useInertia)
        {
            return;
        }

        if (Mathf.Abs(velocity) < 0.01f)
        {
            velocity = 0f;
            return;
        }

        MoveContent(
            velocity * Time.unscaledDeltaTime
        );

        velocity = Mathf.Lerp(
            velocity,
            0f,
            deceleration * Time.unscaledDeltaTime
        );
    }

    public void OnBeginDrag(
        PointerEventData eventData
    )
    {
        if (!CanScroll)
        {
            velocity = 0f;
            return;
        }

        isDragging = true;
        velocity = 0f;

        previousPointerPosition =
            eventData.position;
    }

    public void OnDrag(
        PointerEventData eventData
    )
    {
        if (!CanScroll)
        {
            return;
        }

        Vector2 currentPointerPosition =
            eventData.position;

        float pointerDeltaX =
            currentPointerPosition.x
            - previousPointerPosition.x;

        float movement =
            pointerDeltaX * dragSensitivity;

        MoveContent(movement);

        velocity =
            movement / Mathf.Max(
                Time.unscaledDeltaTime,
                0.0001f
            );

        previousPointerPosition =
            currentPointerPosition;
    }

    public void OnEndDrag(
        PointerEventData eventData
    )
    {
        isDragging = false;
    }

    private void MoveContent(float movement)
    {
        if (!CanScroll)
        {
            return;
        }

        Vector2 position =
            content.anchoredPosition;

        position.x += movement;

        content.anchoredPosition =
            position;

        ClampContentPosition();
        UpdateScrollbarValue();
    }

    public void RefreshScrollRange()
    {
        Canvas.ForceUpdateCanvases();

        LayoutRebuilder.ForceRebuildLayoutImmediate(
            content
        );

        Canvas.ForceUpdateCanvases();

        /*
         * StageContentの幅ではなく、
         * 実際に配置されている子オブジェクトの
         * 一番右端を調べる
         */
        float rightmostCardEdge =
            GetRightmostCardEdgeInViewport();

        float viewportRightEdge =
            viewport.rect.xMax;

        /*
         * 最後のカードがViewportから
         * はみ出している分だけスクロール可能にする
         */
        scrollDistance = Mathf.Max(
            0f,
            rightmostCardEdge
            - viewportRightEdge
            + rightEndPadding
        );

        endContentX =
            startContentX - scrollDistance;

        if (!CanScroll)
        {
            Vector2 position =
                content.anchoredPosition;

            position.x = startContentX;

            content.anchoredPosition =
                position;
        }
        else
        {
            ClampContentPosition();
        }

        UpdateScrollbarSize();
        UpdateScrollbarValue();
    }

    private float GetRightmostCardEdgeInViewport()
    {
        bool foundCard = false;

        float rightmostEdge =
            viewport.rect.xMin;

        Vector3[] corners =
            new Vector3[4];

        for (int i = 0;
             i < content.childCount;
             i++)
        {
            RectTransform child =
                content.GetChild(i)
                as RectTransform;

            if (child == null ||
                !child.gameObject.activeInHierarchy)
            {
                continue;
            }

            child.GetWorldCorners(corners);

            for (int cornerIndex = 0;
                 cornerIndex < corners.Length;
                 cornerIndex++)
            {
                Vector3 viewportLocalPoint =
                    viewport.InverseTransformPoint(
                        corners[cornerIndex]
                    );

                rightmostEdge = Mathf.Max(
                    rightmostEdge,
                    viewportLocalPoint.x
                );
            }

            foundCard = true;
        }

        if (!foundCard)
        {
            return viewport.rect.xMin;
        }

        return rightmostEdge;
    }

    private void ClampContentPosition()
    {
        Vector2 position =
            content.anchoredPosition;

        if (!CanScroll)
        {
            position.x = startContentX;
        }
        else
        {
            position.x = Mathf.Clamp(
                position.x,
                endContentX,
                startContentX
            );
        }

        content.anchoredPosition =
            position;
    }

    private void OnScrollbarValueChanged(
        float value
    )
    {
        velocity = 0f;

        if (!CanScroll)
        {
            Vector2 fixedPosition =
                content.anchoredPosition;

            fixedPosition.x =
                startContentX;

            content.anchoredPosition =
                fixedPosition;

            scrollbar.SetValueWithoutNotify(0f);

            return;
        }

        Vector2 position =
            content.anchoredPosition;

        position.x = Mathf.Lerp(
            startContentX,
            endContentX,
            value
        );

        content.anchoredPosition =
            position;

        ClampContentPosition();
    }

    private void UpdateScrollbarSize()
    {
        if (scrollbar == null)
        {
            return;
        }

        if (!CanScroll)
        {
            /*
             * 全ステージが画面内に収まっている場合、
             * Handleを全面表示にして操作不能にする
             */
            scrollbar.size = 1f;
            scrollbar.interactable = false;
            scrollbar.SetValueWithoutNotify(0f);

            return;
        }

        scrollbar.interactable = true;

        float viewportWidth =
            viewport.rect.width;

        float totalVisibleWidth =
            viewportWidth + scrollDistance;

        scrollbar.size =
            Mathf.Clamp01(
                viewportWidth
                / totalVisibleWidth
            );
    }

    private void UpdateScrollbarValue()
    {
        if (scrollbar == null)
        {
            return;
        }

        if (!CanScroll)
        {
            scrollbar.SetValueWithoutNotify(0f);
            return;
        }

        float scrollRate =
            (startContentX
            - content.anchoredPosition.x)
            / scrollDistance;

        scrollRate =
            Mathf.Clamp01(scrollRate);

        scrollbar.SetValueWithoutNotify(
            scrollRate
        );
    }
}