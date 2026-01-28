using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 管理者に「鍵拾ったよ！」と報告
            GameMaster.Instance.AddKey();
            // 鍵自体は消える
            Destroy(gameObject);
        }
    }
}