using System.Collections;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{


    [Header("表示UI")]
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject[] titles;
    [SerializeField] private GameObject[] comments;
    [SerializeField] private GameObject[] letters;
    [SerializeField] private ResultManager resultManager;
    [SerializeField] private ResultCalculator resultCalculator;
    [SerializeField] private TMP_Text rewardText;

    public void RefreshUI()
    {
        int star =
        resultCalculator.GetStarCount();

        Debug.Log("星の数 = " + star);
        Debug.Log("Stars配列 = " + stars.Length);
        Debug.Log("Titles配列 = " + titles.Length);
        Debug.Log("Comments配列 = " + comments.Length);
        Debug.Log("Letters配列 = " + letters.Length);



        foreach (GameObject obj in stars)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in titles)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in comments)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in letters)
        {
            obj.SetActive(false);
        }
        StartCoroutine(ShowStars(star));
        StartCoroutine(ShowLetters(star));
        StartCoroutine(ShowTitle(star));

        Debug.Log(resultManager);

        rewardText.text =
            resultCalculator.GetTotalReward() + " G";

    }
    private IEnumerator ShowStars(int star)
    {
        for (int i = 0; i < star; i++)
        {
            stars[i].SetActive(true);

            PopAppear pop = stars[i].GetComponent<PopAppear>();

            if (pop != null)
            {
                pop.Play();
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
    private IEnumerator ShowLetters(int star)
    {
        // 星が出たあと少し待つ
        yield return new WaitForSeconds(0.9f);

        for (int i = 0; i < star; i++)
        {
            letters[i].SetActive(true);

            PopAppear pop = letters[i].GetComponent<PopAppear>();

            if (pop != null)
            {
                pop.Play();
            }

            // ←これを追加！！
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator ShowTitle(int star)
    {
        // 手紙が全部出終わるまで待つ
        yield return new WaitForSeconds(
            0.5f + (star * 0.3f) + 0.3f
        );

        if (star <= 0)
        {
            yield break;
        }

        int index = star - 1;

        if (index >= titles.Length)
        {
            Debug.LogError(
                $"Titles配列が不足しています。" +
                $"星の数：{star} / 配列数：{titles.Length}"
            );

            yield break;
        }

        titles[index].SetActive(true);

        PopAppear pop =
            titles[index].GetComponent<PopAppear>();

        if (pop != null)
        {
            pop.Play();
        }
    }
}