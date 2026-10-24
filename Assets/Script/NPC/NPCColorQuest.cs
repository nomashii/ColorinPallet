using UnityEngine;

public class NPCColorQuest : MonoBehaviour
{
    [Header("対象オブジェクト")]
    [SerializeField] private PaintableObject targetObject;

    [Header("正解の色")]
    [SerializeField] private ColorType targetColor;

    [SerializeField] private NPCQuestController questController;

    private bool completed;

    private void Update()
    {
        if (completed)
            return;

        if (targetObject == null)
            return;

        if (targetObject.GetPaintedColor() == targetColor)
        {
            completed = true;

            questController.CompleteQuest();

            Debug.Log("色クエスト達成！");
        }
    }
}