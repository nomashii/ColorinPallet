using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject sadSprite;
    [SerializeField] private GameObject happySprite;
    [SerializeField] private GameObject wrongSprite;
    [SerializeField] private GameObject changeEffectPrefab;

    private void Start()
    {
        Debug.Log("NPCController起動");

        if (bubble != null)
            bubble.SetActive(false);
        if (sadSprite != null)
        sadSprite.SetActive(true);
        if (happySprite != null)
        happySprite.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bubble ON");

            bubble.SetActive(true);

            Debug.Log("現在状態：" + bubble.activeSelf);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXIT");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bubble OFF");

            bubble.SetActive(false);
        }
    }
public void SetHappy()
{
    StartCoroutine(HappyAnimation());
}
public void SetWrong()
{
    Debug.Log("SetWrong呼ばれた");
    Debug.Log(wrongSprite);
    
    if (sadSprite != null)
        sadSprite.SetActive(false);

    if (happySprite != null)
        happySprite.SetActive(false);

    if (wrongSprite != null)
        wrongSprite.SetActive(true);
}
private IEnumerator HappyAnimation()
{
    if (changeEffectPrefab != null)
    {
        Instantiate(
            changeEffectPrefab,
            transform.position + Vector3.up * 0.5f,
            Quaternion.identity);
    }

    yield return new WaitForSeconds(0.08f);

    if (sadSprite != null)
        sadSprite.SetActive(false);

    if (wrongSprite != null)
        wrongSprite.SetActive(false);

    if (happySprite != null)
        happySprite.SetActive(true);
}
}