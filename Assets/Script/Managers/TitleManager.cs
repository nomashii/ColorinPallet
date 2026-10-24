using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]private SceneLoader sceneLoader;
    bool canTap = true;
    void Start()
{
    AudioManager.Instance.PlayTitleBGM();
}

    void Update()
{
    if (!canTap)
        return;

    if (Input.GetMouseButtonDown(0))
    {
        canTap = false;

        AudioManager.Instance.PlayImportantTapSE();

        SceneLoader.Instance.LoadStageSelect();
    }
}
}