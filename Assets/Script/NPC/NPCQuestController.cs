using UnityEngine;

public class NPCQuestController : MonoBehaviour
{
    [SerializeField] private GameObject questBubble;
[SerializeField] private GameObject happyBubble;

[Header("報酬")]
[SerializeField] private int questMoneyReward = 20;

private MoneyManager moneyManager;
private QuestManager questManager;
private NPCController npcController;

private bool isCompleted;

    private void Start()
    {
        moneyManager = FindFirstObjectByType<MoneyManager>();
        questManager =FindFirstObjectByType<QuestManager>();
        

        questBubble.SetActive(true);
        happyBubble.SetActive(false);

        Debug.Log("NPCQuestController起動");
        npcController =GetComponent<NPCController>();
    }

    public void CompleteQuest()
{
    if (isCompleted)
        return;

    isCompleted = true;

    if (moneyManager != null)
    {
        moneyManager.AddMoney(questMoneyReward);
    }
    if (questManager != null)
{
    questManager.CompleteQuest();
}

    questBubble.SetActive(false);
    happyBubble.SetActive(true);
    if (npcController != null)
    {
    npcController.SetHappy();
    }

    Debug.Log("NPC報酬 +" + questMoneyReward + "G");
    Debug.Log("NPCクエスト達成！");
}
public void FailQuest()
{
    if (isCompleted)
        return;

    if (npcController != null)
    {
        npcController.SetWrong();
    }
    AudioManager.Instance.PlayMissSE();
    questBubble.SetActive(false);
    happyBubble.SetActive(false);

    Debug.Log("クエスト失敗");
}

    

    private void Update()
    {
    

    if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.Log("P押された");
        CompleteQuest();
    }
    }
}