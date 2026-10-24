using UnityEngine;

public class StageGoalManager : MonoBehaviour
{
    public System.Action OnGoalChanged;

    [SerializeField] private int totalGoals = 3;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private ResultManager resultManager;

    private int completedGoals = 0;

    void Start()
    {
        totalGoals = FindObjectsByType<PaintableObject>(
            FindObjectsSortMode.None
        ).Length;

        OnGoalChanged?.Invoke();
    }

    public void CompleteGoal()
    {
        completedGoals++;

        Debug.Log(
            "達成数 : " +
            completedGoals +
            "/" +
            totalGoals);

        OnGoalChanged?.Invoke();

        
        if (completedGoals >= totalGoals)
{
    resultManager.ShowClearPanel();
}
    }

    public int GetCompletedGoals()
    {
        return completedGoals;
    }

    public int GetTotalGoals()
    {
        return totalGoals;
    }
}