using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 管理者に「鍵持ってる？」と聞く
            if (GameMaster.Instance.hasKey)
            {
                Debug.Log("🎉 鍵を使ってゴールしました！");

                // 鍵を消費し、リザルトを表示させる（社長、お願いします！）
                GameMaster.Instance.UseKey();
                GameMaster.Instance.GoalReached();

                // ドア自身の消滅（演出）
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("🚫 鍵がないので開けません。");
                // ここで「鍵が必要です」というUIを出してもいいですね
            }
        }
    }
}