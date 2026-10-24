using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    private TMP_Text text;


    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
}