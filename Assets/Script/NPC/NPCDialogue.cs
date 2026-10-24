using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [TextArea(2,5)]
    [SerializeField] private string beforeMessage;

    [TextArea(2,5)]
    [SerializeField] private string afterMessage;

    public string GetBeforeMessage()
    {
        return beforeMessage;
    }

    public string GetAfterMessage()
    {
        return afterMessage;
    }
}