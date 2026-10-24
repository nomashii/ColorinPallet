using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioSource walkSource;

    [Header("BGM")]
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip stageBGM;

    [Header("Tap SE")]
    [SerializeField] private AudioClip tapSE;
    [SerializeField] private AudioClip importantTapSE;
    [SerializeField] private AudioClip popupSE;

    [Header("Action SE")]
    [SerializeField] private AudioClip paintSE;
    [SerializeField] private AudioClip colorChangeSE;
    [SerializeField] private AudioClip walkSE;

    [Header("Event SE")]
    [SerializeField] private AudioClip gameStartSE;
    [SerializeField] private AudioClip clearSE;
    [SerializeField] private AudioClip moneySE;
    [SerializeField] private AudioClip missSE;
    [SerializeField] private AudioClip shoppingSE;
    [SerializeField] private AudioClip failedPaintSE;
    private void Awake()
    {
        Debug.Log("AudioManager生成 : " + gameObject.name);
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

//======================
// BGM
//======================

public bool IsTitleBGMPlaying()
{
    return bgmSource.isPlaying &&
           bgmSource.clip == titleBGM;
}
public void PlayTitleBGM()
{
    if (titleBGM == null) return;

    if (bgmSource.clip == titleBGM && bgmSource.isPlaying)
        return;

    bgmSource.Stop();
    bgmSource.clip = titleBGM;
    bgmSource.loop = true;
    bgmSource.Play();
}

public void PlayStageBGM()
{
    if (stageBGM == null) return;

    if (bgmSource.clip == stageBGM && bgmSource.isPlaying)
        return;

    bgmSource.Stop();
    bgmSource.clip = stageBGM;
    bgmSource.loop = true;
    bgmSource.Play();
}
public float GetGameStartSELength()
{
    if (gameStartSE == null)
        return 0f;

    return gameStartSE.length;
}

public void StopBGM()
{
    bgmSource.Stop();
}
//======================
// Tap SE
//======================

public void PlayTapSE()
{
    if (tapSE != null)
        seSource.PlayOneShot(tapSE);
}


public void PlayImportantTapSE()
{
    if (importantTapSE != null)
        seSource.PlayOneShot(importantTapSE);
}
public float GetImportantTapLength()
{
    if (importantTapSE == null)
        return 0f;

    return importantTapSE.length;
}

public void PlayPopupSE()
{
    if (popupSE != null)
        seSource.PlayOneShot(popupSE);
}

//======================
// Action SE
//======================
public void PlayPaintSE()
{
    if (paintSE != null)
        seSource.PlayOneShot(paintSE);
}

public void PlayColorChangeSE()
{
    if (colorChangeSE != null)
        seSource.PlayOneShot(colorChangeSE);
}
//======================
// Walk SE
//======================

public void StartWalkSE()
{
    if (walkSE == null)
        return;

    if (walkSource.isPlaying)
        return;

    walkSource.clip = walkSE;
    walkSource.loop = true;
    walkSource.Play();
}

public void StopWalkSE()
{
    walkSource.Stop();
}
//======================
// Event SE
//======================

public void PlayGameStartSE()
{
    Debug.Log("GameStartSEが呼ばれた");
    
    if (gameStartSE != null)
        seSource.PlayOneShot(gameStartSE);
}

public void PlayMoneySE()
{
    if (moneySE != null)
        seSource.PlayOneShot(moneySE);
}

public void PlayMissSE()
{
    if (missSE != null)
        seSource.PlayOneShot(missSE);
}

public void PlayShoppingSE()
{
    if (shoppingSE != null)
        seSource.PlayOneShot(shoppingSE);
}

public void PlayFailedPaintSE()
{
    if (failedPaintSE != null)
        seSource.PlayOneShot(failedPaintSE);
}

public void PlayClearSE()
{
    if (clearSE != null)
        seSource.PlayOneShot(clearSE);
}
}