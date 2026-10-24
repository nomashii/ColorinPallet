using UnityEngine;

public class SceneLoadButton : MonoBehaviour
{
    public void LoadTitle()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instanceが存在しません。");
            return;
        }

        SceneLoader.Instance.LoadTitle();
    }

    public void LoadStageSelect()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instanceが存在しません。");
            return;
        }

        SceneLoader.Instance.LoadStageSelect();
    }

    public void LoadStage1()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instanceが存在しません。");
            return;
        }

        SceneLoader.Instance.LoadStage1();
    }

    public void LoadResult()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instanceが存在しません。");
            return;
        }

        SceneLoader.Instance.LoadResult();
    }

    public void LoadNextStage()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instanceが存在しません。");
            return;
        }

        SceneLoader.Instance.LoadNextStage();
    }
}