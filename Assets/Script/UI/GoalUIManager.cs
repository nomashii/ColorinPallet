using TMPro;
using UnityEngine;

public class GoalUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalText;
    [SerializeField] private StageGoalManager goalManager;

    void Start()
{
    goalManager.OnGoalChanged += UpdateGoalUI;

    UpdateGoalUI();
}
private void OnDestroy()
{
    if (goalManager != null)
    {
        goalManager.OnGoalChanged -= UpdateGoalUI;
    }
}
private void UpdateGoalUI()
{
    
    goalText.text =
    goalManager.GetCompletedGoals().ToString("00")
    + " / " +
    goalManager.GetTotalGoals().ToString("00");
}
}