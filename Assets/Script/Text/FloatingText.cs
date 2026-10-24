using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Vector3 startPos;

    [SerializeField] private float amplitude = 10f;
    [SerializeField] private float speed = 2f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;

        transform.localPosition = startPos + Vector3.up * y;
    }
}