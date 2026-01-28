using UnityEngine;

public class ObjectGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float gravityForce = 1.5f; // 通常の重力倍率
    [SerializeField] private float liftPower = 5f;    // プレイヤーを載せていても持ち上げる力

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void SetGravityDirection(bool isUp)
    {
        if (isUp)
        {
            // 上に上げたい時は、単なる重力反転だけでなく、
            // 直接「上への速度」を与えて、プレイヤーの重さをねじ伏せる
            rb.gravityScale = -gravityForce;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, liftPower);
        }
        else
        {
            rb.gravityScale = gravityForce;
        }
    }
}