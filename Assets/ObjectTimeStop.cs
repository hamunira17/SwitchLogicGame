using UnityEngine;

public class ObjectTimeStop : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 savedVelocity; // 止まる直前の速度を覚える
    private float savedAngularVelocity; // 回転も覚える

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void SetTimeStop(bool isStopping)
    {
        if (isStopping)
        {
            // 止める時：今の勢いを保存して、物理演算を「静止」モードにする
            savedVelocity = rb.linearVelocity;
            savedAngularVelocity = rb.angularVelocity;
            rb.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            // 動かす時：物理演算を「動的」に戻し、保存した勢いを叩き込む
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = savedVelocity;
            rb.angularVelocity = savedAngularVelocity;
        }
    }
}