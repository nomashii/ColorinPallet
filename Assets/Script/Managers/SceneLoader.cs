using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private bool isLoading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode
    )
    {
        isLoading = false;

        Debug.Log(
            $"�V�[���ǂݍ��݊����F{scene.name} / isLoading��false�ɂ��܂���"
        );
    }

    public void LoadScene(string sceneName)
    {
        if (!CanStartLoading(sceneName))
        {
            return;
        }

        isLoading = true;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithSE(string sceneName)
    {
        if (!CanStartLoading(sceneName))
        {
            return;
        }

        isLoading = true;

        StartCoroutine(
            LoadSceneCoroutine(sceneName)
        );
    }

    private bool CanStartLoading(string sceneName)
    {
        if (isLoading)
        {
            Debug.LogWarning(
                $"���݃V�[���ǂݍ��ݒ��ł��B{sceneName}�ւ̑J�ڂ𒆎~���܂����B"
            );

            return false;
        }

        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError(
                "�ǂݍ��ރV�[�������ݒ肳��Ă��܂���B"
            );

            return false;
        }

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError(
                $"�V�[���u{sceneName}�v��Build Settings��Scene List�ɓo�^����Ă��܂���B"
            );

            return false;
        }

        return true;
    }

    private IEnumerator LoadSceneCoroutine(
        string sceneName
    )
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayImportantTapSE();

            float waitTime =
                AudioManager.Instance.GetImportantTapLength();

            yield return new WaitForSecondsRealtime(
                waitTime
            );
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadTitle()
    {
        LoadSceneWithSE("Title");
    }

    public void LoadStageSelect()
    {
        LoadSceneWithSE("StageSelect");
    }

    public void LoadStage1()
    {
        LoadSceneWithSE("Stage1-1");
    }

    public void LoadResult()
    {
        LoadSceneWithSE("Result");
    }

    public void LoadNextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Stage1-1":
                LoadScene("Stage1-2");
                break;

            case "Stage1-2":
                LoadScene("Stage1-3");
                break;

            default:
                Debug.LogWarning("���̃X�e�[�W�����݂��܂���B");
                break;
        }
    }

}