using UnityEngine;
using System.Collections;

public class PlayerFormController : MonoBehaviour
{
    [Header("7色の立ち絵イラスト（PNGアセットをここに割り当て）")]

    [SerializeField] private RuntimeAnimatorController whiteAnimator;
[SerializeField] private RuntimeAnimatorController redAnimator;
[SerializeField] private RuntimeAnimatorController blueAnimator;
[SerializeField] private RuntimeAnimatorController yellowAnimator;
[SerializeField] private RuntimeAnimatorController purpleAnimator;
[SerializeField] private RuntimeAnimatorController greenAnimator;
[SerializeField] private RuntimeAnimatorController orangeAnimator;
[SerializeField]private ParticleSystem paintEffect;
[SerializeField]private GameObject changeEffectPrefab;

private void SetPaintEffectColor(Color color)
{
    if (paintEffect == null)
        return;

    var main = paintEffect.main;
    main.startColor = color;
}

    private SpriteRenderer spriteRenderer;
    private Animator animator;
private ColorType currentColor = ColorType.White;
private Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    /// <summary>
    /// パレットから「この色に変身して！」と呼び出される窓口（何度でも上書き可能）
    /// </summary>
    public void ChangeForm(ColorType colorType)
{
    Debug.Log("ChangeForm開始：" + colorType);
    currentColor = colorType;
    Debug.Log("ChangeForm受信: " + colorType);
    if (spriteRenderer == null) return;

    spriteRenderer.color = Color.white;

    switch (colorType)
    {
        case ColorType.White:

    if (whiteAnimator != null)
        animator.runtimeAnimatorController = whiteAnimator;
        SetPaintEffectColor(Color.white);

    break;
        case ColorType.Red:

    if (redAnimator != null)
        animator.runtimeAnimatorController = redAnimator;
        SetPaintEffectColor(Color.red);

    break;

        case ColorType.Blue:
    if (blueAnimator != null)
        animator.runtimeAnimatorController = blueAnimator;
        SetPaintEffectColor(Color.blue);

    break;

        case ColorType.Yellow:

    if (yellowAnimator != null)
        animator.runtimeAnimatorController = yellowAnimator;
        SetPaintEffectColor(Color.yellow);

    break;

        case ColorType.Purple:

    if (purpleAnimator != null)
        animator.runtimeAnimatorController = purpleAnimator;
        SetPaintEffectColor(new Color(0.6f,0f,1f));

    break;

        case ColorType.Green:
    if (greenAnimator != null)
        animator.runtimeAnimatorController = greenAnimator;
        SetPaintEffectColor(Color.green);

    break;

        case ColorType.Orange:
    if (orangeAnimator != null)
        animator.runtimeAnimatorController = orangeAnimator;
        SetPaintEffectColor(new Color(1f,0.5f,0f));

    break;

        default:

    if (whiteAnimator != null)
        animator.runtimeAnimatorController = whiteAnimator;

    break;
    }
    GameObject effect = Instantiate(
    changeEffectPrefab,
    transform.position + Vector3.up * 0.4f,
    Quaternion.identity);

    ParticleSystem ps = effect.GetComponent<ParticleSystem>();

if (ps != null)
{
    var main = ps.main;

    switch (colorType)
    {
        case ColorType.Red:
            main.startColor = Color.red;
            break;

        case ColorType.Blue:
            main.startColor = Color.blue;
            break;

        case ColorType.Yellow:
            main.startColor = Color.yellow;
            break;

        case ColorType.Green:
            main.startColor = Color.green;
            break;

        case ColorType.Purple:
            main.startColor = new Color(0.6f, 0f, 1f);
            break;

        case ColorType.Orange:
            main.startColor = new Color(1f, 0.5f, 0f);
            break;

        default:
            main.startColor = Color.white;
            break;
    }
}

StopAllCoroutines();
StartCoroutine(ChangeEffectAnimation());

    Debug.Log("オバケちゃんが「" + colorType + "」の姿に変身しました！");
}
public void PlayPaintMotion(ColorType colorType)
{
    ChangeForm(colorType);
    AudioManager.Instance.PlayPaintSE();

    animator.SetTrigger("PaintTrigger");
}
public ColorType GetCurrentColor()
{
    return currentColor;
}
private IEnumerator ChangeEffectAnimation()
{
    transform.localScale = originalScale * 1.15f;

    yield return new WaitForSecondsRealtime(0.05f);

    transform.localScale = originalScale * 0.9f;

    yield return new WaitForSecondsRealtime(0.05f);

    transform.localScale = originalScale;
}
}

