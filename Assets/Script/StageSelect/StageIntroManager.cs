using System.Collections;
using UnityEngine;

public class StageIntroManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup backgroundGroup;
    [SerializeField] private CanvasGroup stageImageGroup;

    [SerializeField] private PlayerController playerController;

    private void Start()
    {

        // プレイヤー操作禁止
        playerController.SetCanMove(false);

        // オープニング開始
        StartCoroutine(StageIntro());
    }

    private IEnumerator StageIntro()
    {
        AudioManager.Instance.PlayGameStartSE();

        backgroundGroup.alpha = 0;
        stageImageGroup.alpha = 0;

        // =====================
        // 背景フェードイン
        // =====================
        while (backgroundGroup.alpha < 1)
        {
            backgroundGroup.alpha += Time.deltaTime * 2f;
            yield return null;
        }

        backgroundGroup.alpha = 1;

        yield return new WaitForSeconds(0.2f);

        // =====================
        // タイトルフェードイン
        // =====================
        while (stageImageGroup.alpha < 1)
        {
            stageImageGroup.alpha += Time.deltaTime * 2f;
            yield return null;
        }

        stageImageGroup.alpha = 1;

        // 表示
        yield return new WaitForSeconds(2.2f);

        // =====================
        // タイトルフェードアウト
        // =====================
        while (stageImageGroup.alpha > 0)
        {
            stageImageGroup.alpha -= Time.deltaTime * 2f;
            yield return null;
        }

        stageImageGroup.alpha = 0;

        // =====================
        // 背景フェードアウト
        // =====================
        while (backgroundGroup.alpha > 0)
        {
            backgroundGroup.alpha -= Time.deltaTime * 2f;
            yield return null;
        }

        backgroundGroup.alpha = 0;;

        // StageBGM開始
        AudioManager.Instance.PlayStageBGM();

        // プレイヤー操作開始
        playerController.SetCanMove(true);

        // オープニング終了
        GameStartManager.Instance.EndOpening();
    }
}