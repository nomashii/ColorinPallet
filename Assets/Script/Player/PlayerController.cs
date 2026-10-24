using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    private bool canMove = true;
    public float moveSpeed = 6f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float mobileHorizontalInput;
    private Animator animator;
    private Vector2 touchStartPos;
    private bool isDragging;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    animator.SetBool("FacingRight", true);
    animator.SetBool("IsMoving", false);
}
    void Update()
{
    if (!canMove)
{
    mobileHorizontalInput = 0f;
    horizontalInput = 0f;
    return;
}
    // タッチ開始
if (Input.GetMouseButtonDown(0))
{
    // UIを押したらドラッグ開始しない
    if (EventSystem.current.IsPointerOverGameObject())
        return;

    touchStartPos = Input.mousePosition;
    isDragging = true;
}

// ドラッグ終了
if (Input.GetMouseButtonUp(0))
{
    mobileHorizontalInput = 0f;
    isDragging = false;
}
if (isDragging)
{
    if (EventSystem.current.IsPointerOverGameObject())
{
    mobileHorizontalInput = 0f;
    return;
}
    float distance =
        Input.mousePosition.x - touchStartPos.x;

    if (distance > 50f)
    {
        mobileHorizontalInput = 1f;
    }
    else if (distance < -50f)
    {
        mobileHorizontalInput = -1f;
    }
    else
    {
        mobileHorizontalInput = 0f;
    }
}
    horizontalInput = Input.GetAxisRaw("Horizontal");

    if (horizontalInput == 0)
    {
        horizontalInput = mobileHorizontalInput;
    }

    bool isMoving = Mathf.Abs(horizontalInput) > 0.1f;

animator.SetBool("IsMoving", isMoving);

if (isMoving)
{
    AudioManager.Instance.StartWalkSE();
}
else
{
    AudioManager.Instance.StopWalkSE();
}

    if (horizontalInput > 0)
    {
        animator.SetBool(
            "FacingRight",
            true);
    }
    else if (horizontalInput < 0)
    {
        animator.SetBool(
            "FacingRight",
            false);
    }
}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    public void MoveLeftButtonDown()
    {
        mobileHorizontalInput = -1f;
    }

    public void MoveRightButtonDown()
    {
        mobileHorizontalInput = 1f;
    }

    public void MoveButtonUp()
    {
        mobileHorizontalInput = 0f;
    }
    public void SetCanMove(bool value)
{
    canMove = value;

    if (!value)
    {
        AudioManager.Instance.StopWalkSE();
    }
}
public void ResetInput()
{
    mobileHorizontalInput = 0f;
    horizontalInput = 0f;

    isDragging = false;

    AudioManager.Instance.StopWalkSE();
}
}