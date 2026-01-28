using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float playerFixedGravity = 3f;

    [Header("Collision Check")]
    [SerializeField] private float rayDistance = 0.05f;
    [SerializeField] private LayerMask collisionMask;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Vector2 currentInput = Vector2.zero;

    private bool isBlockedLeft;
    private bool isBlockedRight;
    private bool isGrounded;
    private bool isCeilinged;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponentInChildren<Animator>();

        // プレイヤー自身の重力は固定（世界重力の影響を受けない設計に合わせる）
        rb.gravityScale = playerFixedGravity;
    }

    public void OnMove(InputValue value)
    {
        currentInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        CheckCollisions();
        HandleMovement();
    }

    void HandleMovement()
    {
        if (isGrounded && isCeilinged)
        {
            // 物理的な固定
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;

            // ★スキンを「挟まれ顔」に変更
            if (anim != null) anim.SetBool("isCrushed", true);
            return;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            // ★スキンを「通常」に戻す
            if (anim != null) anim.SetBool("isCrushed", false);
        }

        // --- 通常の移動処理 ---
        float horizontalInput = currentInput.x;

        // 左右の壁判定
        if ((horizontalInput > 0 && isBlockedRight) || (horizontalInput < 0 && isBlockedLeft))
        {
            horizontalInput = 0;
        }

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void CheckCollisions()
    {
        if (playerCollider == null) return;

        // レイの起点計算（少し内側から飛ばすと安定します）
        Bounds bounds = playerCollider.bounds;
        Vector2 origin = rb.position;

        float halfWidth = bounds.size.x / 2f;
        float halfHeight = bounds.size.y / 2f;

        // 衝突判定（Raycast）
        isBlockedRight = Physics2D.Raycast(origin + new Vector2(halfWidth, 0), Vector2.right, rayDistance, collisionMask);
        isBlockedLeft = Physics2D.Raycast(origin + new Vector2(-halfWidth, 0), Vector2.left, rayDistance, collisionMask);
        isGrounded = Physics2D.Raycast(origin + new Vector2(0, -halfHeight), Vector2.down, rayDistance, collisionMask);
        isCeilinged = Physics2D.Raycast(origin + new Vector2(0, halfHeight), Vector2.up, rayDistance, collisionMask);
    }
}