using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int totalQuest;
    private int completedQuest;
    public int GetCompletedQuest()
{
    return completedQuest;
}

public int GetTotalQuest()
{
    return totalQuest;
}

public void SetTotalQuest(int total)
{
    totalQuest = total;
}

public void AddTotalQuest()
{
    totalQuest++;
}

    void Start()
{
    totalQuest =
        FindObjectsByType<NPCQuestController>(
            FindObjectsSortMode.None).Length;

            Debug.Log("お願い総数：" + totalQuest);

}
public void CompleteQuest()
{
    completedQuest++;

    Debug.Log(
        "お願い達成：" +
        completedQuest +
        "/" +
        totalQuest);
}
}
