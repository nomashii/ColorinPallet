using UnityEngine;
using System.Collections.Generic; // ✨ HashSetを使うためにこれが必要です！

public class ResultCalculator : MonoBehaviour
{
    private QuestManager questManager;
    private StageGoalManager goalManager;

    private void Awake()
    {
        questManager = FindFirstObjectByType<QuestManager>();
        goalManager = FindFirstObjectByType<StageGoalManager>();
    }

    public int GetClearBonus()
    {
        if (goalManager.GetCompletedGoals() >= goalManager.GetTotalGoals())
        {
            return 50;
        }

        return 0;
    }

    public int GetStarCount()
    {
        float rate = (float)questManager.GetCompletedQuest() / questManager.GetTotalQuest();

        if (rate >= 1f) return 3;
        if (rate >= 0.6f) return 2;
        if (rate >= 0.3f) return 1;

        return 0;
    }

    public int GetResidentBonus()
    {
        float rate = (float)questManager.GetCompletedQuest() / questManager.GetTotalQuest();

        if (rate >= 1f) return 120;
        if (rate >= 0.8f) return 90;
        if (rate >= 0.6f) return 60;
        if (rate >= 0.3f) return 30;

        return 0;
    }

    public int GetColorBonus()
    {
        PaintableObject[] objects = FindObjectsByType<PaintableObject>(FindObjectsSortMode.None);

        // ✨ 塗られた色を重複なく記憶するための箱（usedColors）を作ります
        HashSet<ColorType> usedColors = new HashSet<ColorType>();


        foreach (PaintableObject obj in objects)
{
    ColorType color = obj.GetPaintedColor();

    if (color != ColorType.None)
    {
        usedColors.Add(color);
    }
}

        int colorCount = usedColors.Count;

        switch (colorCount)
        {
            case 1: return 0;
            case 2: return 20;
            case 3: return 40;
            case 4: return 60;
            case 5: return 80;
            case 6: return 100;
            default:
                if (colorCount >= 7)
                    return 120;
                return 0;
        }
    }
    public int GetTotalReward()
{
    return GetClearBonus()
         + GetColorBonus()
         + GetResidentBonus();
}
}