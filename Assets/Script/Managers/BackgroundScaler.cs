using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldWidth =
            Camera.main.orthographicSize * 2f * Camera.main.aspect;

        float spriteWidth = sr.sprite.bounds.size.x;

        float scale = worldWidth / spriteWidth;

        transform.localScale = new Vector3(scale, scale, 1f);
    }
}