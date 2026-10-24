using UnityEngine;

public class StageSelectBGM : MonoBehaviour
{
    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayTitleBGM();
        }
    }
}